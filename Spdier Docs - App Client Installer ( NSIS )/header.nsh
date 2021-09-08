

!macro define
  Var /GLOBAL SERVICE_AUTOUPDATE_NAME
  Var /GLOBAL SERVICE_AUTOUPDATE_EXE

  # in NSIS, All variables are global scope even if you decraler in the Section, Function and Macro.
  # so I had write here
  Var /GLOBAL i
  Var /GLOBAL j
	Var /GLOBAL k
  Var /GLOBAL var
  Var /GLOBAL var1
  Var /GLOBAL var2
  Var /GLOBAL var3
  Var /GLOBAL var4
  Var /GLOBAL var5
  Var /GLOBAL var6
  Var /GLOBAL var7
  Var /GLOBAL var8

  Var /GLOBAL len1
  Var /GLOBAL len2
  Var /GLOBAL len3

  Var /GLOBAL FH1

	Var /GLOBAL NOW

  Var /GLOBAL LOG
  Var /GLOBAL IsDelete

	Var /GLOBAL PRODUCT_CODE
  Var /GLOBAL INSTDIR_BACK

  Var /GLOBAL ADDIN_DIR_NAME

  Var /GLOBAL _HKCR
  Var /GLOBAL _HKCU
  Var /GLOBAL _HKLM
  Var /GLOBAL _HKU
  Var /GLOBAL _HKPD
  Var /GLOBAL _HKCC
  Var /GLOBAL _HKDD
  #Var /GLOBAL processFoundprocessFound
!macroend

!define FindProc_NOT_FOUND 1
!define FindProc_FOUND 0
!macro FindProc result processName
    ExecCmd::exec "%SystemRoot%\System32\tasklist /NH /FI $\"IMAGENAME eq ${processName}$\" | %SystemRoot%\System32\find /I $\"${processName}$\""
    Pop $0 ; The handle for the process
    ExecCmd::wait $0
    Pop ${result} ; The exit code
!macroend


!macro Now
	${GetTime} "" "L" $0 $1 $2 $3 $4 $5 $6
	; $0="01"      day
	; $1="04"      month
	; $2="2005"    year
	; $3="Friday"  day of week name
	; $4="16"      hour
	; $5="05"      minute
	; $6="50"      seconds
  StrCpy $NOW '$2-$0-$1 ($3) $4:$5:$6'

!macroend

!macro init
	StrCpy $INSTDIR_BACK $INSTDIR

	${If} ${RunningX64}
	# 64 bit code
	StrCpy $PROG_PATH $PROGRAMFILES32
	${Else}
	# 32 bit code
	StrCpy $PROG_PATH $PROGRAMFILES
	${EndIf}

	StrCpy $INSTDIR "$PROG_PATH\${_PRODUCT_NAME}"


	${List.Create} ListApp
	${List.Add} ListApp "Excel"
	${List.Add} ListApp "Outlook"
	${List.Add} ListApp "Outlook"
	${List.Add} ListApp "PowerPoint"
	${List.Add} ListApp "Word"

	${List.Create} ListAddin
	${List.Add} ListAddin "AddInExcel2013"
	${List.Add} ListAddin "AddInOutlookAttachment2013"
	${List.Add} ListAddin "AddInOutlook2013"
	${List.Add} ListAddin "AddInPowerPoint2013"
	${List.Add} ListAddin "AddInWord2013"

	${List.Create} ListOfficeVersion
	#${List.Add} ListOfficeVersion "11.0"
	#${List.Add} ListOfficeVersion "12.0"
	#${List.Add} ListOfficeVersion "13.0"
	#${List.Add} ListOfficeVersion "14.0"
	#${List.Add} ListOfficeVersion "15.0"
	#${List.Add} ListOfficeVersion "16.0"

	# Get information json file contains
	${registry::Read} "HKEY_CLASSES_ROOT\Outlook.Application\CurVer" "" $R0 $R1
	StrCpy $var $R0
	StrCpy $var "$R0" "" -2
	#${StrReplace} "$var" "Outlook.Application." ""

	${If} $R1 == "REG_SZ"
	  ${List.Add} ListOfficeVersion "$var.0"
	${EndIf}

	${List.Create} ListAddinDescription
	${List.Add} ListAddinDescription "Spider Docs Excel Addin"
	${List.Add} ListAddinDescription "Spider Docs Outlook Attachment Addin"
	${List.Add} ListAddinDescription "Spider Docs Outlook Addin"
	${List.Add} ListAddinDescription "Spider Docs PowerPoint Addin"
	${List.Add} ListAddinDescription "Spider Docs Word Addin"

	${List.Create} SendToFileNames
	${List.Add} SendToFileNames "${_PRODUCT_NAME} - Database.lnk"
	${List.Add} SendToFileNames "${_PRODUCT_NAME} - Workspace.lnk"

	${List.Create} SendToFileArgs
	${List.Add} SendToFileArgs "savefile"
	${List.Add} SendToFileArgs "workspace"

	${List.Create} SendToFileText
	${List.Add} SendToFileText "Import to Database"
	${List.Add} SendToFileText "Import to Work Space"


	StrCpy $SERVICE_AUTOUPDATE_NAME "Spider Docs Auto Update"
	StrCpy $SERVICE_AUTOUPDATE_EXE "AutoUpdateService.exe"

	!insertmacro Now

	StrCpy $ADDIN_DIR_NAME "Spider Docs AddIns"

	StrCpy $_HKCR "HKEY_CLASSES_ROOT"
	StrCpy $_HKCU "HKEY_CURRENT_USER"
	StrCpy $_HKLM "HKEY_LOCAL_MACHINE"
	StrCpy $_HKU "HKEY_USERS"
	StrCpy $_HKPD "HKEY_PERFORMANCE_DATA"
	StrCpy $_HKCC "HKEY_CURRENT_CONFIG"
	StrCpy $_HKDD "HKEY_DYN_DATA"

	FileOpen $LOG "$DESKTOP\send-me-if-spiderdocs-has-any-issue.txt" a

