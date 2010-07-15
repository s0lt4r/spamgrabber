; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

; http://msdn2.microsoft.com/en-us/library/bb332051.aspx
[Setup]
AppName=Spamgrabber
AppVerName=Spamgrabber 4.0.8
AppVersion=4.0.8
AppPublisher=SoftScan
AppPublisherURL=http://www.softscan.dk
AppSupportURL=http://www.softscan.dk
AppUpdatesURL=http://www.softscan.dk
CreateAppDir=true
OutputBaseFilename=SpamgrabberSoftScansetup
Compression=lzma
SolidCompression=yes
CreateUninstallRegKey=no
UpdateUninstallLogAppName=no
VersionInfoVersion = 4.0.8
DefaultDirName={pf}\SpamGrabber

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Files]
Source: "SpamGrabberSetupSoftScan.msi"; DestDir: "{app}"; Flags: ignoreversion
Source: "Office2007PIA\o2007pia.msi"; DestDir: "{app}"; Flags: ignoreversion
Source: "Office2003PIA\o2003pia.msi"; DestDir: "{app}"; Flags: ignoreversion
Source: "ComponentCheck.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "AppCheck.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "vstor.exe"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Run]
Filename: "{app}\vstor.exe"; Parameters: "/q"; Check:(CheckInstallVSTORCheck());StatusMsg: "Installing VSTO 2005 SE..."
Filename: "msiexec.exe"; Parameters: "/i ""{app}\o2003pia.msi"" /qn"; Check:(CheckInstallOffice2003PIA());StatusMsg: "Installing Office 2003 PIA..."
Filename: "msiexec.exe"; Parameters: "/i ""{app}\o2007pia.msi"" /qn"; Check:(CheckInstallOffice2007PIA());StatusMsg: "Installing Office 2007 PIA..."
Filename: "msiexec.exe"; Parameters: "/i ""{app}\SpamGrabberSetupSoftScan.msi"" /qn"; Check:(CheckInstallSpamgrabber());Flags:skipifnotsilent; StatusMsg: "Installing Spamgrabber..."
Filename: "msiexec.exe"; Parameters: "/i ""{app}\SpamGrabberSetupSoftScan.msi"""; Check:(CheckInstallSpamgrabber());Flags: skipifsilent; StatusMsg: "Installing Spamgrabber..."

[UninstallRun]
Filename: "msiexec.exe"; Parameters: "/x ""{app}\SpamGrabberSetupSoftScan.msi"" /qn"; Check:(CheckInstallSpamgrabber());StatusMsg: "Uninstalling Spamgrabber..."


[Code]
var
  InstallOffice2003PIA: Boolean;
  InstallOffice2007PIA: Boolean;
  InstallVSTOR: Boolean;
  InstallSpamgrabber: Boolean;
  InstallPreReq: Boolean;


 // Installation test of outlook
function IsOutlookInstalled(OutlookVersion:Integer):Boolean;
var
  ResultCode : Integer;
  ShouldInstall : Boolean;
begin
  if(OutlookVersion=2007) then
    begin
      Exec(ExpandConstant('{app}\ComponentCheck.exe'), '{0638C49D-BB8B-4CD1-B191-050E8F325736}', '', SW_SHOW,
          ewWaitUntilTerminated, ResultCode);  // PIA Conponent installed ?
     end
  else
     begin
      Exec(ExpandConstant('{app}\ComponentCheck.exe'), '3EC1EAE0-A256-411D-B00B-016CA8376078', '', SW_SHOW,
            ewWaitUntilTerminated, ResultCode);  // PIA Conponent installed ?
  end;

  if ResultCode=0 then // office installed ?
    begin
      ShouldInstall := True;// office installed
//      InstallSpamgrabber := True;
    end
  else
    begin
      ShouldInstall := False; // office Not Installed
  end;

  Result := ShouldInstall;
end;


function CheckInstallOffice2003PIA(): Boolean;
var
  ResultCode : Integer;
