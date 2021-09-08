# 23/08/2017
# powershell -ExecutionPolicy ByPass -File "C:\Dev\SpiderDocs\Spdier Docs - App Client Installer ( NSIS )\init-spiderdocs.ps1"
# powershell -ExecutionPolicy ByPass -File "C:\Users\spider2\Desktop\init-spiderdocs.ps1"
#  Start-Process powershell -Verb runAs
#


$ErrorActionPreference= 'silentlycontinue'


function Get-Key
(
[string]$ParentKey,
[string]$KeyName)
{
    for ($i = 0; $i -lt $KeyName.Split("\").Count; $i++)
    {
        try
        {
            $Key = get-item -Path ("{0}:\{1}" -f $ParentKey,($KeyName.split("\")[0..$i] -join "\"))
        }
        catch 
        {
            $Key = new-item -Path ("{0}:\{1}" -f $ParentKey,($KeyName.split("\")[0..$i] -join "\"))
        }
    }
    return $Key
}

$OutlookVersion = (Get-ItemProperty HKLM:\SOFTWARE\Classes\Outlook.Application\CurVer)."(default)".Replace("Outlook.Application.", "")
$WordVersion = (Get-ItemProperty HKLM:\SOFTWARE\Classes\Word.Application\CurVer)."(default)".Replace("Word.Application.", "")
$ExcelVersion = (Get-ItemProperty HKLM:\SOFTWARE\Classes\Excel.Application\CurVer)."(default)".Replace("Excel.Application.", "")
$PowerPointVersion = (Get-ItemProperty HKLM:\SOFTWARE\Classes\PowerPoint.Application\CurVer)."(default)".Replace("PowerPoint.Application.", "")

#
# Force enable
#
Get-Key -ParentKey HKCU -KeyName "Software\Policies\Microsoft\Office\$OutlookVersion.0\Outlook\Resiliency\AddinList" | Remove-Item
Get-Key -ParentKey HKCU -KeyName "Software\Policies\Microsoft\Office\$ExcelVersion.0\Excel\Resiliency\AddinList" | Remove-Item
Get-Key -ParentKey HKCU -KeyName "Software\Policies\Microsoft\Office\$PowerPointVersion.0\PowerPoint\Resiliency\AddinList" | Remove-Item
Get-Key -ParentKey HKCU -KeyName "Software\Policies\Microsoft\Office\$WordVersion.0\Word\Resiliency\AddinList" | Remove-Item



Get-Key -ParentKey HKCU -KeyName "Software\Microsoft\Office\$OutlookVersion.0\Outlook\Resiliency\DisabledItems" | Remove-Item
Get-Key -ParentKey HKCU -KeyName "Software\Microsoft\Office\$OutlookVersion.0\Outlook\Resiliency\CrashingAddinList" | Remove-Item

Get-Key -ParentKey HKCU -KeyName "Software\Microsoft\Office\$ExcelVersion.0\Excel\Resiliency\DisabledItems" | Remove-Item
Get-Key -ParentKey HKCU -KeyName "Software\Microsoft\Office\$ExcelVersion.0\Excel\Resiliency\CrashingAddinList" | Remove-Item

Get-Key -ParentKey HKCU -KeyName "Software\Microsoft\Office\$PowerPointVersion.0\PowerPoint\Resiliency\DisabledItems" | Remove-Item
Get-Key -ParentKey HKCU -KeyName "Software\Microsoft\Office\$PowerPointVersion.0\PowerPoint\Resiliency\CrashingAddinList" | Remove-Item

Get-Key -ParentKey HKCU -KeyName "Software\Microsoft\Office\$WordVersion.0\Word\Resiliency\DisabledItems" | Remove-Item
Get-Key -ParentKey HKCU -KeyName "Software\Microsoft\Office\$WordVersion.0\Word\Resiliency\CrashingAddinList" | Remove-Item

#
# DoNotDisableAddinList
#
remove-ItemProperty -Path  "HKCU:\Software\Microsoft\Office\$OutlookVersion.0\Outlook\Resiliency\DoNotDisableAddinList" -Name "SpiderDocs.AddInOutlook2013"
remove-ItemProperty -Path  "HKCU:\Software\Microsoft\Office\$OutlookVersion.0\Outlook\Resiliency\DoNotDisableAddinList" -Name "SpiderDocs.AddInOutlookAttachment2013"
remove-ItemProperty -Path  "HKCU:\Software\Microsoft\Office\$ExcelVersion.0\Excel\Resiliency\DoNotDisableAddinList" -Name "SpiderDocs.AddInExcel2013"
remove-ItemProperty -Path  "HKCU:\Software\Microsoft\Office\$PowerPointVersion.0\PowerPoint\Resiliency\DoNotDisableAddinList" -Name "SpiderDocs.AddInPowerPoint2013"
remove-ItemProperty -Path  "HKCU:\Software\Microsoft\Office\$WordVersion.0\Word\Resiliency\DoNotDisableAddinList" -Name "SpiderDocs.AddInWord2013"



#
# Full uninstall
#
Get-Key -ParentKey HKCU -KeyName "Software\Microsoft\Office\$OutlookVersion.0\Outlook\Addins" | Remove-Item  -Recurse
Get-Key -ParentKey HKCU -KeyName "Software\Microsoft\Office\$ExcelVersion.0\Excel\Addins" | Remove-Item  -Recurse
Get-Key -ParentKey HKCU -KeyName "Software\Microsoft\Office\$PowerPointVersion.0\PowerPoint\Addins" | Remove-Item  -Recurse
Get-Key -ParentKey HKCU -KeyName "Software\Microsoft\Office\$WordVersion.0\Word\Addins" | Remove-Item  -Recurse

Get-Key -ParentKey HKCU -KeyName "Software\Wow6432Node\Microsoft\Office\$OutlookVersion.0\Outlook\Addins" | Remove-Item -Recurse
Get-Key -ParentKey HKCU -KeyName "Software\Wow6432Node\Microsoft\Office\$ExcelVersion.0\Excel\Addins" | Remove-Item -Recurse
Get-Key -ParentKey HKCU -KeyName "Software\Wow6432Node\Microsoft\Office\$PowerPointVersion.0\PowerPoint\Addins" | Remove-Item -Recurse
Get-Key -ParentKey HKCU -KeyName "Software\Wow6432Node\Microsoft\Office\$WordVersion.0\Word\Addins" | Remove-Item -Recurse


Get-Key -ParentKey HKCU -KeyName "Software\Microsoft\Office\Outlook\Addins\SpiderDocs.AddInOutlook2013" | Remove-Item -Recurse
Get-Key -ParentKey HKCU -KeyName "Software\Microsoft\Office\Outlook\Addins\SpiderDocs.AddInOutlookAttachment2013" | Remove-Item -Recurse
Get-Key -ParentKey HKCU -KeyName "Software\Microsoft\Office\Excel\Addins\SpiderDocs.AddInExcel2013" | Remove-Item -Recurse
Get-Key -ParentKey HKCU -KeyName "Software\Microsoft\Office\PowerPoint\Addins\SpiderDocs.AddInPowerPoint2013" | Remove-Item -Recurse
Get-Key -ParentKey HKCU -KeyName "Software\Microsoft\Office\Word\Addins\SpiderDocs.AddInWord2013" | Remove-Item -Recurse

Get-Key -ParentKey HKCU -KeyName "Software\Wow6432Node\Microsoft\Office\Outlook\Addins\SpiderDocs.AddInOutlook2013" | Remove-Item -Recurse
Get-Key -ParentKey HKCU -KeyName "Software\Wow6432Node\Microsoft\Office\Outlook\Addins\SpiderDocs.AddInOutlookAttachment2013" | Remove-Item -Recurse
Get-Key -ParentKey HKCU -KeyName "Software\Wow6432Node\Microsoft\Office\Excel\Addins\SpiderDocs.AddInExcel2013" | Remove-Item -Recurse
Get-Key -ParentKey HKCU -KeyName "Software\Wow6432Node\Microsoft\Office\PowerPoint\Addins\SpiderDocs.AddInPowerPoint2013" | Remove-Item -Recurse
Get-Key -ParentKey HKCU -KeyName "Software\Wow6432Node\Microsoft\Office\Word\Addins\SpiderDocs.AddInWord2013" | Remove-Item -Recurse

# for dev adin
Get-Key -ParentKey HKCU -KeyName "Software\Microsoft\Office\Outlook\Addins\AddInOutlook2013" | Remove-Item -Recurse
Get-Key -ParentKey HKCU -KeyName "Software\Microsoft\Office\Outlook\Addins\AddInOutlookAttachment2013" | Remove-Item -Recurse
Get-Key -ParentKey HKCU -KeyName "Software\Microsoft\Office\Excel\Addins\AddInExcel2013" | Remove-Item -Recurse
Get-Key -ParentKey HKCU -KeyName "Software\Microsoft\Office\PowerPoint\Addins\AddInPowerPoint2013" | Remove-Item -Recurse
Get-Key -ParentKey HKCU -KeyName "Software\Microsoft\Office\Word\Addins\AddInWord2013" | Remove-Item -Recurse

Get-Key -ParentKey HKCU -KeyName "Software\Wow6432Node\Microsoft\Office\Outlook\Addins\AddInOutlook2013" | Remove-Item -Recurse
Get-Key -ParentKey HKCU -KeyName "Software\Wow6432Node\Microsoft\Office\Outlook\Addins\AddInOutlookAttachment2013" | Remove-Item -Recurse
Get-Key -ParentKey HKCU -KeyName "Software\Wow6432Node\Microsoft\Office\Excel\Addins\AddInExcel2013" | Remove-Item -Recurse
Get-Key -ParentKey HKCU -KeyName "Software\Wow6432Node\Microsoft\Office\PowerPoint\Addins\AddInPowerPoint2013" | Remove-Item -Recurse
Get-Key -ParentKey HKCU -KeyName "Software\Wow6432Node\Microsoft\Office\Word\Addins\AddInWord2013" | Remove-Item -Recurse





#remove-Item -Path  "HKLM:\SOFTWARE\SpiderDocs" -Force
#remove-Item -Path  "HKLM:\SOFTWARE\Wow6432Node\SpiderDocs" -Force

Remove-ItemProperty -Path "HKLM:\SOFTWARE\SpiderDocs" -Name "SpiderDocsPath"
Remove-ItemProperty -Path "HKLM:\SOFTWARE\SpiderDocs" -Name "Version"

Remove-ItemProperty -Path "HKLM:\SOFTWARE\Wow6432Node\SpiderDocs" -Name "SpiderDocsPath"
Remove-ItemProperty -Path "HKLM:\SOFTWARE\Wow6432Node\SpiderDocs" -Name "Version"

#Remove Service

$service = Get-WmiObject -Class Win32_Service -Filter "Name='Spider Docs Auto Update'"
$service.stopservice()
$service.delete()

# Remove Spider Docs

Remove-Item "C:\Program Files (x86)\Spider Docs" -Force  -Recurse
Remove-Item "C:\Program Files (x86)\Spider Docs AddIns" -Force  -Recurse
Remove-Item "C:\Program Files (x86)\Spider Docs\setting.json" -Force  -Recurse
Remove-Item "C:\Program Files (x86)\Spider Docs Server\updates" -Force  -Recurse