!macroend

!macro WriteLog text
  !insertmacro Now
  FileWrite $LOG "$\t$NOW:${text}$\r$\n"
!macroend

!macro WriteLogRoot text
  !insertmacro Now
  FileWrite $LOG "==================== $NOW:${text}$\r$\n"
!macroend


;
; dialog if spiderdocs.exe is running.
;
!macro checkprocess

  ; force kill if silent
  ${If} ${Silent}
    ${nsProcess::KillProcess} "SpiderDocs.exe" $R0
    ${nsProcess::KillProcess} "WINWORD.EXE" $R1
    ${nsProcess::KillProcess} "POWERPNT.EXE" $R2
    ${nsProcess::KillProcess} "EXCEL.EXE" $R3
    ${nsProcess::KillProcess} "OUTLOOK.EXE" $R4

  ${Else}

    ${nsProcess::FindProcess} "SpiderDocs.exe" $R0

    ${If} $R0 == 0
      MessageBox MB_OK|MB_ICONEXCLAMATION "Spider Docs is running. Please close it first ($R0) " /SD IDOK
      Abort
    ${EndIf}

    ${nsProcess::FindProcess} "WINWORD.EXE" $R0
    ${nsProcess::FindProcess} "POWERPNT.EXE" $R1
    ${nsProcess::FindProcess} "EXCEL.EXE" $R2
    ${nsProcess::FindProcess} "OUTLOOK.EXE" $R3
    ${If} $R0 == 0
    ${OrIf} $R1 == 0
    ${OrIf} $R2 == 0
    ${OrIf} $R3 == 0
      MessageBox MB_OK|MB_ICONEXCLAMATION "Microsoft Office is running. Please close it first ($R0,$R1,$R2,$R3) " /SD IDOK
      Abort
    ${EndIf}

  ${EndIf}

  ${nsProcess::Unload}
!macroend


#
# $0 means IS_DELETE
#
!macro Shortcut IsDelete

	StrCpy $i 0
	StrCpy $var1 ""

	!insertmacro WriteLog "Shortcut : ${IsDelete} ----- "

	FindFirst $FH1 $var1 "$DESKTOP\..\..\*"
	loop:
		StrCmp $var1 "" done

		#!insertmacro WriteLog "$DESKTOP\..\..\$var1"
		${If} ${IsDelete} == 1
			!insertmacro SendTo "$DESKTOP\..\..\$var1\AppData\Roaming\Microsoft\Windows\SendTo" 1
			!insertmacro WriteLog "remove sendto shortcut At $DESKTOP\..\..\$var1\AppData\Roaming\Microsoft\Windows\SendTo"
		${Else}
			#!insertmacro SendTo "$DESKTOP\..\..\$var1\AppData\Roaming\Microsoft\Windows\SendTo" "c"
			#!insertmacro WriteLog "create sendto shortcut At $DESKTOP\..\..\$var1\AppData\Roaming\Microsoft\Windows\SendTo"
		${EndIf}


		/*
		FindFirst $FH2 $var2 "$DESKTOP\..\..\$var1\AppData\Roaming\Microsoft\Windows\SendTo\${_PRODUCT_NAME} - Workspace.lnk"
		${If} $var2  == "${_PRODUCT_NAME} - Workspace.lnk"
			!insertmacro Del "$DESKTOP\..\..\$var1\AppData\Roaming\Microsoft\Windows\SendTo\${_PRODUCT_NAME} - Workspace.lnk"
		${EndIf}
		FindClose $FH2

		FindFirst $FH2 $var2 "$DESKTOP\..\..\$var1\AppData\Roaming\Microsoft\Windows\SendTo\${_PRODUCT_NAME} - Database.lnk"
		${If} $var2  == "${_PRODUCT_NAME} - Database.lnk"
			!insertmacro Del "$DESKTOP\..\..\$var1\AppData\Roaming\Microsoft\Windows\SendTo\${_PRODUCT_NAME} - Database.lnk"
		${EndIf}
		*/
		FindNext $FH1 $var1
		Goto loop
	done:
	FindClose $FH1

	# The above code does not include Public\Desctop. Here remove the .lnk in Public\Desctop
	${If} ${IsDelete} == 1
		Delete "$DESKTOP\${_PRODUCT_NAME}.lnk"
		!insertmacro WriteLog "remove desktop shortcut At $DESKTOP\${_PRODUCT_NAME}.lnk"
	${Else}
		CreateShortCut "$DESKTOP\${_PRODUCT_NAME}.lnk" "$INSTDIR\SpiderDocs.exe" "" "$INSTDIR\icon3d.ico"
		CreateDirectory "$SMPROGRAMS\${_PRODUCT_NAME}"
		CreateShortCut "$SMPROGRAMS\${_PRODUCT_NAME}\${_PRODUCT_NAME}.lnk" "$INSTDIR\SpiderDocs.exe" "" "$INSTDIR\icon3d.ico"

    #ShellLink::SetRunAsAdministrator "$DESKTOP\${_PRODUCT_NAME}.lnk"
    #ShellLink::SetRunAsAdministrator "$SMPROGRAMS\${_PRODUCT_NAME}\${_PRODUCT_NAME}.lnk"

		!insertmacro WriteLog "create shortcut with '$DESKTOP\${_PRODUCT_NAME}.lnk' '$INSTDIR\SpiderDocs.exe' '' '$INSTDIR\icon3d.ico'"
		!insertmacro WriteLog "create directory with '$SMPROGRAMS\${_PRODUCT_NAME}' "
		!insertmacro WriteLog "create shortcut with $SMPROGRAMS\${_PRODUCT_NAME}\${_PRODUCT_NAME}.lnk' '$INSTDIR\SpiderDocs.exe' '' '$INSTDIR\icon3d.ico "
	${EndIf}

    #!insertmacro WriteLog "End : Shortcut ${IsDelete} "
