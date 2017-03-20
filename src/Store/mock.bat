cls
set initialPath=%cd%
set srcPath=%cd%\src\Store.Core
set mockPath=%cd%\test\Store.Core.Mocks
cd %srcPath%
dotnet build
cd %mockPath%
dotnet test
cd %initialPath%
pause
