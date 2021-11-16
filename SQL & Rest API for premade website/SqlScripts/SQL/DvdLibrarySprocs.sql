USE DvdLibrary
GO

--stored procedures
--delete then create

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdSelectAll')
      DROP PROCEDURE DvdSelectAll
GO

CREATE PROCEDURE DvdSelectAll
AS
	SELECT id, Title, ReleaseYear, Director, Rating, Notes
	FROM Dvd 
	ORDER BY Title
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdSelectById')
      DROP PROCEDURE DvdSelectById
GO

CREATE PROCEDURE DvdSelectById (
	@id int
)
AS
	SELECT id, Title, ReleaseYear, Director, Rating, Notes
	FROM Dvd
	WHERE id = @id
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdInsert')
      DROP PROCEDURE DvdInsert
GO

CREATE PROCEDURE DvdInsert (
	@id int output,
	@Title varchar(50),
	@ReleaseYear int,
	@Director varchar(50),
	@Rating varchar(6),
	@Notes varchar(300)
)
AS
	INSERT INTO Dvd (Title, ReleaseYear, Director, Rating, Notes)
	VALUES (@Title, @ReleaseYear, @Director, @Rating, @Notes)

	SET @id = SCOPE_IDENTITY()
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdUpdate')
      DROP PROCEDURE DvdUpdate
GO

CREATE PROCEDURE DvdUpdate (
	@id int,
	@Title varchar(50),
	@ReleaseYear int,
	@Director varchar(50),
	@Rating varchar(6),
	@Notes varchar(300)
)
AS
	UPDATE Dvd
		SET Title = @Title,
		ReleaseYear = @ReleaseYear,
		Director = @Director,
		Rating = @Rating,
		Notes = @Notes
	WHERE id = @id
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdDelete')
      DROP PROCEDURE DvdDelete
GO

CREATE PROCEDURE DvdDelete (
	@id int
)
AS
	DELETE FROM Dvd
	WHERE id = @id
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdSelectByTitle')
      DROP PROCEDURE DvdSelectByTitle
GO

CREATE PROCEDURE DvdSelectByTitle (
	@Title varchar(50)
)
AS
	SELECT id, Title, ReleaseYear, Director, Rating, Notes
	FROM Dvd
	WHERE Title LIKE @Title
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdSelectByYear')
      DROP PROCEDURE DvdSelectByYear
GO

CREATE PROCEDURE DvdSelectByYear (
	@ReleaseYear int
)
AS
	SELECT id, Title, ReleaseYear, Director, Rating, Notes
	FROM Dvd
	WHERE ReleaseYear LIKE @ReleaseYear
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdSelectByDirector')
      DROP PROCEDURE DvdSelectByDirector
GO

CREATE PROCEDURE DvdSelectByDirector (
	@Director varchar(50)
)
AS
	SELECT id, Title, ReleaseYear, Director, Rating, Notes
	FROM Dvd
	WHERE Director LIKE @Director
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdSelectByRating')
      DROP PROCEDURE DvdSelectByRating
GO

CREATE PROCEDURE DvdSelectByRating (
	@Rating varchar(6)
)
AS
	SELECT id, Title, ReleaseYear, Director, Rating, Notes
	FROM Dvd
	WHERE Rating LIKE @Rating
GO
