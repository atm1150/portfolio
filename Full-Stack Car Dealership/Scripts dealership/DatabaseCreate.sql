USE master
GO

IF EXISTS(SELECT * FROM sys.databases WHERE name='CarDealership')
DROP DATABASE CarDealership
GO

CREATE DATABASE CarDealership
GO