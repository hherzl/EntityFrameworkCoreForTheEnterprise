cls
set initialPath=%cd%
set srcPath=%cd%\src\Store.Core
set testPath=%cd%\test\Store.Core.Tests
cd %srcPath%
cd %testPath%
dotnet test
cd %initialPath%
pause
