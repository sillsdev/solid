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
Source: ..\output\net8.0-windows\win-x64\solid.exe; DestDir: {app}; Flags: replacesameversion
Source: ..\output\net8.0-windows\win-x64\*.dll; DestDir: {app}; Flags: replacesameversion
Source: ..\output\net8.0-windows\win-x64\*.config; DestDir: {app}; Flags: replacesameversion
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

