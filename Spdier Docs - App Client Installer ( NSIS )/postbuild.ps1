# Fistof all
# https://stackoverflow.com/questions/6500320/post-build-event-execute-powershell
# powershell -windowstyle hidden -ExecutionPolicy ByPass -File "$(SolutionDir)..\Spdier Docs - App Client Installer ( NSIS )\postbuild.ps1" $(ConfigurationName)
# What includes
# Install Spider Docs Directory
$spiders_dir = "C:\Dev\SpiderDocs"
$out_spider_dir = $PSScriptRoot+"\Spider Docs\"
$out_addin_dir = $PSScriptRoot+"\Spider Docs AddIns\"
$nsi_file = $PSScriptRoot+"\SpiderDocs.nsi"
$compiler = "C:\Program Files (x86)\NSIS\makensis.exe"
#$certificate_file = "C:\Dev\SpiderDocs\SpiderCertificate.pfx"

#
# Replace New Assembly Info
# https://www.snip2code.com/Snippet/118570/Powershell-Script-to-Increment-Build-Num
#
$assemblyInfoPath = $spiders_dir+"\SpiderDocs\Properties\AssemblyInfo.cs"
$sourcensi = $PSScriptRoot+"\_base_.nsi"
$destnsi = $PSScriptRoot+"\SpiderDocs.nsi"

Write-Host ("assemblyInfoPath: " +$assemblyInfoPath)

Remove-Item $destnsi -Force

$contents = [System.IO.File]::ReadAllText($assemblyInfoPath)
$dest = [System.IO.File]::ReadAllText($sourcensi)

