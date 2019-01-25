cls
set initialPath=%cd%
set rothschildHouseISPath=%cd%\Resources\RothschildHouse\RothschildHouse.IdentityServer
set rothschildHousePath=%cd%\Resources\RothschildHouse\RothschildHouse
set onlineStoreISPath=%cd%\Source\Backend\OnlineStore\OnlineStore.IdentityServer
set srcPath=%cd%\Source\Backend\OnlineStore\OnlineStore.WebAPI
set unitTestPath=%cd%\Source\Backend\OnlineStore\OnlineStore.WebAPI.UnitTests
set integrationTestPath=%cd%\Source\Backend\OnlineStore\OnlineStore.WebAPI.IntegrationTests
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