!macroend

!macro SendTo Path IsDelete

	!insertmacro WriteLog "SendTo ${Path}, ${IsDelete} -----"

	StrCpy $i 0
	StrCpy $len1 0
	StrCpy $var2 ""

	${List.Count} $len1 SendToFileNames
	IntOp $len1 $len1 - 1

	${ForEach} $i 0 $len1 + 1
		${List.Get} $var2 SendToFileNames $i

		${If} ${IsDelete} == 1
				!insertmacro Del "${Path}\$var2"
				!insertmacro WriteLog "Del '${Path}\$var2'"
		${Else}
				!insertmacro CreateShortCut "${Path}\$var2" "$i"
				!insertmacro WriteLog "CreateShortCut '${Path}\$var2' '$i'"
		${EndIf}
	${Next}

!macroend

!macro CreateShortCut FullPath Index
  StrCpy $var3 ""
  StrCpy $var4 ""

  ${List.Get} $var3 SendToFileArgs ${Index}
  ${List.Get} $var4 SendToFileText ${Index}

  CreateShortCut "${FullPath}" "$INSTDIR\SpiderDocs.exe" \
                  "$var3" "$INSTDIR\icon3d.ico" "0" SW_SHOWMAXIMIZED \
                  "" "$var4"

  !insertmacro WriteLog "CreateShortCut : '${FullPath}' '$INSTDIR\SpiderDocs.exe' '$var3' '$INSTDIR\icon3d.ico' '' SW_SHOWMAXIMIZED '' '$var4' "

!macroend

!macro Del Path
    !insertmacro WriteLog "Del : ${Path} "

    Delete "${Path}"
    #!insertmacro WriteLog "${Path}"

    #!insertmacro WriteLog "End : Del ${Path} "
!macroend

!macro OfficeAddin IsDelete

    ${If} ${RunningX64}

      SetRegView 64
      !insertmacro _OfficeAddin "${IsDelete}"

      SetRegView 32
      !insertmacro _OfficeAddin "${IsDelete}"

      SetRegView 64
    ${Else}

      SetRegView 32
      !insertmacro _OfficeAddin "${IsDelete}"

      SetRegView 64
      !insertmacro _OfficeAddin "${IsDelete}"

      SetRegView 32

    ${EndIf}
!macroend

!macro _OfficeAddin IsDelete
	StrCpy $j 0
	StrCpy $i 0
	StrCpy $k 0
  	StrCpy $var ""
	StrCpy $var1 ""
	StrCpy $var2 ""
	StrCpy $var3 ""
	StrCpy $var4 ""
	StrCpy $var5 ""

  !insertmacro WriteLog "_OfficeAddin : ${IsDelete} ----- "

	# Remove reg for all users but max 100
	${For} $k 0 100
		EnumRegKey $var4 HKLM "SOFTWARE\Microsoft\Windows NT\CurrentVersion\ProfileList" $k
		${If} $var4 == ""
		  	!insertmacro WriteLog "Exit For because not registory at SOFTWARE\Microsoft\Windows NT\CurrentVersion\ProfileList, $var4 "
			${ExitFor}
		${EndIf}

		${registry::KeyExists} "$_HKU\$var4" $var
		!insertmacro WriteLog "KeyExists, $_HKU\$var4"

		#Do if key exists
		${If} $var == 0

			AccessControl::GrantOnRegKey HKU "$var4\Software\Policies" "Everyone" "FullAccess"

			${List.Count} $len1 ListOfficeVersion
			IntOp $len1 $len1 - 1

			${ForEach} $i 0 $len1 + 1

				${List.Count} $len2 ListApp # return should be always same to ListAddin
				IntOp $len2 $len2 - 1

				${ForEach} $j 0 $len2 + 1
					${List.Get} $var1 ListOfficeVersion $i
					${List.Get} $var2 ListApp $j
					${List.Get} $var3 ListAddin $j

					${If} ${IsDelete} == 1

            # HKCU is a just alias to HKU. All users is in HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\ProfileList
            #!insertmacro RmRegVal "HKCU" "Software\Microsoft\Office\$var1\$var2\Resiliency\DoNotDisableAddinList" "SpiderDocs.$var3"

            # Get All users but max 100 user
            !insertmacro RmRegVal $_HKU "$var4\Software\Microsoft\Office\$var1\$var2\Resiliency\DoNotDisableAddinList" "SpiderDocs.$var3"
            !insertmacro RmRegVal $_HKU "$var4\Software\Policies\Microsoft\Office\$var1\$var2\Resiliency\AddinList" "SpiderDocs.$var3"

					${Else}

            !insertmacro WrRegVal $_HKU "$var4\Software\Policies\Microsoft\Office\$var1\$var2\Resiliency\AddinList" "SpiderDocs.$var3"  1 "REG_DWORD"

            !insertmacro RmReg $_HKLM "$var4\Software\Policies\Microsoft\Office\$var1\$var2\Resiliency\DisabledItems"
            #!insertmacro WrReg $_HKLM "$var4\Software\Policies\Microsoft\Office\$var1\$var2\Resiliency" "DisabledItems"

            !insertmacro RmReg $_HKLM "$var4\Software\Policies\Microsoft\Office\$var1\$var2\Resiliency\CrashingAddinList"
            ##!insertmacro WrReg $_HKLM "$var4\Software\Policies\Microsoft\Office\$var1\$var2\Resiliency" "CrashingAddinList"

            !insertmacro WrRegVal $_HKU "$var4\Software\Microsoft\Office\$var1\$var2\Resiliency\DoNotDisableAddinList" "SpiderDocs.$var3"  1 "REG_DWORD"

					${EndIf}
				${Next}

			${Next}

		${EndIf}

	${Next}

    # install/uninstall adding
    ${List.Count} $len2 ListApp # return should be always same to ListAddin
    IntOp $len2 $len2 - 1

    ${ForEach} $j 0 $len2 + 1
        ${List.Get} $var2 ListApp $j
        ${List.Get} $var3 ListAddin $j
        ${List.Get} $var5 ListAddinDescription $j

        ${If} ${IsDelete} == 1
            !insertmacro RmReg $_HKLM "Software\Microsoft\Office\$var2\Addins\SpiderDocs.$var3"
        ${Else}
          #!insertmacro RmRegVal $_HKLM "Software\Microsoft\Office\$var2\Addins\" "SpiderDocs.$var3" "1"

          !insertmacro WrRegVal $_HKLM "Software\Microsoft\Office\$var2\Addins\SpiderDocs.$var3"  "Description" "$var5" "REG_SZ"
          !insertmacro WrRegVal $_HKLM "Software\Microsoft\Office\$var2\Addins\SpiderDocs.$var3"  "FriendlyName" "$var5" "REG_SZ"
          !insertmacro WrRegVal $_HKLM "Software\Microsoft\Office\$var2\Addins\SpiderDocs.$var3"  "LoadBehavior" 3 "REG_DWORD"
          !insertmacro WrRegVal $_HKLM "Software\Microsoft\Office\$var2\Addins\SpiderDocs.$var3"  "Manifest" "file:///$INSTDIR\..\$ADDIN_DIR_NAME\$var3.vsto|vstolocal" "REG_SZ"

        ${EndIf}
    ${Next}

	;# Remove reg for all users but max 100
	;${For} $k 0 100
	;	EnumRegKey $var4 HKLM "SOFTWARE\Microsoft\Windows NT\CurrentVersion\ProfileList" $k
    ;    !insertmacro WriteLog "ProfileList $var4"
	;
	;	${If} $var4 == ""
	;		${ExitFor}
	;	${EndIf}
    ;
    ;    AccessControl::GrantOnRegKey HKU "Software\Policies" "Everyone" "FullAccess"
    ;
	;${Next}

    AccessControl::GrantOnRegKey HKCU "Software\Policies" "Everyone" "FullAccess"
