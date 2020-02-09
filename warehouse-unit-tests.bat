cls
set initialPath=%cd%
set srcPath=%cd%\Source\Backend\OnlineStore\OnlineStore.API.Warehouse
set testPath=%cd%\Source\Backend\OnlineStore\OnlineStore.API.Warehouse.UnitTests
cd %srcPath%
dotnet build
cd %testPath%
dotnet test
cd %initialPath%
pause
