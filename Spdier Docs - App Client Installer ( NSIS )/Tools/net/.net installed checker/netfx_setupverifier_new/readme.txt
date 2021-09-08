-----------------------------------------------------
.NET FRAMEWORK SETUP VERIFICATION TOOL USER'S GUIDE
-----------------------------------------------------

The latest version of this user's guide can be found at https://blogs.msdn.microsoft.com/astebner/2008/10/13/net-framework-setup-verification-tool-users-guide/.

The .NET Framework Repair Tool can be found at the following locations:

    http://support.microsoft.com/kb/2698555
    http://go.microsoft.com/fwlink/?LinkID=246062

Additional support can be obtained by visiting the .NET Framework setup forum at http://social.msdn.microsoft.com/Forums/en-US/netfxsetup/threads.

-----------------------------
INTRODUCTION
-----------------------------

This .NET Framework setup verification tool is designed to automatically perform a set of steps to verify 
the installation state of one or more versions of the .NET Framework on a computer.  It will verify the 
presence of files, directories, registry keys and values for the .NET Framework.  It will also verify that 
simple applications that use the .NET Framework can be run correctly.

-----------------------------
DOWNLOAD LOCATIONS
-----------------------------

The .NET Framework setup verification tool is available for download at the following locations:

http://cid-27e6a35d1a492af7.skydrive.live.com/self.aspx/Blog%7C_Tools/netfx%7C_setupverifier%7C_new.zip 

http://blogs.msdn.com/astebner/attachment/8999004.ashx

The .zip file that contains the tool also contains a file named history.txt that lists when the most recent 
version of the tool was published and what changes have been made to the tool over time.

-----------------------------
SUPPORTED PRODUCTS
-----------------------------

The .NET Framework setup verification tool supports verifying the following products:

.NET Framework 1.0 
.NET Framework 1.1 
.NET Framework 1.1 SP1 
.NET Framework 2.0 
.NET Framework 2.0 SP1 
.NET Framework 2.0 SP2 
.NET Framework 3.0 
.NET Framework 3.0 SP1 
.NET Framework 3.0 SP2 
.NET Framework 3.5 
.NET Framework 3.5 SP1
.NET Framework 4 Client
.NET Framework 4 Full
.NET Framework 4.5
.NET Framework 4.5.1
.NET Framework 4.5.2
.NET Framework 4.6
.NET Framework 4.6.1
.NET Framework 4.6.2

By default, the .NET Framework setup verification tool will only list versions of the .NET Framework that 
it detects are installed on the computer that it is being run on.  As a result, the tool will not list all 
of the above versions of the .NET Framework.  This product filtering can be overridden by running the .NET 
Framework setup verification tool with the following command line switch:

netfx_setupverifier.exe /q:a /c:"setupverifier2.exe /a"

-----------------------------
SILENT MODE
-----------------------------

The .NET Framework setup verification tool does not support running in silent mode.

-----------------------------
EXIT CODES
-----------------------------

The verification tool can returns the following exit codes:

0 -    verification completed successfully for the specified product 
1 -    the required file setupverifier.ini was not found in the same path as setupverifier.exe 
2 -    a product name was passed in that cannot be verified because it does not support installing on the 
       OS that the tool is running on 
3 -    a product name was passed in that does not exist in setupverifier.ini 
100 -  verification failed for the specified product 
1602 - verification was canceled

-----------------------------
LOG FILES
-----------------------------

This verification tool creates 2 log files by default that can be used to determine what actions the tool 
is taking and what errors it encounters while verifying a product.  The 2 log files are listed below, and 
they are created in the %temp% directory by default.  Note that you can find the %temp% directory by 
clicking on the Windows start menu, choosing Run, typing %temp% and clicking OK to open the directory in 
Windows Explorer.

%temp%\setupverifier_main_*.txt - this log contains information about all actions taken during a verification 
                                  tool session; it will include information about each resource that the tool 
                                  attempts to verify for a chosen product and whether or not that resource was 
                                  found on the system; this log tends to be fairly long, so errors will be logged 
                                  with the prefix ****ERROR**** to make it easy to search and find them 

%temp%\setupverifier_errors_*.txt - this log only contains information about any errors found during verification 
                                    of a chosen product

A new pair of log files will be created each time the verification tool is launched.  The date and time the tool 
is launched will be appended to the end of the log file names by default in place of the * in the names listed 
above.  If you want to control the exact names used for the log files, you can use the following command line 
parameters:

/l <filename> - specifies a name to replace the default value of setupverifier_main_*.txt for the main activity 
                log for the verification tool 

/e <filename> - specifies a name to replace the default value of setupverifier_errors_*.txt for the error log 
                for the verification tool

For example, the following command line will allow you to specify non-default names for both log files:

netfx_setupverifier.exe /q:a /c:"setupverifier2.exe /l %temp%\my_main_log.txt /e %temp%\my_error_log.txt"