$versionString = [RegEx]::Match($contents,"AssemblyVersion\(\""(\d+\.\d+\.\d+)").Groups[1]
Write-Host ("AssemblyVersion: " +$versionString)

#update AssemblyVersion and AssemblyVersion, then write to file
Write-Host ("Setting version in assembly info file ")
$dest = [RegEx]::Replace($dest, "####VERSION####", $versionString.ToString())
[System.IO.File]::WriteAllText($destnsi, $dest)

if ( $Args[0] -ne "Release" )
{
    $newzip = "setup_"+$versionString.ToString()+".zip"
    $newname = "setup_"+$versionString.ToString()+".exe"
    if (Test-Path "$PSScriptRoot\$newzip" ) { Remove-Item -Force $PSScriptRoot\$newzip }
    if (Test-Path "$PSScriptRoot\$newname") { Remove-Item -Force $PSScriptRoot\$newname }

    Write-Host ("####### NOT BUILD FOR EXE ########")
    exit
    #exit(1000)
}


#$MyInvocation.MyCommand.Path+
#
# Main
#
$dirs = @(  "\SpiderCommonModules\",
            "\SpiderDocsCommon\DatabaseUtilities\bin\Release\Spider.Data.*",
            "\AutoUpdateService\bin\Release\*",
            "\Spider Docs - Components\Spider Docs - CheckComboBox\bin\Release\*",
            "\SpiderDocs\bin\Release\*",
            "\SpiderDocsCommon\SpiderDocsForm\bin\Release\*",
            #"\SpiderDocsCommon\SpiderDocsModels\bin\Release\*",
            #"\SpiderDocsCommon\SpiderDocsModule\bin\Release\*",
            "\SpiderDocsCommon\SpiderDocsModule\bin\Release\*",
            "\SpiderDocs\Resources\icon3d.ico",
            #"\Updater\bin\Release\*",
            "\UpdateWaitDialog\bin\Release\*"
            #"\Spider Docs - App Client Installer\SpiderDocsInstaller\bin\Release\"
            )

Write-Output "Removed all file : OK"
Remove-Item -Recurse -Force $out_spider_dir

foreach ($dir in $dirs) {
    #Write-Output  -force $spiders_dir$dir  -destination $out_spider_dir
    Copy-Item $spiders_dir$dir  -destination $out_spider_dir -recurse -force
}

Write-Output "Copied all file : OK"

#
# Addin
#
$dirs = @(  "\SpiderCommonModules\",
            "\SpiderDocsCommon\DatabaseUtilities\bin\Release\Spider.Data.*",
            "\Spider Docs - AddInsOffice\Excel\bin\Release\*",
            "\Spider Docs - AddInsOffice\Outlook\bin\Release\*",
            "\Spider Docs - AddInsOffice\OutlookAttachment\bin\Release\*",
            "\Spider Docs - AddInsOffice\PowerPoint\bin\Release\*",
            "\Spider Docs - AddInsOffice\Word\bin\Release\*",
            "\Spider Docs - Components\Spider Docs - CheckComboBox\bin\Release\*",
            "\SpiderDocs\Resources\icon3d.ico",
            "\Spider Docs - AddInsOffice\AddInModules\bin\Release\*",
            "\SpiderDocsCommon\SpiderDocsForm\bin\Release\*",
            #"\SpiderDocsCommon\SpiderDocsModels\bin\Release\*",
            "\SpiderDocsCommon\SpiderDocsModule\bin\Release\*"
            )

Write-Output "Removed all file : OK"
Remove-Item -Recurse -Force $out_addin_dir

foreach ($dir in $dirs) {
    #Write-Output  -force $spiders_dir$dir  -destination $out_addin_dir
    Copy-Item -force $spiders_dir$dir  -destination $out_addin_dir -recurse
}
    Write-Output "Copied all file : OK"

#
# copy certificate
#
#Copy-Item -force $certificate_file  -destination $PSScriptRoot
Copy-Item -force $spiders_dir\SpiderCommonModules\NLog.config -destination $out_spider_dir
Copy-Item -force $spiders_dir\SpiderCommonModules\NLog.config -destination $out_addin_dir
Copy-Item -force $spiders_dir\SpiderCommonModules\TWAINDSM.dll -destination $out_spider_dir
Copy-Item -force $spiders_dir\SpiderCommonModules\TWAINDSM.dll -destination $out_addin_dir

#
# remove all .PDB extentions
#
Remove-Item -Recurse -Force $out_spider_dir\*.pdb
Remove-Item -Recurse -Force $out_spider_dir\*.vshost*
#Remove-Item -Recurse -Force $out_spider_dir\logs
if (Test-Path "$out_spider_dir\logs") { Remove-Item -Recurse -Force $out_spider_dir\logs }
if (Test-Path "$out_spider_dir\setting.json") { Remove-Item -Recurse -Force $out_spider_dir\setting.json }
Remove-Item -Recurse -Force $out_addin_dir\*.pdb
Remove-Item -Recurse -Force $out_addin_dir\*.vshost*


#
# Remove
#
$newzip = "setup_"+$versionString.ToString()+".zip"
$newname = "setup_"+$versionString.ToString()+".exe"
$setup = $PSScriptRoot+"\setup.exe"
$tmp = $PSScriptRoot+"\tmp"
$up = $PSScriptRoot+"\"+$newzip

if (Test-Path $setup ) { Remove-Item -Force -Recurse $setup }
if (Test-Path "$PSScriptRoot\$newzip" ) { Remove-Item -Force $PSScriptRoot\$newzip }
if (Test-Path "$PSScriptRoot\$newname") { Remove-Item -Force $PSScriptRoot\$newname }
if (Test-Path $tmp) { Remove-Item -Force -Recurse $tmp }

#
# Build
#
invoke-expression "& 'C:\Program Files (x86)\NSIS\makensis.exe' 'C:\Dev\SpiderDocs\Spdier Docs - App Client Installer ( NSIS )\SpiderDocs.nsi'"

$w = 1
Do {

    Start-Sleep -s 1
     Write-Output "########################## Waiting ~"
    #
    # Fianl treat
    #


    if (Test-Path $setup)
    {

        New-Item -force -ItemType directory -Path $tmp
        Copy-Item -force $setup  -destination $tmp

        Add-Type -Assembly "System.IO.Compression.FileSystem"
        [io.compression.zipfile]::CreateFromDirectory($tmp, $up)

        Rename-Item -Force -Path $PSScriptRoot\setup.exe -NewName $newname
        exit
    }

 } While ($w -le 1)