!macroend
/*
!macro RegRoot Root
  ${If} ${Root} == "HKCR"
    StrCpy $0 "HKEY_CLASSES_ROOT"
  ${ElseIf} ${Root} == "HKCU"
    StrCpy $0 "HKEY_CURRENT_USER"
  ${ElseIf} ${Root} == $_HKLM
    StrCpy $0 "HKEY_LOCAL_MACHINE"
  ${ElseIf} ${Root} == $_HKU
    StrCpy $0 "HKEY_USERS"
  ${ElseIf} ${Root} == "HKPD"
    StrCpy $0 "HKEY_PERFORMANCE_DATA"
  ${ElseIf} ${Root} == "HKCC"
    StrCpy $0 "HKEY_CURRENT_CONFIG"
  ${ElseIf} ${Root} == "HKDD"
    StrCpy $0 "HKEY_DYN_DATA"
  ${Else}
    StrCpy $0 ${Root}
  ${EndIf}
!macroend
*/
!macro WrRegVal Root SubKey Name Value Type
    #!insertmacro RegRoot ${Root} ;// will return $0

  	#StrCpy $var ""

    !insertmacro WriteLog "WrRegVal : '${Root}' '${SubKey}' '${Name}' '${Value}' '${Type}'"

#	${registry::KeyExists} "${Root}\${SubKey}" $1
#
#	#Do if key exists
#	${If} $1 == -1
#		#if subkey is not eixt, just create it
#		${registry::CreateKey} "${Root}\${SubKey}" $1
#
#		${If} $1 == 1
#			!insertmacro WriteLog "${Root}\${SubKey} Already exists"
#		${EndIf}
#
#		${If} $1 == -1
#			!insertmacro WriteLog "${Root}\${SubKey} ERROR"
#		${EndIf}
#
#		${If} $1 == 0
#			!insertmacro WriteLog "${Root}\${SubKey} Successfully created"
#		${EndIf}
#	${EndIf}

    !insertmacro WrReg "${Root}" "${SubKey}"

    ;${If} ${RunningX64}
    ;  SetRegView 32
    ;${Else}
    ;${EndIf}

    ${If} ${Type} == "REG_DWORD"

	  ${registry::Write} "${Root}\${SubKey}" "${Name}" "${Value}" "${Type}" $1
	  !insertmacro WriteLog "Write $1"

    ${ElseIf} ${Type} == "REG_SZ"

      ${registry::Write} "${Root}\${SubKey}" "${Name}" "${Value}"  "${Type}" $1
      !insertmacro WriteLog "Write2 $1"

    ${EndIf}

    #${registry::WriteExtra} "[fullpath]" "[value]" "[string]" $var
!macroend

!macro WrReg Root SubKey
  !insertmacro WriteLog "WrReg : '${Root}' '${SubKey}'"

  ${registry::KeyExists} "${Root}\${SubKey}" $1

	#Do if key exists
	${If} $1 == -1
		#if subkey is not eixt, just create it
		${registry::CreateKey} "${Root}\${SubKey}" $1

		${If} $1 == 1
			!insertmacro WriteLog "${Root}\${SubKey} Already exists"
		${EndIf}

		${If} $1 == -1
			!insertmacro WriteLog "${Root}\${SubKey} ERROR"
		${EndIf}

		${If} $1 == 0
			!insertmacro WriteLog "${Root}\${SubKey} Successfully created"
		${EndIf}
	${EndIf}

