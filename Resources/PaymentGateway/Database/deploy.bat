cls
set db_server=(local)
set db_name=RothschildHouse
sqlcmd -S %db_server% -E -i "00-logins.sql"
sqlcmd -S %db_server% -E -i "01-database.sql"
sqlcmd -S %db_server% -d %db_name% -E -i "02-users.sql"
sqlcmd -S %db_server% -d %db_name% -E -i "03-schemas.sql"
sqlcmd -S %db_server% -d %db_name% -E -i "04-tables.sql"
sqlcmd -S %db_server% -d %db_name% -E -i "05-constraints.sql"
sqlcmd -S %db_server% -d %db_name% -E -i "06-views.sql"
pause
