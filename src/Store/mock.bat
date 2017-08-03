cls
set initialPath=%cd%
set srcPath=%cd%\src\Store.Core
set mockPath=%cd%\src\Store.Mocker
cd %srcPath%
dotnet build
cd %mockPath%
dotnet run
cd %initialPath%
pause