!macroend

!macro MainSpiderDocs IsDelete

  StrCpy $var1 ""

  ${If} ${IsDelete} == 1
      !insertmacro RmRegVal $_HKLM "Software\SpiderDocs" "SpiderDocsPath"
      !insertmacro RmRegVal $_HKLM "Software\SpiderDocs" "Version"
  ${Else}

    #${registry::KeyExists} "HKEY_LOCAL_MACHINE\Software\SpiderDocs\SpiderDocsPath" $var1

    !insertmacro WrRegVal $_HKLM "Software\SpiderDocs" "SpiderDocsPath" "$INSTDIR\${_PRODUCT_NAME}.exe" "REG_SZ"
    !insertmacro WrRegVal $_HKLM "Software\SpiderDocs" "Version" "${_VERSION}" "REG_SZ"

  ${EndIf}

!macroend


!macro RmRegVal Root Key ValueName
    !insertmacro WriteLog "RmRegVal : ${Root} ${Key} ${ValueName}"

    ;${If} ${RunningX64}
    ;  SetRegView 32
    ;${Else}
    ;${EndIf}
    #DeleteRegValue "${Root}" "${Key}" "${ValueName}"
    ${registry::DeleteValue} "${Root}\${Key}" "${ValueName}" $var

    #SetRegView 64
    #DeleteRegValue "${Root}" "${Key}" "${ValueName}"

    #!insertmacro WriteLog "End : RmReg ${Root} ${Key} "

!macroend

!macro RmReg Root Key
    !insertmacro WriteLog "RmReg : ${Root} ${Key} "

;    ${If} ${RunningX64}
;      SetRegView 32
;    ${Else}
    ;${EndIf}

    #DeleteRegKey "${Root}" "${Key}"
    ${registry::DeleteKey} "${Root}\${Key}" $var
    #SetRegView 64
    #DeleteRegKey "${Root}" "${Key}"

    #!insertmacro WriteLog "End : RmReg ${Root} ${Key} "

!macroend

/*
!macro WrReg Root SubKey Entry Value
    !insertmacro WriteLog "WrReg : '${Root}' '${SubKey}' '${Entry}' '${Value}' "
    ${If} ${RunningX64}
      SetRegView 32
    ${Else}
    ${EndIf}

    WriteRegStr  "${Root}" "${SubKey}" "${Entry}" "${Value}"

    #SetRegView 64
    #WriteRegStr  "${Root}" "${SubKey}" "${Entry}" "${Value}"

    #!insertmacro WriteLog "End : WrReg ${Root} ${SubKey} ${Entry} ${Value} "
!macroend
*/
!macro RemoveService Name

  !insertmacro StopService ${Name}

  SimpleSC::RemoveService "${Name}"
  !insertmacro WriteLog "RemoveService : '${Name}' "

    ##!insertmacro WriteLog "End : RemoveService ${Name} "

!macroend

!macro StopService Name
    !insertmacro WriteLog "StopService : '${Name}' "

    SimpleSC::StopService "${Name}" 1 60

    #!insertmacro WriteLog "End : StopService ${Name} "
!macroend

/* -------------------------------------------------------------
  service_type - One of the following codes
    1 - SERVICE_KERNEL_DRIVER - Driver service.
    2 - SERVICE_FILE_SYSTEM_DRIVER - File system driver service.
    16 - SERVICE_WIN32_OWN_PROCESS - Service that runs in its own process. (Should be used in most cases)
    32 - SERVICE_WIN32_SHARE_PROCESS - Service that shares a process with one or more other services.
    256 - SERVICE_INTERACTIVE_PROCESS - The service can interact with the desktop.

  start_type - one of the following codes
    0 - SERVICE_BOOT_START - Driver boot stage start
    1 - SERVICE_SYSTEM_START - Driver scm stage start
    2 - SERVICE_AUTO_START - Service auto start (Should be used in most cases)
    3 - SERVICE_DEMAND_START - Driver/service manual start
*/
!macro InstallService Name
  !insertmacro RemoveService "${Name}"

  !insertmacro WriteLog "InstallService : '${Name}' , $INSTDIR\$SERVICE_AUTOUPDATE_EXE"

  # Install a service - ServiceType own process - StartType automatic - NoDependencies - Logon as System Account
  #SimpleSC::InstallService "Spider Docs Auto Update" "Spider Docs Auto Update" 16 2 "C:\Program Files (x86)\Spider Docs\AutoUpdateService.exe" "" "" ""
  SimpleSC::InstallService "${Name}" "${Name}" 16 2 "$INSTDIR\$SERVICE_AUTOUPDATE_EXE" "" "" ""
  SimpleSC::SetServiceDelayedAutoStartInfo "${Name}" "1" #Delayed start

!macroend

!macro StasrtService Name
  !insertmacro WriteLog "StartService : '${Name}' "

  SimpleSC::StartService "${Name}" "" 60

!macroend

!macro uninstall_sheildinstaller cmd

    ${If} ${RunningX64}
		SetRegView 64
		!insertmacro _uninstall_sheildinstaller "${cmd}"
		SetRegView 32
		!insertmacro _uninstall_sheildinstaller "${cmd}"

      	SetRegView 64
    ${Else}
		SetRegView 32
		!insertmacro _uninstall_sheildinstaller "${cmd}"
		SetRegView 64
		!insertmacro _uninstall_sheildinstaller "${cmd}"

      	SetRegView 32
    ${EndIf}

