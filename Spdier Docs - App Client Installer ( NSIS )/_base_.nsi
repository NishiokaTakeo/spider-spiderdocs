;---------------------------------------------------------------------------
; Main Spider Docs Installer Script for NSIS3.
; Created by Takeo Nishioka 11/05/2017
;
; Prerequisites
;	- NSISList
;	- NSIS_Simple_Service
;	- Registry
;	- NsisDotNetChecker
;---------------------------------------------------------------------------

;--------------------------------
;Include System nsh

  !include "MUI2.nsh"
  !include "LogicLib.nsh"
  !include "x64.nsh"
  !include "FileFunc.nsh"

;--------------------------------
; List Plagin
; How to use : http://nsis.sourceforge.net/NSISList_plug-in
; How to download : http://nsis.sourceforge.net/mediawiki/images/4/4e/NSISList-Plugin.zip
  !include NSISList.nsh
  ;Reserve the NSISList plugin
  ReserveFile "${NSISDIR}\Plugins\NSISList.dll"

;--------------------------------
; Service Plugin
; How to use : http://nsis.sourceforge.net/NSIS_Simple_Service_Plugin
; How to download : http://nsis.sourceforge.net/mediawiki/images/c/c9/NSIS_Simple_Service_Plugin_1.30.zip
  ReserveFile "${NSISDIR}\Plugins\SimpleSC.dll"


;--------------------------------
; Registry Plugin
; How to use : http://nsis.sourceforge.net/Registry_plug-in
; How to download : http://nsis.sourceforge.net/mediawiki/images/4/47/Registry.zip
  !include "Registry.nsh" ;
  ReserveFile "${NSISDIR}\Plugins\registry.dll"


;--------------------------------
; .NET Framework Checker NSIS plugin
; https://github.com/ReVolly/NsisDotNetChecker
  !include "DotNetChecker.nsh"

;--------------------------------
; Shell Link plugin
; http://nsis.sourceforge.net/ShellLink_plug-in
  ReserveFile "${NSISDIR}\Plugins\ShellLink.dll"

;--------------------------------
; AccessControl plug-in
; http://nsis.sourceforge.net/AccessControl_plug-in
  ReserveFile "${NSISDIR}\Plugins\AccessControl.dll"

;--------------------------------
; NsJSON plug-in
; http://nsis.sourceforge.net/NsJSON_plug-in
  ReserveFile "${NSISDIR}\Plugins\nsJSON.dll"

;--------------------------------
; NsProcess plugin
; http://nsis.sourceforge.net/NsProcess_plugin
ReserveFile "${NSISDIR}\Plugins\nsProcess.dll"
!include "nsProcess.nsh"

; Include My nsh
;--------------------------------
  !include "header.nsh"


;DetailPrint "test "
;--------------------------------
;General, Define install attribute
  Var PROG_PATH # program file directory path. depend on CPU BIT

  ;Name and file
  Name "Spider Docs Installer"
  OutFile "setup.exe"


  ;Request application privileges for Windows Vista
  RequestExecutionLevel admin

  InstallDirRegKey HKLM "Software\SpiderDocs" SpiderDocsPath   ;Get installation folder from registry if available

;--------------------------------
;Interface Settings

  !define MUI_ABORTWARNING

;--------------------------------
;Custom Codes

  !define _VERSION                 "####VERSION####"
  !define _COMPANY_NAME            "Spider Developments"
  !define _PRODUCT_NAME            "Spider Docs"
  !define _WEBSITEADDRESS          "http://www.spiderdevelopments.com.au"
  !define _INSTALLSIZE             "100000"

  # Welcome page
  !define MUI_WELCOMEPAGE_TEXT "The Install Wizard will install Spider Docs on your computer. To continue, click Next."
  !define MUI_WELCOMEPAGE_TITLE "Welcome to the Install Wizard for Spider Docs Version ${_VERSION}"
   BrandingText "Spider Developments @ 2018"

  !define MUI_ICON "logo.ico"
  !define MUI_HEADERIMAGE
  !define MUI_HEADERIMAGE_BITMAP "header.bmp"
  !define MUI_HEADERIMAGE_RIGHT

  ; Sign the application
  ; To use signtool.exe, you might need to add path to your environemnt's PATH'. in my case C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin
  ; see more information : https://msdn.microsoft.com/en-us/library/bb756995.aspx
  ;                        http://stackoverflow.com/questions/10581570/setting-the-uac-publisher-field-for-a-nsis-installer
  !finalize 'signtool.exe sign /f ../SpiderDocs/SpiderCertificate.pfx /p "*Aspider#" /t http://timestamp.digicert.com "%1"'


