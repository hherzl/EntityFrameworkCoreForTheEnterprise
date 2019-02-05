cls
set server=(local)
set database=OnlineStore
sqlcmd -S %server% -i "00 - Database.sql"
sqlcmd -S %server% -d %database% -i "01 - Schemas.sql"
sqlcmd -S %server% -d %database% -i "02 - Tables.sql"
sqlcmd -S %server% -d %database% -i "03 - Constraints.sql"
sqlcmd -S %server% -d %database% -i "04 - Functions.sql"
sqlcmd -S %server% -d %database% -i "05 - Views.sql"
sqlcmd -S %server% -d %database% -i "06 - Rows.sql"
pause
