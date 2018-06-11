@echo off
set VERSION=%~1
                               
if "%VERSION%" == "" (goto error)
if EXIST %VERSION% (goto already_exist)

start /wait /d Frontend dotnet publish -o ..\%VERSION%\Frontend
start /wait /d Backend dotnet publish -o ..\%VERSION%\Backend
start /wait /d TextListener dotnet publish -o ..\%VERSION%\TextListener

xcopy /I config %VERSION%\config
xcopy run.cmd %VERSION%
xcopy stop.cmd %VERSION%

echo Project building succeeded
exit 0

:already_exist
echo Directory "%VERSION%" already exists
exit 1

:error
echo Project building failed
if EXIST %VERSION% (rd /s /q %VERSION%)
exit 1