begin
// {14D3E42A-A318-4D77-9895-A7EE585EFC3B} outlook 2003 PIA
  if Exec(ExpandConstant('{app}\ComponentCheck.exe'), '{14D3E42A-A318-4D77-9895-A7EE585EFC3B}', '', SW_SHOW,
     ewWaitUntilTerminated, ResultCode) then // PIA Conponent installed ?
  begin
    if ResultCode = 0 then begin
      InstallOffice2003PIA := False; // office 2003 pia installed
    end else begin
      InstallOffice2003PIA := True; // office 2003 pia not installed
    end;
  end;

  Result := InstallOffice2003PIA;
end;


// Check for installation of office 2007 PIA components
function CheckInstallOffice2007PIA(): Boolean;
var
  ResultCode : Integer;
  
begin
// {ED569DB3-58C4-4463-971F-4AAABB6440BD} Outlook 2007 PIA
  if Exec(ExpandConstant('{app}\ComponentCheck.exe'), '{ED569DB3-58C4-4463-971F-4AAABB6440BD}', '', SW_SHOW,
     ewWaitUntilTerminated, ResultCode) then // PIA Conponent installed ?
  begin
    if ResultCode = 0 then
    begin
      InstallOffice2007PIA := False;// Office 2007 PIA installed
    end else begin
      InstallOffice2007PIA := True; // Office 2007 PIA not installed
    end;
  end;
  
  Result := InstallOffice2007PIA;
end;

// check for installation of VSTOR
function CheckInstallVSTORCheck():Boolean;
var
  ResultCode : Integer;
begin
  if Exec(ExpandConstant('{app}\ComponentCheck.exe'), '{D2AC2FF1-6F93-40D1-8C0F-0E135956A2DA}', '', SW_SHOW,
      ewWaitUntilTerminated, ResultCode) then
    begin
      if ResultCode = 0 then begin
        InstallVSTOR :=False; // vstor installed
      end else begin
        InstallVSTOR :=True; // vstor not installed
      end;
    end
  else
    begin
      InstallVSTOR :=True; // vstor not installed
    end;
  
  Result := InstallVSTOR;
end;

// --------------------------------------------------------------------------------
function CheckInstallSpamgrabber():Boolean;
var
  ResultCode : Integer;
  
begin
  if Exec(ExpandConstant('{app}\ComponentCheck.exe'), '{14D3E42A-A318-4D77-9895-A7EE585EFC3B}', '', SW_SHOW,
     ewWaitUntilTerminated, ResultCode) then // PIA Conponent installed ?
  begin
    if ResultCode = 0 then begin
      InstallSpamgrabber := True; // Office 2003 installed
    end else begin
      InstallSpamgrabber := False; // Office 2003 not installed
    end;
  end;
  
  if Exec(ExpandConstant('{app}\ComponentCheck.exe'), '{ED569DB3-58C4-4463-971F-4AAABB6440BD}', '', SW_SHOW,
     ewWaitUntilTerminated, ResultCode) then // PIA Conponent installed ?
  begin
    if ResultCode = 0 then
    begin
      InstallSpamgrabber := True; // Office 2007 PIA installed
    end else begin
      if InstallSpamgrabber <> True then begin
        InstallSpamgrabber :=  False;// Office 2007 PIA not installed
      end;
    end;
  end;
  
  Result := InstallSpamgrabber;
end;
//-------------------------------------------------------------------------------------

function InitializeSetup(): Boolean;
var
    NetFrameWorkInstalled : Boolean;
    Result1 : Boolean;
begin
  NetFrameWorkInstalled := RegKeyExists(HKLM,'SOFTWARE\Microsoft\.NETFramework\policy\v2.0');

  if NetFrameWorkInstalled = true then
  begin
    Result := true;
  end;

  if NetFrameWorkInstalled =false then
  begin
    Result1 := MsgBox('This setup requires the .NET 2.0 Framework. Please download and install the .NET Framework and run this setup again.',
              mbConfirmation, MB_OK) = idOk;

    Result:=false;
//        ShellExec('open', ExpandConstant('{src}\dotnetfx.exe'),'', '', SW_SHOW, ewNoWait, ErrorCode);
        //Run the .NET redistributable here using shellexec.

    end;
end;

