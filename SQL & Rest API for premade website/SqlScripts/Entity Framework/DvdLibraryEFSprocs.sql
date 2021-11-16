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
	SELECT id, title, releaseYear, director, rating, notes
	FROM Dvds 
	ORDER BY title
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdSelectById')
      DROP PROCEDURE DvdSelectById
GO

CREATE PROCEDURE DvdSelectById (
	@id int
)
AS
	SELECT id, title, releaseYear, director, rating, notes
	FROM Dvds
	WHERE id = @id
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdInsert')
      DROP PROCEDURE DvdInsert
GO

CREATE PROCEDURE DvdInsert (
	@id int output,
	@title varchar(50),
	@releaseYear int,
	@director varchar(50),
	@rating varchar(6),
	@notes varchar(300)
)
AS
	INSERT INTO Dvds (title, releaseYear, director, rating, notes)
	VALUES (@title, @releaseYear, @director, @rating, @notes)

	SET @id = SCOPE_IDENTITY()
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdUpdate')
      DROP PROCEDURE DvdUpdate
GO

CREATE PROCEDURE DvdUpdate (
	@id int,
	@title varchar(50),
	@releaseYear int,
	@director varchar(50),
	@rating varchar(6),
	@notes varchar(300)
)
AS
	UPDATE Dvds
		SET title = @title,
		releaseYear = @releaseYear,
		director = @director,
		rating = @rating,
		notes = @notes
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
	DELETE FROM Dvds
	WHERE id = @id
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdSelectByTitle')
      DROP PROCEDURE DvdSelectByTitle
GO

CREATE PROCEDURE DvdSelectByTitle (
	@title varchar(50)
)
AS
	SELECT id, title, releaseYear, director, rating, notes
	FROM Dvds
	WHERE title LIKE @title
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdSelectByYear')
      DROP PROCEDURE DvdSelectByYear
GO

CREATE PROCEDURE DvdSelectByYear (
	@releaseYear int
)
AS
	SELECT id, title, releaseYear, director, rating, notes
	FROM Dvds
	WHERE releaseYear LIKE @releaseYear
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdSelectByDirector')
      DROP PROCEDURE DvdSelectByDirector
GO

CREATE PROCEDURE DvdSelectByDirector (
	@director varchar(50)
)
AS
	SELECT id, title, releaseYear, director, rating, notes
	FROM Dvds
	WHERE director LIKE @director
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdSelectByRating')
      DROP PROCEDURE DvdSelectByRating
GO

CREATE PROCEDURE DvdSelectByRating (
	@rating varchar(6)
)
AS
	SELECT id, title, releaseYear, director, rating, notes
	FROM Dvds
	WHERE rating LIKE @rating
GO
