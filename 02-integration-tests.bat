cls
set initialPath=%cd%
set rothschildHouseISPath=%cd%\Resources\RothschildHouse\RothschildHouse.IdentityServer
set rothschildHousePath=%cd%\Resources\RothschildHouse\RothschildHouse
set onlineStoreISPath=%cd%\Source\Backend\OnlineStore\OnlineStore.IdentityServer
set srcPath=%cd%\Source\Backend\OnlineStore\OnlineStore.WebAPI
set testPath=%cd%\Source\Backend\OnlineStore\OnlineStore.WebAPI.IntegrationTests
cd %rothschildHouseISPath%
start dotnet run
cd %rothschildHousePath%
start dotnet run
cd %onlineStoreISPath%
start dotnet run
cd %srcPath%
dotnet build
cd %testPath%
dotnet test
cd %initialPath%
pause
