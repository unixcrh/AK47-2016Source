"%windir%"\microsoft.net\framework\v4.0.30319\msbuild.exe %~dp0PPTSAll.csproj
xcopy .\Bin\*.xap .\MCSWebApp\Xap\ /Y /D /R
pause 