!macroend
;;-----------------------------------------------------
;;	Uninstall Install section
;;	see more detail how to find uninstaller for InstallSheild
;;	http://stackoverflow.com/questions/5063129/how-to-find-the-upgrade-code-productcode-of-an-installed-application-in-win-7
;; 	http://www.silentinstall.org/msiexec
;;	http://stackoverflow.com/questions/30067976/programatically-uninstall-a-software-using-c-sharp
; Result will be returned to PRODUCT_CODE variable
!macro _uninstall_sheildinstaller cmd
		StrCpy $k 0
		StrCpy $var1 ""
		StrCpy $var2 ""
		# Remove reg for all users but max 100
		!insertmacro WriteLog "----- _uninstall_sheildinstaller -----"

		${For} $k 0 10000
			EnumRegKey $var1 HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall" $k

			#!insertmacro WriteLog "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall $k $var1"

			${If} $var1 == ""
				!insertmacro WriteLog "Exit For because not registory at SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall, $var1 $k"
				${ExitFor}
			${EndIf}

			#${registry::Read} "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$var1" "DisplayName" $R0 $R1
			#!insertmacro WriteLog "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$var1 DisplayName $R0"

			# If Product Code is Product Name, It means NSIS Version so skip
			${If} $var1 != "${_PRODUCT_NAME}"

				StrCpy $PRODUCT_CODE $var1
				${registry::Read} "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$PRODUCT_CODE" "DisplayName" $R0 $R1
				#ReadRegStr $var2 HKLM "HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Print\Printers\SomePrinter" "Name"

				${If} $R0 == "${_PRODUCT_NAME}"
          			!insertmacro WriteLog "previous uninstall information is found at HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$PRODUCT_CODE"

					${registry::Read} "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$PRODUCT_CODE" "UninstallString" $R0 $R1

                    ${If} "${cmd}" == "run"
                        !insertmacro WriteLog "exec command '$R0 /quiet /qn'"

                        #ExecWait "$R0 /quiet /qn" $0
                        !insertmacro Exec "$R0" "/quiet /qn"
                    ${EndIf}

          			!insertmacro RmReg $_HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$PRODUCT_CODE"

          			${ExitFor}
				${EndIf}
			${EndIf}

			#IntOp $k $k + 1
		${Next}

  !insertmacro WriteLog "----- _uninstall_sheildinstaller"
!macroend

!macro create_dirs
	!insertmacro WriteLog "CreateDirectory $INSTDIR "
	!insertmacro WriteLog "CreateDirectory $INSTDIR\..\$ADDIN_DIR_NAME "
	!insertmacro WriteLog "$WINDIR\${_PRODUCT_NAME} "

	CreateDirectory "$INSTDIR"
	CreateDirectory "$INSTDIR\..\$ADDIN_DIR_NAME"
	CreateDirectory "$WINDIR\${_PRODUCT_NAME}"

  !insertmacro remove_directory "$INSTDIR\logs"
  !insertmacro remove_directory "$INSTDIR\..\$ADDIN_DIR_NAME\logs"

  # logs with full permission
  CreateDirectory "$INSTDIR\logs"
  CreateDirectory "$INSTDIR\..\$ADDIN_DIR_NAME\logs"

  AccessControl::GrantOnFile "$INSTDIR\logs" "(BU)" "FullAccess"
  AccessControl::GrantOnFile "$INSTDIR\..\$ADDIN_DIR_NAME\logs" "(BU)" "FullAccess"

  !insertmacro WriteLog "CreateDirectory $INSTDIR\logs "
	!insertmacro WriteLog "CreateDirectory $INSTDIR\..\$ADDIN_DIR_NAME\logs "

!macroend

!macro copies_all_dirs
  #StrCpy $INSTDIR "$PROG_PATH\Spider Docs"

    !insertmacro WriteLog "copies_all_dirs : SetOutPath '$INSTDIR' from '${_PRODUCT_NAME}\' "
    !insertmacro WriteLog "copies_all_dirs : SetOutPath '$INSTDIR\..\$ADDIN_DIR_NAME' from '$ADDIN_DIR_NAME' "

    SetOutPath "$INSTDIR"
    File /nonfatal /a /r "${_PRODUCT_NAME}\"

    SetOutPath "$INSTDIR\..\$ADDIN_DIR_NAME"
    File /nonfatal /a /r "Spider Docs AddIns\"

    SetOutPath "$WINDIR\${_PRODUCT_NAME}\"
    File /r "OCR"
    !insertmacro WriteLog "copies_all_dirs : SetOutPath '$WINDIR\${_PRODUCT_NAME}\' from 'OCR' "



    /*
  SetOutPath "$WINDIR\${_PRODUCT_NAME}\"
  File /nonfatal /a /r "SpiderDocsInstaller.exe"
  File /r "OCR"
  */
!macroend

!macro settingJson
	!insertmacro WriteLog " BEGIN settingJson"

    ${If} ${FileExists} "$INSTDIR\setting.json"

        !insertmacro WriteLog "setting.json is found"

    ${Else}

        !insertmacro WriteLog "setting.json is not found"

        #create file
        FileOpen $0 "$INSTDIR\setting.json" w
        FileClose $0

        # Get information json file contains
        ${registry::Read} "HKEY_LOCAL_MACHINE\SOFTWARE\SpiderDocs" "SpiderDocsPath" $R0 $R1

		${StrReplace} "$R0" "\" "\\"

        ; Create and Output Json file
        nsJSON::Set /value "{}"
        nsJSON::Set "SpiderDocsPath" /value `"$0"`
        nsJSON::Set "UpdateServer" /value `"_noset_"`  ; update server cannot retrieve
        nsJSON::Set "UpdateServerPort" /value "5322" ; update server cannot retrieve

        nsJSON::Serialize /format /file "$INSTDIR\setting.json"

    ${EndIf}

    # grant full control permission
    #AccessControl::GrantOnFile "$INSTDIR\setting.json" "Everyone" "FullAccess"
    AccessControl::GrantOnFile "$INSTDIR\setting.json" "(BU)" "GenericRead + GenericExecute + GenericWrite + Delete"
    ;Pop $0

