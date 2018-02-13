cls
set initialPath=%cd%
set srcPath=%cd%\src\Store.Core
set testPath=%cd%\test\Store.Core.Tests
set apiTestPath=%cd%\test\Store.API.Tests
cd %srcPath%
dotnet restore
dotnet build
cd %testPath%
dotnet restore
dotnet build
cd %apiTestPath%
dotnet restore
dotnet build
cd %initialPath%
pause
