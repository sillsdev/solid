[_ISTool]
EnableISX=true

#define MyAppName "Solid"

#ifndef MyAppVersion
#define MyAppVersion "1.0.0"
#endif

#define MyAppPublisher "SIL International"
#define MyAppURL "http://software.sil.org/solid"
#define MyAppExeName "Solid.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{4AE9E6FF-434D-4465-BB07-ADC3673461AC}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
; AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={commonpf}\{#MyAppName}
DisableDirPage=yes
DisableReadyPage=yes
DefaultGroupName={#MyAppName}
DisableProgramGroupPage=yes
LicenseFile=license.rtf
OutputBaseFilename=SolidInstaller-{AppVersion}
Compression=lzma
SolidCompression=yes
;WizardImageFile=compiler:WIZMODERNIMAGE-IS.BMP
CreateUninstallRegKey=true
UsedUserAreasWarning=no

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Dirs]
Name: {app}\mappings
Name: {app}\exporters
Name: {app}\templates
Name: {userdocs}\Solid Examples

[Files]
Source: ..\installer\isxdl.dll; Flags: dontcopy
Source: ..\output\net481\solid.exe; DestDir: {app}; Flags: replacesameversion
Source: ..\output\net481\*.dll; DestDir: {app}; Flags: replacesameversion
Source: ..\output\net481\*.config; DestDir: {app}; Flags: replacesameversion
Source: ..\mappings\MappingXmlToHtml.xsl; DestDir: {app}\mappings
Source: ..\mappings\LIFT.mappingSystem; DestDir: {app}\mappings
Source: ..\mappings\FLEX.mappingSystem; DestDir: {app}\mappings
Source: ..\templates\MDF.solid; DestDir: {app}\templates
Source: ..\templates\MDF.txt; DestDir: {app}\templates
Source: ..\templates\MDF Flat.solid; DestDir: {app}\templates
Source: ..\templates\MDF Loose.solid; DestDir: {app}\templates
Source: ..\templates\No Assumptions.solid; DestDir: {app}\templates
Source: ..\templates\PLB SFM.solid; DestDir: {app}\templates
Source: ..\templates\PLB SFM.txt; DestDir: {app}\templates
Source: ..\Solid Examples\BambaraSolidDemo.db; DestDir: {userdocs}\Solid Examples
Source: ..\Solid Examples\BambaraTutorial2.db; DestDir: {userdocs}\Solid Examples
Source: ..\doc\Solid Manual\Solid Documentation.pdf; DestDir: {app}
Source: ..\LICENSE.md; DestDir: {app}
Source: ..\DistFiles\*.*; DestDir: {app}\DistFiles
Source: ..\ArtWork\solid.png; DestDir: {app}\ArtWork

[Messages]
WinVersionTooLowError=Solid requires Windows 2000 or later.

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"

[Code]
var
  dotnetRedistPath: string;
  downloadNeeded: boolean;
  dotNetNeeded: boolean;
  memoDependenciesNeeded: string;

procedure isxdl_AddFile(URL, Filename: String);
external 'isxdl_AddFile@files:isxdl.dll stdcall';
function isxdl_DownloadFiles(hWnd: Integer): Integer;
external 'isxdl_DownloadFiles@files:isxdl.dll stdcall';
function isxdl_SetOption(Option, Value: String): Integer;
external 'isxdl_SetOption@files:isxdl.dll stdcall';

// Detect .NET framework 4.8.1 is missing
// See https://msdn.microsoft.com/en-us/library/hh925568(v=vs.110).aspx
function DotNetIsMissing(): Boolean;
var
  readVal: cardinal;
  success: Boolean;
begin
  success := RegQueryDWordValue(HKLM, 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full', 'Release', readVal);
  success := success and (readVal >= 533320); // 533320 is the number for 4.8.1
  Result := not success;
end;

function InitializeSetup(): Boolean;

begin
  Result := true;
  dotNetNeeded := false;

  // Check for required netfx installation
  if DotNetIsMissing() then begin
      MsgBox('Solid needs the Microsoft .NET Framework 4.8.1 or greater to be installed by an Administrator', mbInformation, MB_OK);
      Result := false;
    end;
end;

function NextButtonClick(CurPage: Integer): Boolean;
var
  hWnd: Integer;
  ResultCode: Integer;

begin
  Result := true;

  if CurPage = wpWelcome then begin

    hWnd := StrToInt(ExpandConstant('{wizardhwnd}'));

    // don't try to init isxdl if it's not needed because it will error on < ie 3
    if downloadNeeded then begin

      isxdl_SetOption('label', 'Downloading Microsoft .NET Framework');
      isxdl_SetOption('description', 'Solid needs to install the Microsoft .NET Framework. Please wait while Setup is downloading extra files to your computer. This may take several minutes.');
      if isxdl_DownloadFiles(hWnd) = 0 then Result := false;
    end;
    if (Result = true) and (dotNetNeeded = true) then begin
      if Exec(ExpandConstant(dotnetRedistPath), '', '', SW_SHOW, ewWaitUntilTerminated, ResultCode) then begin
         // handle success if necessary; ResultCode contains the exit code
         if not (ResultCode = 0) then begin
           Result := false;
         end;
      end else begin
         // handle failure if necessary; ResultCode contains the error code
         Result := false;
      end;
    end;
  end;
end;

function UpdateReadyMemo(Space, NewLine, MemoUserInfoInfo, MemoDirInfo, MemoTypeInfo, MemoComponentsInfo, MemoGroupInfo, MemoTasksInfo: String): String;
var
  s: string;

begin
  if memoDependenciesNeeded <> '' then s := s + 'Dependencies to install:' + NewLine + memoDependenciesNeeded + NewLine;
  s := s + MemoDirInfo + NewLine + NewLine;

  Result := s
end;

////////
// Code below uninstalls previous Solid version before installing the current one
// Taken from https://stackoverflow.com/a/2099805
////////

/////////////////////////////////////////////////////////////////////
function GetUninstallString(): String;
var
  sUnInstPath: String;
  sUnInstallString: String;
begin
  sUnInstPath := ExpandConstant('Software\Microsoft\Windows\CurrentVersion\Uninstall\{#emit SetupSetting("AppId")}_is1');
  sUnInstallString := '';
  if not RegQueryStringValue(HKLM, sUnInstPath, 'UninstallString', sUnInstallString) then
    RegQueryStringValue(HKCU, sUnInstPath, 'UninstallString', sUnInstallString);
  Result := sUnInstallString;
end;


/////////////////////////////////////////////////////////////////////
function IsUpgrade(): Boolean;
begin
  Result := (GetUninstallString() <> '');
end;


/////////////////////////////////////////////////////////////////////
function UnInstallOldVersion(): Integer;
var
  sUnInstallString: String;
  iResultCode: Integer;
begin
// Return Values:
// 1 - uninstall string is empty
// 2 - error executing the UnInstallString
// 3 - successfully executed the UnInstallString

  // default return value
  Result := 0;

  // get the uninstall string of the old app
  sUnInstallString := GetUninstallString();
  if sUnInstallString <> '' then begin
    sUnInstallString := RemoveQuotes(sUnInstallString);
    if Exec(sUnInstallString, '/SILENT /NORESTART /SUPPRESSMSGBOXES','', SW_HIDE, ewWaitUntilTerminated, iResultCode) then
      Result := 3
    else
      Result := 2;
  end else
    Result := 1;
end;

/////////////////////////////////////////////////////////////////////
procedure CurStepChanged(CurStep: TSetupStep);
begin
  if (CurStep=ssInstall) then
  begin
    if (IsUpgrade()) then
    begin
      UnInstallOldVersion();
    end;
  end;
end;

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, "&", "&&")}}"; Flags: nowait postinstall skipifsilent


[InnoIDE_Settings]
LogFileOverwrite=false

