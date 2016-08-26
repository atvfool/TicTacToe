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
	, intMoveIndex INT NOT NULL IDENTITY
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