;--------------------------------
;Pages

  !insertmacro MUI_PAGE_WELCOME
  ;!insertmacro MUI_PAGE_LICENSE "License.txt"

  ;!insertmacro MUI_PAGE_DIRECTORY

  !insertmacro MUI_PAGE_DIRECTORY
  !insertmacro MUI_PAGE_INSTFILES

  !insertmacro MUI_PAGE_FINISH

  !insertmacro MUI_UNPAGE_WELCOME
  !insertmacro MUI_UNPAGE_CONFIRM

  !insertmacro MUI_UNPAGE_INSTFILES
  !insertmacro MUI_UNPAGE_FINISH

;--------------------------------
;Languages
  !insertmacro MUI_LANGUAGE "English"

  !insertmacro define

;--------------------------------
;Callback Function
  Function .onInit
    !insertmacro checkprocess

    ${If} ${RunningX64}
      SetRegView 64
    ${Else}
      SetRegView 32
    ${EndIf}

    SetShellVarContext all
    !insertmacro init
    !insertmacro WriteLogRoot "Install Start"
  FunctionEnd

  Function un.onInit
    SetShellVarContext all
    !insertmacro init
    !insertmacro WriteLogRoot "Uninstall Start"
  FunctionEnd

  Function .onInstFailed
    !insertmacro WriteLogRoot "Install End (Failed)"
    FileClose $LOG
  FunctionEnd

  Function .onInstSuccess
    !insertmacro WriteLogRoot "Install End (Success)"
    FileClose $LOG
  FunctionEnd

  Function un.onUninstFailed
    !insertmacro WriteLogRoot "Uninstall End (Failed)"
    FileClose $LOG
  FunctionEnd

  Function un.onUninstSuccess
    !insertmacro WriteLogRoot "Uninstall End (Success)"
    FileClose $LOG
  FunctionEnd



;--------------------------------
;Pages

  #!insertmacro MUI_PAGE_DIRECTORY

;--------------------------------
;Installer Sections
  Section

    ${If} $INSTDIR == ""
      StrCpy $INSTDIR "$PROG_PATH\${_PRODUCT_NAME}"
    ${EndIf}

    #stop service
    !insertmacro StopService  $SERVICE_AUTOUPDATE_NAME

    # backup envirionment registry keys
      #StrCpy PROG_PATH SpiderDocsPath

    # call uninstaller
  	;!insertmacro call_uninstaller

    # install
	!insertmacro create_dirs

	!insertmacro InstallPrerequisites

	!insertmacro uninstall_sheildinstaller "run"

	WriteUninstaller "$INSTDIR\uninstall.exe"

    !insertmacro copies_all_dirs

	!insertmacro MainSpiderDocs "c"

	!insertmacro Shortcut "c"

	#wait until all office applications are closed
	!insertmacro OfficeAddin "c"

    !insertmacro product_registory "c"

    !insertmacro settingJson

    !insertmacro InstallService $SERVICE_AUTOUPDATE_NAME

        #System.IO.File.Copy(program_files_folder + "SpiderDocsInstaller.exe", windows_folder + "SpiderDocsInstaller.exe", true);
    !insertmacro StasrtService $SERVICE_AUTOUPDATE_NAME

    Call RefreshShellIcons

    !insertmacro WriteLog " END OF SECTION"
  SectionEnd

;--------------------------------
;Uninstaller Section

  Section "Uninstall"

    # stop service
    !insertmacro RemoveService $SERVICE_AUTOUPDATE_NAME

    !insertmacro uninstall_sheildinstaller "run"

    !insertmacro MainSpiderDocs 1

    !insertmacro Shortcut 1

    !insertmacro OfficeAddin 1

    !insertmacro remove_all_dirs

    !insertmacro product_registory 1

    #Sleep 5000
  SectionEnd






#!define LogWrite !insertmacro _LogWrite
