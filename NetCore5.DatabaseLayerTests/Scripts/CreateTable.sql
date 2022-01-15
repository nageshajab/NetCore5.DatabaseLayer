IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='a' and xtype='U')
	BEGIN
		create table a(b int)
	END  
