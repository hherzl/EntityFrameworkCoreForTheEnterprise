cls
set initialPath=%cd%
set srcPath=%cd%\Source\Backend\OnlineStore\OnlineStore.WebAPI
set testPath=%cd%\Source\Backend\OnlineStore\OnlineStore.WebAPI.UnitTests
cd %srcPath%
dotnet build
cd %testPath%
dotnet test
cd %initialPath%
pause
