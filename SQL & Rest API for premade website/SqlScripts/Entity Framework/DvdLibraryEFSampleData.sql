USE DvdLibrary
GO

-- Sample Data

SET IDENTITY_INSERT Dvds ON

INSERT INTO Dvds (id, title, releaseYear, director, rating, notes)
VALUES (1, 'The Lion King', 1994, 'Roger Allers', 'G', 'Lion with family issues'),
	(2, 'Titanic', 1997, 'James Cameron', 'PG-13', 'Love story about a sinking ship'),
	(3, 'Batman', 1989, 'Tim Burton', 'PG-13', 'Rich billionaire attempts alternative therapy for coping with loss'),
	(4, 'Star Wars', 1977, 'George Lucas', 'PG', 'Laser swords and an evil cyborg in space')

SET IDENTITY_INSERT Dvds OFF