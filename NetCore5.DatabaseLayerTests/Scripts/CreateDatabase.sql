 IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'test')
  BEGIN
	create database test
  END 