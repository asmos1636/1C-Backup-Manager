#define MyAppName "1C Backup Manager"
#define MyAppVersion "0.1.0"
#define MyPublisher "1C Backup Manager"
#define UiDir "..\\artifacts\\ui"
#define WorkerDir "..\\artifacts\\worker"

[Setup]
AppId={{8A94A2D1-6D4E-4D6F-9C35-2E0A3B8F1C70}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyPublisher}
DefaultDirName={autopf}\\1C Backup Manager
DefaultGroupName=1C Backup Manager
OutputDir=..\\artifacts\\installer
OutputBaseFilename=1C-Backup-Manager-Setup-0.1.0-x64
ArchitecturesAllowed=x64
ArchitecturesInstallIn64BitMode=x64
PrivilegesRequired=admin
WizardStyle=modern
Compression=lzma2
SolidCompression=yes
UninstallDisplayIcon={app}\\OneCBackupManager.UI.exe

[Dirs]
Name: "{commonappdata}\\1C Backup Manager"; Permissions: users-modify
Name: "{commonappdata}\\1C Backup Manager\\Logs"; Permissions: users-modify

[Files]
Source: "{#UiDir}\\*"; DestDir: "{app}\\UI"; Flags: recursesubdirs ignoreversion
Source: "{#WorkerDir}\\*"; DestDir: "{app}\\Worker"; Flags: recursesubdirs ignoreversion

[Icons]
Name: "{group}\\1C Backup Manager"; Filename: "{app}\\UI\\OneCBackupManager.UI.exe"
Name: "{autodesktop}\\1C Backup Manager"; Filename: "{app}\\UI\\OneCBackupManager.UI.exe"; Tasks: desktopicon

[Tasks]
Name: "desktopicon"; Description: "Создать ярлык на рабочем столе"; GroupDescription: "Дополнительные значки:"; Flags: unchecked

[Run]
Filename: "{sys}\\sc.exe"; Parameters: "create ""1CBackupManagerWorker"" binPath= ""{app}\\Worker\\OneCBackupManager.Worker.exe"" start= auto DisplayName= ""1C Backup Manager Worker"""; Flags: runhidden waituntilterminated
Filename: "{sys}\\sc.exe"; Parameters: "start ""1CBackupManagerWorker"""; Flags: runhidden waituntilterminated; StatusMsg: "Запуск фоновой службы..."; Check: not WizardSilent

[UninstallRun]
Filename: "{sys}\\sc.exe"; Parameters: "stop ""1CBackupManagerWorker"""; Flags: runhidden waituntilterminated
Filename: "{sys}\\sc.exe"; Parameters: "delete ""1CBackupManagerWorker"""; Flags: runhidden waituntilterminated
