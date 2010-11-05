[_ISTool]
EnableISX=true

#define MyAppName "Solid"
#define MyAppVersion "0.0.0"
#define MyAppPublisher "Payap Language Software"
#define MyAppURL "http://solid.palaso.org"
#define MyAppExeName "Solid.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{4AE9E6FF-434D-4465-BB07-ADC3673461AC}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\{#MyAppName}
DisableDirPage=yes
DisableReadyPage=yes
DefaultGroupName={#MyAppName}
DisableProgramGroupPage=yes
LicenseFile=license.rtf
OutputBaseFilename=SolidInstaller
Compression=lzma
SolidCompression=yes
WizardImageFile=compiler:WIZMODERNIMAGE-IS.BMP 
CreateUninstallRegKey=true

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Dirs]
Name: {app}\mappings
Name: {app}\exporters
Name: {app}\templates
Name: {userdocs}\Solid Examples

[Files]
Source: ..\installer\isxdl.dll; Flags: dontcopy
Source: ..\output\release\Solid.exe; DestDir: {app}; Flags: replacesameversion
Source: ..\output\release\KeymanLink.dll; DestDir: {app}; Flags: replacesameversion
Source: ..\output\release\GlacialList.dll; DestDir: {app}; Flags: replacesameversion
Source: ..\output\release\Palaso.dll; DestDir: {app}; Flags: replacesameversion
Source: ..\output\release\PalasoUIWindowsForms.dll; DestDir: {app}; Flags: replacesameversion
Source: ..\output\release\LiftIO.dll; DestDir: {app}; Flags: replacesameversion
Source: ..\output\release\Palaso.DictionaryServices.dll; DestDir: {app}; Flags: replacesameversion
Source: ..\output\release\Palaso.Lift.dll; DestDir: {app}; Flags: replacesameversion
Source: ..\mappings\MappingXmlToHtml.xsl; DestDir: {app}\mappings
Source: ..\mappings\LIFT.mappingSystem; DestDir: {app}\mappings
Source: ..\mappings\FLEX.mappingSystem; DestDir: {app}\mappings
Source: ..\templates\MDF Unicode.solid; DestDir: {app}\templates
Source: ..\templates\MDF Legacy Font.solid; DestDir: {app}\templates
Source: ..\templates\MDF Alternate Legacy Font.solid; DestDir: {app}\templates
Source: ..\templates\MDF Alternate Unicode.solid; DestDir: {app}\templates
Source: ..\templates\FLEx-friendly MDF Unicode.solid; DestDir: {app}\templates
Source: ..\templates\FLEx-friendly MDF Legacy Font.solid; DestDir: {app}\templates
Source: ..\Solid Examples\BambaraSolidDemo.db; DestDir: {userdocs}\Solid Examples
Source: ..\Solid Examples\BambaraTutorial2.db; DestDir: {userdocs}\Solid Examples

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


const
  dotnetRedistURL = 'http://palaso.org/install/dotnet2/dotnetfx.exe';
  // local system for testing...
  //dotnetRedistURL = 'http://localhost/install/dotnet2/dotnetfx.exe';

function InitializeSetup(): Boolean;

begin
  Result := true;
  dotNetNeeded := false;

  // Check for required netfx installation
  if (not RegKeyExists(HKLM, 'Software\Microsoft\.NETFramework\policy\v2.0')) then begin
    dotNetNeeded := true;
    if (not IsAdminLoggedOn()) then begin
      MsgBox('Solid needs the Microsoft .NET Framework to be installed by an Administrator', mbInformation, MB_OK);
      Result := false;
    end else begin
      memoDependenciesNeeded := memoDependenciesNeeded + '      .NET Framework' #13;
      dotnetRedistPath := ExpandConstant('{src}\dotnetfx.exe');
      if not FileExists(dotnetRedistPath) then begin
        dotnetRedistPath := ExpandConstant('{tmp}\dotnetfx.exe');
        if not FileExists(dotnetRedistPath) then begin
          isxdl_AddFile(dotnetRedistURL, dotnetRedistPath);
          downloadNeeded := true;
        end;
      end;
      SetIniString('install', 'dotnetRedist', dotnetRedistPath, ExpandConstant('{tmp}\dep.ini'));
    end;
  end;

  // For testing
  //dotNetNeeded := true;
  //downloadNeeded := true;
  //SetIniString('install', 'dotnetRedist', dotnetRedistPath, ExpandConstant('{tmp}\dep.ini'));


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

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, "&", "&&")}}"; Flags: nowait postinstall skipifsilent


[InnoIDE_Settings]
LogFileOverwrite=false