!macroend

!macro remove_all_dirs
	!insertmacro remove_directory "$WINDIR\${_PRODUCT_NAME}"
	#!insertmacro remove_directory "$INSTDIR"
	!insertmacro remove_directory "$INSTDIR\..\$ADDIN_DIR_NAME"
	!insertmacro remove_directory "$SMPROGRAMS\${_PRODUCT_NAME}"

	Push "$INSTDIR"
	Push "setting.json"
	Call un.RmFilesButOne
!macroend

#
# Add/Remove regisotory
#
!macro product_registory IsDelete
  StrCpy $var1 ""
  ${If} ${IsDelete} == 1
    !insertmacro RmReg $_HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${_PRODUCT_NAME}"
  ${Else}
    StrCpy $var1 "Software\Microsoft\Windows\CurrentVersion\Uninstall\${_PRODUCT_NAME}"

    !insertmacro WrRegVal $_HKLM "$var1" "DisplayName" "${_PRODUCT_NAME}" "REG_SZ"
    !insertmacro WrRegVal $_HKLM "$var1" "UninstallString" "$INSTDIR\uninstall.exe"  "REG_SZ"
    !insertmacro WrRegVal $_HKLM "$var1" "DisplayIcon" "$INSTDIR\icon3d.ico" "REG_SZ"
    !insertmacro WrRegVal $_HKLM "$var1" "DisplayVersion" "${_VERSION}" "REG_SZ"
    !insertmacro WrRegVal $_HKLM "$var1" "Publisher" "${_COMPANY_NAME}" "REG_SZ"
    !insertmacro WrRegVal $_HKLM "$var1" "EstimatedSize" "${_INSTALLSIZE}" "REG_DWORD"
    !insertmacro WrRegVal $_HKLM "$var1" "NoModify" 1 "REG_DWORD"
    !insertmacro WrRegVal $_HKLM "$var1" "NoRepair" 1 "REG_DWORD"
  ${EndIf}


 ${If} ${IsDelete} == 1

    !insertmacro RmReg $_HKCR ".dms"
    !insertmacro RmReg $_HKCR "dms_auto_file"
    !insertmacro RmReg $_HKCU "Software\Classes\Applications\SpiderDocs.exe"

  ${Else}

    #StrCpy $var1 "dms_auto_file"

    !insertmacro WrRegVal $_HKCR ".dms" "" "dms_auto_file" "REG_SZ"

    !insertmacro WrRegVal $_HKCR "dms_auto_file" "" "DMS File"  "REG_SZ"
    !insertmacro WrRegVal $_HKCR "dms_auto_file\DefaultIcon" "" "$INSTDIR\icon3d.ico" "REG_SZ"




    !insertmacro WrReg $_HKCR "dms_auto_file\OpenWithList"

    !insertmacro WrReg $_HKCR "dms_auto_file\OpenWithList\SpiderDocs.exe"
    !insertmacro WrRegVal $_HKCR "dms_auto_file\shell" "" "open" "REG_SZ"
    !insertmacro WrReg $_HKCR "dms_auto_file\shell\open"
    !insertmacro WrRegVal $_HKCR "dms_auto_file\shell\open\command" "" "$INSTDIR\SpiderDocs.exe dmsfile %1" "REG_SZ"

    !insertmacro WrRegVal $_HKCU "Software\Classes\Applications\SpiderDocs.exe\shell\open\command" "" "$INSTDIR\SpiderDocs.exe dmsfile %1" "REG_SZ"
  ${EndIf}

!macroend

!macro remove_directory Path
  RMDir /r "${Path}"
	!insertmacro WriteLog "RMdir : /r ${Path} "
!macroend

!macro install_ghostscript

  SetOutPath "$INSTDIR\Prerequisites"

  File /nonfatal /a "Prerequisites\gs952w64.exe"

  ; ${If} ${FileExists} "C:\gs\uninstgs.exe"
  ;   !insertmacro Exec "C:\gs\uninstgs.exe" "/S"

  ;   Sleep 3000
  ; ${EndIf}

  !insertmacro Exec "$INSTDIR\Prerequisites\gs952w64.exe" "/S /D=C:\gs"

!macroend

!macro install_cplusplus
  ; https://support.microsoft.com/en-us/help/2977003/the-latest-supported-visual-c-downloads

  StrCpy $var1 ""

  SetOutPath "$INSTDIR\Prerequisites"

  ${If} ${RunningX64}
      # 64 bit code
      StrCpy $var1 "x64"
  ${Else}
      # 32 bit code
      StrCpy $var1 "x86"
  ${EndIf}

  File /nonfatal /a "Prerequisites\VC_redist.x64.exe"
  File /nonfatal /a "Prerequisites\VC_redist.x86.exe"

  !insertmacro Exec "$INSTDIR\Prerequisites\VC_redist.$var1.exe" "/install /quiet /norestart "

!macroend

; !macro install_vcredistributable

;   SetOutPath "$INSTDIR\Prerequisites"

;   File /nonfatal /a "Prerequisites\VC_redist.x64.exe"

;   !insertmacro Exec "$INSTDIR\Prerequisites\VC_redist.x64.exe" "/quite"

; !macroend

