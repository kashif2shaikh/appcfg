@echo off
del *.nupkg
nuget pack Acr.AppConfig.Client.nuspec
pause