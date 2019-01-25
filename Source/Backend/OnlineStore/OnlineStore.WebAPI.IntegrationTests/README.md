# Entity Framework Core 2 for Enterprise

## Integration Tests

Execute *dotnet test* command to run integration tests for *Online Store Web API*.

### Prerequisites

In order to run integration tests for *Online Store Web API*, You need to start these Web APIS:

    *Rothschild House Identity Server*
    *Rothschild House Web API*
    *Online Store Identity Server*

These Web APIS can be started from command line with *dotnet run* command.

:warning: *Rothschild House Identity Server* runs on **18000** port.

:warning: *Rothschild House Web API* runs on **19000** port.

:warning: Identity Server for Online Store Web API* runs on **56000** port.

Also, specified SQL Server instance in *appsettings.json* file needs to be started before to run integration tests.
