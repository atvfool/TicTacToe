/* ************************************ *\
 *										*
 * Author: Andrew Hayden				*
 * Date: 05/30/2013						*
 * Abstract: Database for recording tic	*
 * 	tac toe games						*
 *										*
\* ************************************ */


/* ************************************ *\
 * Check for foreign keys and drop 		*
 * if exist								*
\* ************************************ */

IF EXISTS (SELECT 1 FROM sys.objects WHERE name = N'TGames_TGameDifficulties_FK')
BEGIN
	ALTER TABLE TGames
	DROP CONSTRAINT TGames_TGameDifficulties_FK
END
IF EXISTS (SELECT 1 FROM sys.objects WHERE name = N'TGames_TGameOutcome_FK')
BEGIN
	ALTER TABLE TGames
	DROP CONSTRAINT TGames_TGameOutcome_FK
END
IF EXISTS (SELECT 1 FROM sys.objects WHERE name = N'TGames_TGameMoves_FK')
BEGIN
	ALTER TABLE TGames
	DROP CONSTRAINT TGames_TGameMoves_FK
END



/* ************************************ *\
 * Name: TGameDifficulties				*
 * Abstract: Stores available 			*
 *		difficulties					*
\* ************************************ */
IF EXISTS (SELECT 1 FROM sys.tables WHERE name = N'TGameDifficulties')
BEGIN
	DROP TABLE TGameDifficulties
END

GO

CREATE TABLE TGameDifficulties
(
	  intGameDifficultyID INT NOT NULL PRIMARY KEY IDENTITY
	, strGameDifficulty NVARCHAR(50)
)

GO

INSERT INTO TGameDifficulties(strGameDifficulty)
VALUES('Easy')
INSERT INTO TGameDifficulties(strGameDifficulty)
VALUES('Medium')
INSERT INTO TGameDifficulties(strGameDifficulty)
VALUES('Hard')



/* ************************************ *\
 * Name: TGameOutcomes					*
 * Abstract: Stores available 			*
 *		outcomes						*
\* ************************************ */
IF EXISTS (SELECT 1 FROM sys.tables WHERE name = N'TGameOutcomes')
BEGIN
	DROP TABLE TGameOutcomes
END

GO

CREATE TABLE TGameOutcomes
(
	  intGameOutcomeID INT NOT NULL PRIMARY KEY IDENTITY
	, strGameOutcome NVARCHAR(50)
)

GO

INSERT INTO TGameOutcomes(strGameOutcome)
VALUES('Player Won')
INSERT INTO TGameOutcomes(strGameOutcome)
VALUES('Computer Won')
INSERT INTO TGameOutcomes(strGameOutcome)
VALUES('Tie')



/* ************************************ *\
 * Name: TGames				 			*
 * Abstract: Stores games and their		*
 *		outcomes						*
\* ************************************ */
IF EXISTS (SELECT 1 FROM sys.tables WHERE name = N'TGames')
BEGIN 
	DROP TABLE TGames
END

GO

CREATE TABLE TGames
(
	  intGameID INT NOT NULL PRIMARY KEY IDENTITY
	, strPlayerName NVARCHAR(50)
	, blnComputerMovesFirst BIT
	, intGameDifficultyID INT
	, intGameOutcomeID INT
	, dtmPlayed DATETIME DEFAULT CURRENT_TIMESTAMP
)

GO



/* ************************************ *\
 * Name: TGamesMoves		 			*
 * Abstract: Stores games moves			*
\* ************************************ */
IF EXISTS (SELECT 1 FROM sys.tables WHERE name = N'TGameMoves')
BEGIN
	DROP TABLE TGameMoves
END

GO

CREATE TABLE TGameMoves
(
	  intGameID INT NOT NULL 
	, intMoveIndex INT NOT NULL
	, intSquare INT
		, PRIMARY KEY (intGameID,intMoveIndex)
)


/* ************************************ *\
 * 			  Foreign Keys	 			*
\* ************************************ */

ALTER TABLE TGames ADD CONSTRAINT TGames_TGameDifficulties_FK
FOREIGN KEY (intGameDifficultyID) REFERENCES TGameDifficulties(intGameDifficultyID)