!macro call_uninstaller
  StrCpy $i 0

  #If the file is an existance then silently kick
  ${If} ${FileExists} "$INSTDIR\uninstall.exe"
    !insertmacro Exec "$INSTDIR\uninstall.exe" "/S"

    #When put the /S param, ExecWait will finish immediately, so while for 2 min or exit if uninstall is finish
    ${ForEach} $i 0 120 + 1
      ${If} ${FileExists} "$INSTDIR\uninstall.exe"
        Sleep 1000
      ${Else}
        !insertmacro WriteLog "Uninstall Timeout"
        ${ExitFor}
      ${EndIf}
    ${Next}

  ${Else}

  ${EndIf}

  !insertmacro WriteLog "Uninstall : Time:$i"
!macroend

!macro Exec Path Params
  ExecWait "${Path} ${Params}" $0
  !insertmacro WriteLog "Exec : '${Path} ${Params}'  , return code is $0"
!macroend

!macro InstallPrerequisites

  ;!insertmacro install_vcredistributable

  !insertmacro install_cplusplus

  !insertmacro install_ghostscript

  ;!insertmacro WriteLog "InstallPrerequisites : SetOutPath '$INSTDIR\Prerequisites' from '.\Prerequisites\' "

  ;SetOutPath "$INSTDIR\Prerequisites"
  ;File /nonfatal /a /r ".\Prerequisites\"

  ;
  ; Install if you donot have
  ;

  # 2.0
  ;!insertmacro netframework 20

  # 3.5
  #!insertmacro netframework 35

  # 4.5.2
  !insertmacro netframework 452
!macroend


!macro netframework Version
  StrCpy $var1 ""

  !insertmacro WriteLog "netframework ${Version}"

  System::Call DotNetChecker::GetDotNet${Version}ServicePack
  Pop $0

  !insertmacro WriteLog "Framework Check ${Version} : return value is $0"

  ${If} $0 <= 0
    ${If} ${RunningX64}
        # 64 bit code
        StrCpy $var1 "x64" ;
    ${Else}
        # 32 bit code
        StrCpy $var1 "x86"
    ${EndIf}

    ; 452 installer is used both bit machine
    ${If} ${Version} == 452
      StrCpy $var1 "x64-x86"
    ${EndIf}

    !insertmacro WriteLog "Install Framework $INSTDIR\Prerequisites\net-framework_${Version}-$var1.exe"

    !insertmacro CheckNetFramework ${Version}

    ; Install Framework
    ;${If} $0 == -2
      ;!insertmacro WriteLog "Install Framework $INSTDIR\Prerequisites\net-framework_${Version}-$var1.exe"

      ;!insertmacro CheckNetFramework ${Version}

      ;CreateDirectory "$INSTDIR\Prerequisites"

      ;NSISdl::download "http://spiderdocs.spiderdevelopments.com.au:5321/updates/prerequisites/net-framework_${Version}-$var1.exe" "$INSTDIR\Prerequisites\net-framework_${Version}-$var1.exe"

      ;!insertmacro Exec "$INSTDIR\Prerequisites\net-framework_${Version}-$var1.exe" ""

    ;${EndIf}

    ;!insertmacro CheckNetFramework ${Version}
  ${EndIf}

!macroend

; refresh icons. the behaviour is same like pressing F5
Function RefreshShellIcons
  !define SHCNE_ASSOCCHANGED 0x08000000
  !define SHCNF_IDLIST 0
  System::Call 'shell32.dll::SHChangeNotify(i, i, i, i) v (${SHCNE_ASSOCCHANGED}, ${SHCNF_IDLIST}, 0, 0)'
FunctionEnd

; This function will delete all files in a directory except one file.
; http://nsis.sourceforge.net/Delete_dirs_/_files_in_a_directory_except_one_dir_/_file
Function un.RmFilesButOne
 Exch $R0 ; exclude file
 Exch
 Exch $R1 ; route dir
 Push $R2
 Push $R3

  FindFirst $R3 $R2 "$R1\*.*"
  IfErrors Exit

  Top:
   StrCmp $R2 "." Next
   StrCmp $R2 ".." Next
   StrCmp $R2 $R0 Next
   IfFileExists "$R1\$R2\*.*" Next
    Delete "$R1\$R2"

   #Goto Exit ;uncomment this to stop it being recursive (delete only one file)

   Next:
    ClearErrors
    FindNext $R3 $R2
    IfErrors Exit
   Goto Top

  Exit:
  FindClose $R3

 Pop $R3
 Pop $R2
 Pop $R1
 Pop $R0
FunctionEnd



;
; Replce
;
Function StrRep
  Exch $R4 ; $R4 = Replacement String
  Exch
  Exch $R3 ; $R3 = String to replace (needle)
  Exch 2
  Exch $R1 ; $R1 = String to do replacement in (haystack)
  Push $R2 ; Replaced haystack
  Push $R5 ; Len (needle)
  Push $R6 ; len (haystack)
  Push $R7 ; Scratch reg
  StrCpy $R2 ""
  StrLen $R5 $R3
  StrLen $R6 $R1
loop:
  StrCpy $R7 $R1 $R5
  StrCmp $R7 $R3 found
  StrCpy $R7 $R1 1 ; - optimization can be removed if U know len needle=1
  StrCpy $R2 "$R2$R7"
  StrCpy $R1 $R1 $R6 1
  StrCmp $R1 "" done loop
found:
  StrCpy $R2 "$R2$R4"
  StrCpy $R1 $R1 $R6 $R5
  StrCmp $R1 "" done loop
done:
  StrCpy $R3 $R2
  Pop $R7
  Pop $R6
  Pop $R5
  Pop $R2
  Pop $R1
  Pop $R4
  Exch $R3
FunctionEnd

!macro _StrReplaceConstructor ORIGINAL_STRING TO_REPLACE REPLACE_BY
  Push "${ORIGINAL_STRING}"
  Push "${TO_REPLACE}"
  Push "${REPLACE_BY}"
  Call StrRep
  Pop $0
!macroend


!define StrReplace '!insertmacro "_StrReplaceConstructor"'