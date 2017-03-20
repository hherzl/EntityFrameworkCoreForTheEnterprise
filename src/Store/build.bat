cls
set initialPath=%cd%
set srcPath=%cd%\src\Store.Core
cd %srcPath%
dotnet build
cd %mockPath%
dotnet test
cd %srcPath%
pause
