IF NOT EXISTS (SELECT name FROM master.sys.sql_logins WHERE name = 'rothschild-house.api.payment-gateway')
	BEGIN
		CREATE LOGIN [rothschild-house.api.payment-gateway] WITH PASSWORD = 'rothschild-house.api.paymentgateway2023'
	END
