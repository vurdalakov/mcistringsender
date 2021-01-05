@echo off

set configuration=Release
set runtime=win-x64

set exe=.\MciStringSender.exe

if exist %exe% del %exe%

dotnet publish --configuration %configuration% --runtime %runtime% --self-contained=false /p:PublishSingleFile=true

copy ..\bin\%configuration%\net5.0\%runtime%\publish\MciStringSender.exe %exe%
