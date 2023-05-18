cls
set db_server=(local)
set db_name=RothschildHouse
sqlcmd -S %db_server% -d %db_name% -E -i "01-tables.sql"
sqlcmd -S %db_server% -d %db_name% -E -i "02-constraints.sql"
sqlcmd -S %db_server% -d %db_name% -E -i "03-views.sql"
pause
