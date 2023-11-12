IF NOT EXISTS(SELECT name FROM master.sys.databases WHERE name = 'RothschildHouse')
    BEGIN
        CREATE DATABASE [RothschildHouse]
    END