ALTER TABLE TGames ADD CONSTRAINT TGames_TGameOutcome_FK
FOREIGN KEY (intGameOutcomeID) REFERENCES TGameOutcomes(intGameOutcomeID)



/* ************************************ *\
 * 			  Stored Procedures 		*
\* ************************************ */
IF EXISTS (SELECT 1 FROM sys.objects WHERE Object_ID = Object_ID(N'spCreateGame'))
BEGIN
	DROP PROCEDURE spCreateGame
END

IF EXISTS (SELECT 1 FROM sys.objects WHERE Object_ID = Object_ID(N'spUpdateWhoWon'))
BEGIN
	DROP PROCEDURE spUpdateWhoWon
END

IF EXISTS (SELECT 1 FROM sys.objects WHERE Object_ID = Object_ID(N'spInsertNewMove'))
BEGIN
	DROP PROCEDURE spInsertNewMove
END

IF EXISTS (SELECT 1 FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'spGetRecentGames'))
BEGIN
	DROP PROCEDURE spGetRecentGames
END

GO



/* ************************************ *\
 * Name: spCreateGame		 			*
 * Abstract: Create games				*
\* ************************************ */
CREATE PROCEDURE spCreateGame
(
  @strPlayerName As Nvarchar(50)
, @blnComputerMovesFirst AS BIT
, @intGameDifficultyID AS INT
) 
AS
BEGIN
	BEGIN TRANSACTION
		INSERT INTO TGames(strPlayerName, blnComputerMovesFirst, intGameDifficultyID)
		VALUES(@strPlayerName, @blnComputerMovesFirst, @intGameDifficultyID)
		SELECT MAX(intGameID) FROM TGames
	COMMIT TRANSACTION
END

GO



/* ************************************ *\
 * Name: spUpdateWhoWon		 			*
 * Abstract: Updates winnner			*
\* ************************************ */
CREATE PROCEDURE spUpdateWhoWon
(
   @intGameID INT
,  @intGameOutcomeID INT
)
AS
BEGIN
	UPDATE TGames
	SET intGameOutcomeID = @intGameOutcomeID
	WHERE intGameID = @intGameID
END

GO



/* ************************************ *\
 * Name: spInsertNewMove	 			*
 * Abstract: Inserts move				*
\* ************************************ */
CREATE PROCEDURE spInsertNewMove
(
   @intGameID INT
,  @intSquare INT
)
AS 
BEGIN

	DECLARE @intMoveIndex AS INTEGER
	SET @intMoveIndex = (ISNULL((SELECT MAX(intMoveIndex) FROM TGameMoves WHERE intGameID = @intGameID), 0)) + 1

	INSERT INTO  TGameMoves(intGameID, intMoveIndex, intSquare)
	VALUES(@intGameID, @intMoveIndex, @intSquare)

	SELECT MAX(intMoveIndex) FROM TGameMoves WHERE intGameID = @intGameID

END

GO

/* ************************************ *\
 * Name: spGetRecentGames	 			*
 * Abstract: Gets a list of all recent 	*
 " games and related information		*
\* ************************************ */
CREATE PROCEDURE spGetRecentGames
AS
BEGIN
	SELECT [g].[intgameid]
		 , [g].[strPlayerName]
		 , [gd].[strGameDifficulty]
		 , ISNULL([goc].[strGameOutcome], 'Never Finished') AS [GameOutome]
		 , ISNULL(MAX([gm].[intMoveIndex]), 0) AS [MoveCount]
		 , [g].[dtmPlayed]
	FROM [TGames] [g]
	LEFT JOIN [TGameDifficulties] [gd] ON [g].[intGameDifficultyID] = [gd].[intGameDifficultyID]
	LEFT JOIN [TGameOutcomes] [goc] ON [g].[intGameOutcomeID] = [goc].[intGameOutcomeID]
	LEFT JOIN [TGameMoves] [gm] ON [g].[intGameID] = [gm].[intGameID] 
		AND [gm].[intMoveIndex] = (SELECT MAX([intMoveIndex]) FROM [TGameMoves] WHERE [intGameID] = [g].[intGameID])
	GROUP BY [g].[intGameID], [g].[dtmPlayed], [g].[strPlayerName], [gd].[strGameDifficulty], [goc].[strGameOutcome], [gm].[intMoveIndex]
	ORDER BY [g].[dtmPlayed] DESC
END
