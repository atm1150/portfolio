USE master
GO

IF EXISTS(SELECT * FROM sys.databases WHERE name='DvdLibrary')
DROP DATABASE DvdLibrary
GO

CREATE DATABASE DvdLibrary
GO

USE DvdLibrary
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Dvd')
	DROP TABLE Dvd
GO

CREATE TABLE Dvd (
	id int identity(1,1) primary key not null,
	Title varchar(50) not null,
	ReleaseYear int not null,
	Director varchar(50) not null,
	Rating varchar(5) null,
	Notes varchar(300) null,	
)