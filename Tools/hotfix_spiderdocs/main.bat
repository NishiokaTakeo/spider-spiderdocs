@echo off

set THEME_REGKEY=HKLM\Software\SpiderDocs
set THEME_REGVAL=SpiderDocsPath

REM Check for presence of key first.
reg query %THEME_REGKEY% /v %THEME_REGVAL% 2>nul || (echo No theme name present! & exit /b 1)

REM query the value. pipe it through findstr in order to find the matching line that has the value. only grab token 3 and the remainder of the line. %%b is what we are interested in here.
set THEME_NAME=
for /f "tokens=2,*" %%a in ('reg query %THEME_REGKEY% /v %THEME_REGVAL% ^| findstr %THEME_REGVAL%') do (
    set THEME_NAME=%%b
)

REM Possibly no value set
if not defined THEME_NAME (echo No theme name present! & exit /b 1)
echo %THEME_NAME%
REM replace any spaces with +
rem set THEME_NAME=%THEME_NAME: =+%

REM open up the default browser, searching google for the theme name
rem echo %THEME_NAME%

rem SET mypath=%THEME_NAME%
rem SET Install_dir=%mypath:~0,-15%


SET exedir=%THEME_NAME:~0,-14%
echo %exedir%


echo sc stop "Spider Docs Auto Update" 
sc stop "Spider Docs Auto Update"

echo copy %~dp0AutoUpdateService.exe "%exedir%"
copy %~dp0AutoUpdateService.exe "%exedir%"
  
echo copy %~dp0Updater.exe "%exedir%"
copy %~dp0Updater.exe "%exedir%"  

sc start "Spider Docs Auto Update"

rem echo %cd%
rem echo %~dp0
rem pause 
