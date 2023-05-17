cls
set initialPath=%cd%
set srcPath=%cd%\Source\Backend\OnlineStore\OnlineStore.API.Sales
set testPath=%cd%\Source\Backend\OnlineStore\OnlineStore.API.Sales.UnitTests
cd %srcPath%
dotnet build
cd %testPath%
dotnet test
cd %initialPath%
pause
