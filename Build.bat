:: START PS Admin.
POWERSHELL START-PROCESS POWERSHELL -VERB runAs -ArgumentList '-noexit', '-File', '%CD%\Build.ps1 %CD%'