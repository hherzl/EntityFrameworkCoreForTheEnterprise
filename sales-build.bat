cls
set initialPath=%cd%
set rothschildHouseISPath=%cd%\Resources\RothschildHouse\RothschildHouse.IdentityServer
set rothschildHousePath=%cd%\Resources\RothschildHouse\RothschildHouse
set onlineStoreISPath=%cd%\Source\Backend\OnlineStore\OnlineStore.Identity
set srcPath=%cd%\Source\Backend\OnlineStore\OnlineStore.API.Sales
set unitTestPath=%cd%\Source\Backend\OnlineStore\OnlineStore.API.Sales.UnitTests
set integrationTestPath=%cd%\Source\Backend\OnlineStore\OnlineStore.API.Sales.IntegrationTests
cd %rothschildHouseISPath%
dotnet build
cd %rothschildHousePath%
dotnet build
cd %onlineStoreISPath%
dotnet build
cd %srcPath%
dotnet build
cd %unitTestPath%
dotnet build
cd %integrationTestPath%
dotnet build
cd %initialPath%
pause
