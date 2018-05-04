cls
set initialPath=%cd%
set testPath=%cd%\test\Store.API.Tests
cd %testPath%
dotnet test
cd %initialPath%
pause
