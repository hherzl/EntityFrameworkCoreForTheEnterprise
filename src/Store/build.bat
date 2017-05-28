cls
set initialPath=%cd%
set srcPath=%cd%\src\Store.Core
set testPath=%cd%\test\Store.Core.Tests
set apiTestPath=%cd%\test\Store.API.Tests
cd %srcPath%
dotnet build
cd %testPath%
dotnet test
cd %apiTestPath%
dotnet test
cd %initialPath%
pause
