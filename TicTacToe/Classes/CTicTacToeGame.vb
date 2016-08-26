' -------------------------------------------------------------------------
' Class: CTicTacToe
' Author: Andrew Hayden
' Abstract: Tic tac toe module
'
' Revision        Owner   Changes:
' 1  2013/05/30   A.H.    Created.
' -------------------------------------------------------------------------

' -------------------------------------------------------------------------
' Options
' -------------------------------------------------------------------------
Option Explicit On

' -------------------------------------------------------------------------
' Imports
' -------------------------------------------------------------------------

Public Class CTicTacToeGame

    ' -------------------------------------------------------------------------
    ' Name: <name>
    ' Abstract: <description>
	' -------------------------------------------------------------------------


	Enum enuGridEntry
		Empty
		PlayerX
		PlayerO
	End Enum

    ' -------------------------------------------------------------------------
    ' Class Properties
    ' -------------------------------------------------------------------------
    ' 0-1-2
    ' 1
    ' 2
    ' First Number is row, second is columns
	Private m_aintPlayerMoves(8) As Integer	' 0 = Empty, 1 = player, 2 = cpu
    ' Decides if the player or cpu goes first, 1 = player, 2 = CPU
	Private m_blnWhoMovesFirst As Boolean = True
	' Decices what piece the player uses, True = Player, False = Computer
    Private m_intPlayerPiece As Integer
    ' 0 = Not Over, 1 = Player, 2 = CPU, 3 = Tie
    Private m_intWhoWon As Integer
    ' 1 = Easy, 2 = Medium, 3 = Hard
	Private m_intDifficulty As Integer = 1
	Private m_enuGridEntries(8) As enuGridEntry
	Private m_intScore As Integer
    Private m_blnGameOver As Boolean
	Private m_blnTurnForX As Boolean = True
	Private m_clsDatabase As CDatabaseUtilites
	Private m_clsParameters As CParameterCollection
	Private m_intGameID As Integer
	Private _getPiece As Integer



	' -------------------------------------------------------------------------
	' Name: New
	' Abstract: Constructor
	' -------------------------------------------------------------------------
	Public Sub New(ByVal enuValues() As enuGridEntry, ByVal blnWhoMovesFirst As Boolean)

		Try

			InitializeBoard()
			m_enuGridEntries = enuValues
			m_blnWhoMovesFirst = blnWhoMovesFirst

		Catch excError As Exception

			WriteLog(excError.ToString)

		End Try

	End Sub



	' -------------------------------------------------------------------------
	' Name: New
	' Abstract: Constructor
	' -------------------------------------------------------------------------
    Public Sub New(ByVal strPlayersName As String, ByVal blnWhoMovesFirst As Boolean, ByVal intGameDifficultyID As Integer)

        Try

            m_blnWhoMovesFirst = blnWhoMovesFirst
            m_intDifficulty = intGameDifficultyID

            InitializeBoard()
            m_enuGridEntries(0) = enuGridEntry.Empty
            m_enuGridEntries(1) = enuGridEntry.Empty
            m_enuGridEntries(2) = enuGridEntry.Empty
            m_enuGridEntries(3) = enuGridEntry.Empty
            m_enuGridEntries(4) = enuGridEntry.Empty
            m_enuGridEntries(5) = enuGridEntry.Empty
            m_enuGridEntries(6) = enuGridEntry.Empty
            m_enuGridEntries(7) = enuGridEntry.Empty
            m_enuGridEntries(8) = enuGridEntry.Empty
            m_clsParameters = New CParameterCollection

            m_clsDatabase = New CDatabaseUtilites
            m_clsParameters.Add("@strPlayerName", DbType.String, strPlayersName)  ' TODO: Get Players name
            m_clsParameters.Add("@blnComputerMovesFirst", DbType.Boolean, blnWhoMovesFirst)
            m_clsParameters.Add("@intGameDifficultyID", DbType.Int32, intGameDifficultyID)

            Dim dtTable As DataTable
            dtTable = m_clsDatabase.RunStoredProcedure("spCreateGame", m_clsParameters)

            If dtTable.Rows.Count > 0 Then
                m_intGameID = dtTable.Rows(0)(0)
            End If

            m_clsDatabase.Dispose()

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: New
    ' Abstract: Constructor
    ' -------------------------------------------------------------------------
    Public Sub New()

        Try

            InitializeBoard()
            m_enuGridEntries(0) = enuGridEntry.Empty
            m_enuGridEntries(1) = enuGridEntry.Empty
            m_enuGridEntries(2) = enuGridEntry.Empty
            m_enuGridEntries(3) = enuGridEntry.Empty
            m_enuGridEntries(4) = enuGridEntry.Empty
            m_enuGridEntries(5) = enuGridEntry.Empty
            m_enuGridEntries(6) = enuGridEntry.Empty
            m_enuGridEntries(7) = enuGridEntry.Empty
            m_enuGridEntries(8) = enuGridEntry.Empty

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: InitializeBoard
    ' Abstract: Gets the game ready
    ' -------------------------------------------------------------------------
    Private Sub InitializeBoard()

        Try

			Dim intSquareIndex As Integer = 0
            Dim intRowIndex As Integer = 0

			For intSquareIndex = m_aintPlayerMoves.GetLowerBound(0) To m_aintPlayerMoves.GetUpperBound(0)

                m_aintPlayerMoves(intSquareIndex) = -1

			Next

			m_intWhoWon = 0

		Catch excError As Exception

			WriteLog(excError.ToString)

		End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: PlaceMove
    ' Abstract: Places the move if it can otherwise does nothing
    ' -------------------------------------------------------------------------
	Public Function PlaceMove(ByVal intSquare As Integer, ByVal intPlayer As Integer) As Boolean

		Dim blnResult As Boolean = False

		Try

			If IsGameOver() = False Then

				If CheckMove(intSquare) = True Then

					SetPiece(intSquare, intPlayer)

					If m_blnTurnForX = True Then m_blnTurnForX = False
					If m_blnTurnForX = False Then m_blnTurnForX = True

					m_clsDatabase = New CDatabaseUtilites

					m_clsParameters.Clear()
                    m_clsParameters.Add("@intGameID", DbType.Int32, m_intGameID)    ' TODO: Make class variable of game ID and place it here
                    m_clsParameters.Add("@intSquare", DbType.Int32, intSquare)  ' TODO: make introws and intcolumns into a singel variable, intsquare

                    m_clsDatabase.AddMoveToGame(m_clsParameters)

                    m_clsDatabase.Dispose()

					blnResult = True

				End If

			End If

		Catch excError As Exception

			WriteLog(excError.ToString)

		End Try

		Return blnResult

	End Function



    ' -------------------------------------------------------------------------
    ' Name: CheckMove
    ' Abstract: Check if the move is available, place the piece, and record
    ' -------------------------------------------------------------------------
	Private Function CheckMove(ByVal intSquare As Integer) As Boolean

		Dim blnResult As Boolean = False

		Try

            If GetPiece(intSquare) = -1 Then

                blnResult = True

            End If

		Catch excError As Exception

			WriteLog(excError.ToString)

		End Try

		Return blnResult

	End Function



    ' -------------------------------------------------------------------------
    ' Name: IsGameOver
    ' Abstract: Checks if either player won or if the move results in a tie
    ' -------------------------------------------------------------------------
    Public Function IsGameOver() As Boolean

        Dim blnResult As Boolean = False

        Try

            Dim intIndex As Integer = 0
            Dim blnCheckIfPlayerWon As Boolean = False


            For intIndex = 1 To 2 Step 1

                blnCheckIfPlayerWon = False

                blnCheckIfPlayerWon = CheckIfAPlayerWon(intIndex)

                If blnCheckIfPlayerWon = True Then

                    m_intWhoWon = intIndex
                    blnResult = True

                End If

            Next

            If blnResult = False And AreThereAnyMovesLeft() = False Then

                m_intWhoWon = 3
                blnResult = True

            End If

            If blnResult = True Then
                m_clsDatabase.UpdateGameWinner(m_intGameID, m_intWhoWon)
            End If

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

        Return blnResult

    End Function



    ' -------------------------------------------------------------------------
    ' Name: CheckIfAPlayerWon
    ' Abstract: Checks if one of the players won, if yes then return true
    ' -------------------------------------------------------------------------
    Private Function CheckIfAPlayerWon(ByVal intPlayerType As Integer) As Boolean

        Dim blnResult As Boolean = False

        Try

            ' Check all the rows
			If GetPiece(0) = intPlayerType And _
			   GetPiece(1) = intPlayerType And _
			   GetPiece(2) = intPlayerType Then

				blnResult = True

			ElseIf GetPiece(3) = intPlayerType And _
				   GetPiece(4) = intPlayerType And _
				   GetPiece(5) = intPlayerType Then

				blnResult = True

			ElseIf GetPiece(6) = intPlayerType And _
				   GetPiece(7) = intPlayerType And _
				   GetPiece(8) = intPlayerType Then

				blnResult = True

				' Check all the columns
			ElseIf GetPiece(0) = intPlayerType And _
				   GetPiece(3) = intPlayerType And _
				   GetPiece(6) = intPlayerType Then

				blnResult = True

			ElseIf GetPiece(1) = intPlayerType And _
				   GetPiece(4) = intPlayerType And _
				   GetPiece(7) = intPlayerType Then

				blnResult = True

			ElseIf GetPiece(2) = intPlayerType And _
				   GetPiece(5) = intPlayerType And _
				   GetPiece(8) = intPlayerType Then

				blnResult = True

				' Diagonal
			ElseIf GetPiece(0) = intPlayerType And _
				   GetPiece(4) = intPlayerType And _
				   GetPiece(8) = intPlayerType Then

				blnResult = True

			ElseIf GetPiece(2) = intPlayerType And _
				   GetPiece(4) = intPlayerType And _
				   GetPiece(7) = intPlayerType Then

				blnResult = True

			End If

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

        Return blnResult

	End Function



	' -------------------------------------------------------------------------
	' Name: ComputeScore
	' Abstract: 
	' -------------------------------------------------------------------------
	Private Sub ComputeScore()
		Dim intTotal As Integer = 0
		Dim intLines As Integer(,) = {{0, 1, 2}, {3, 4, 5}, {6, 7, 8}, {0, 3, 6}, {1, 4, 7}, {2, 5, 8}, _
			{0, 4, 8}, {2, 4, 6}}

		For intIndex As Integer = intLines.GetLowerBound(0) To intLines.GetUpperBound(0)
			intTotal += GetScoreForOneLine(New enuGridEntry() {m_enuGridEntries(intLines(intIndex, 0)), m_enuGridEntries(intLines(intIndex, 1)), m_enuGridEntries(intLines(intIndex, 2))})
		Next
		m_intScore = intTotal
	End Sub



	' -------------------------------------------------------------------------
	' Name: GetScoreForOneLine
	' Abstract: getst the score for one line
	' -------------------------------------------------------------------------
	Private Function GetScoreForOneLine(enuValues As enuGridEntry()) As Integer

		Dim intScore As Integer

		Try

			Dim intCountX As Integer = 0
			Dim intCountO As Integer = 0

			For Each enuValue As enuGridEntry In enuValues
				If enuValue = enuGridEntry.PlayerX Then
					intCountX += 1
				ElseIf enuValue = enuGridEntry.PlayerO Then
					intCountO += 1
				End If
			Next

			If intCountO = 3 OrElse intCountX = 3 Then
				m_blnGameOver = True
			End If

			'The player who has turn should have more advantage.
			'What we should have done
			Dim intAdvantage As Integer = 1

			If intCountO = 0 Then

				If m_blnWhoMovesFirst Then
					intAdvantage = 3
				End If

				intScore = CInt(Math.Truncate(System.Math.Pow(10, intCountX))) * intAdvantage

			ElseIf intCountX = 0 Then

				If Not m_blnWhoMovesFirst Then
					intAdvantage = 3
				End If

				intScore = -CInt(Math.Truncate(System.Math.Pow(10, intCountO))) * intAdvantage

			End If

		Catch excError As Exception

			WriteLog(excError.ToString)

		End Try

		Return intScore

	End Function



    ' -------------------------------------------------------------------------
    ' Name: AreThereAnyMovesLeft
    ' Abstract: Checks if there are any moves left, If yes returns true
    ' -------------------------------------------------------------------------
    Private Function AreThereAnyMovesLeft() As Boolean

        Dim blnResult As Boolean = False

        Try

            '  Are there any moves still left?
			If GetPiece(0) = -1 Or _
				   GetPiece(1) = -1 Or _
				   GetPiece(2) = -1 Or _
				   GetPiece(3) = -1 Or _
				   GetPiece(4) = -1 Or _
				   GetPiece(5) = -1 Or _
				   GetPiece(6) = -1 Or _
				   GetPiece(7) = -1 Or _
				   GetPiece(8) = -1 Then
				' Yes, The Game Can Continue
				blnResult = True
			End If


        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

		Return blnResult

    End Function



    ' -------------------------------------------------------------------------
    ' Name: ComputerPlaceMove
    ' Abstract: Will place a move for the computer
    ' -------------------------------------------------------------------------
    Public Function ComputerPlaceMove() As Integer

        Dim intComputerMove As Integer

        Try

            Select Case m_intDifficulty
                Case 1

                    intComputerMove = EasyMove()

                Case 2
                    ' Medium
                Case 3
                    intComputerMove = HardMove()
            End Select

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

        Return intComputerMove

    End Function



    ' -------------------------------------------------------------------------
    ' Name: EasyMove
    ' Abstract: Will place a move for the computer
    ' -------------------------------------------------------------------------
    Private Function EasyMove() As Integer

        Dim intComputerMove As Integer

        Try

            Dim rndNumber As Random
            rndNumber = New Random
            Dim intSquare As Integer

            Do

                intSquare = rndNumber.Next(0, 9)

            Loop Until PlaceMove(intSquare, 2) = True

            intComputerMove = intSquare

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

        Return intComputerMove

    End Function

#Region "MiniMax Stuff"



	' -------------------------------------------------------------------------
	' Name: MiniMax
	' Abstract: Does the minimax algorithim
	' -------------------------------------------------------------------------
	Public Function MiniMax(ByVal intDepth As Integer _
						   , ByVal intAlpha As Integer _
						   , ByVal intBeta As Integer _
						   , ByRef clsChildBoardWithMax As CTicTacToeGame) As Integer

		Dim intRecursiveScore As Integer = 0

		Try

			Dim intScore As Integer

			clsChildBoardWithMax = Nothing

			If intDepth = 0 Or IsTerminalNode() Then
				If m_blnTurnForX = True Then
					intRecursiveScore = m_intScore
				Else
					intRecursiveScore = -m_intScore
				End If
				Exit Try
			End If

			For Each clsChildBoard As CTicTacToeGame In GetChildren()

				Dim clsTest As CTicTacToeGame
				'intScore = 0
				intScore = -clsChildBoard.MiniMax(intDepth - 1, -intBeta, -intAlpha, clsTest)

				If intAlpha < intScore Then

					intAlpha = intScore

					clsChildBoardWithMax = clsChildBoard
					If intAlpha >= intBeta Then
						Exit For
					End If

				End If

			Next

			intRecursiveScore = intAlpha

		Catch excError As Exception

			WriteLog(excError.ToString)

		End Try

		Return intRecursiveScore

	End Function



	' -------------------------------------------------------------------------
	' Name: IsTerminalNode
	' Abstract: 
	' -------------------------------------------------------------------------	
	Private Function IsTerminalNode() As Boolean

		Dim blnResult As Boolean = True

		Try

			If IsGameOver() = True Then
				blnResult = True
			End If

			' If All entries are set then its a leaf node
			For Each enuEntry In m_enuGridEntries

				If enuEntry = enuGridEntry.Empty Then
					blnResult = False
					Exit For
				End If

			Next

		Catch excError As Exception

			WriteLog(excError.ToString)

		End Try

		Return blnResult

	End Function


	Public Function GetChild(ByVal enuNewValues() As enuGridEntry, ByVal blnTurnForX As Boolean) As CTicTacToeGame

		Dim clsReturnBoard As New CTicTacToeGame(enuNewValues, blnTurnForX)

		Return clsReturnBoard

	End Function



	' -------------------------------------------------------------------------
	' Name: GetChildren
	' Abstract: returns the current board
	' -------------------------------------------------------------------------	
	Public Function GetChildren() As IEnumerable(Of CTicTacToeGame)

		Dim aclsReturn As IEnumerable(Of CTicTacToeGame) = New CTicTacToeGame

		Try

			Dim intClassCount As Integer = 0

			For intIndex As Integer = 0 To m_enuGridEntries.Length - 1

				If m_enuGridEntries(intIndex) = enuGridEntry.Empty Then

					Dim enuNewValues As enuGridEntry() = DirectCast(m_enuGridEntries.Clone(), enuGridEntry())
					enuNewValues(intIndex) = m_enuGridEntries(intIndex)
					aclsReturn.Concat(GetChild(enuNewValues, m_blnTurnForX))
					
					If m_blnTurnForX = False Then m_blnTurnForX = True
					If m_blnTurnForX = True Then m_blnTurnForX = False
					intClassCount += 1


				End If

			Next

		Catch excError As Exception

			WriteLog(excError.ToString)

		End Try

		Return aclsReturn

	End Function



	' -------------------------------------------------------------------------
	' Name: FindNextMove
	' Abstract: finds the next move
	' -------------------------------------------------------------------------
    Private Function FindNextMove(ByVal intDepth As Integer) As Integer

        Dim aintNewCoordinates As Integer = 0

        Try

            Dim clsReturnNextMove As CTicTacToeGame = Nothing

            MiniMax(intDepth, Integer.MinValue + 1, Integer.MaxValue - 1, clsReturnNextMove)

            aintNewCoordinates = GetNextMoveFromNewBoard(clsReturnNextMove)

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

        Return aintNewCoordinates

    End Function



	' -------------------------------------------------------------------------
	' Name: GetNextMoveFromNewBoard
	' Abstract: Compares the current board and a new board to get the first 
	'	position that is different
	' -------------------------------------------------------------------------
    Private Function GetNextMoveFromNewBoard(ByRef clsNewBoard As CTicTacToeGame) As Integer

        Dim aintNewMoveCoordinates As Integer = -1

        Try

            For intSquare = m_aintPlayerMoves.GetLowerBound(0) To m_aintPlayerMoves.GetUpperBound(0)

                ' Compare each piece
                If Me.GetPiece(intSquare) <> clsNewBoard.GetPiece(intSquare) Then

                    aintNewMoveCoordinates = intSquare

                    Exit For

                End If

                ' If a piece was set
                If aintNewMoveCoordinates <> -1 Then
                    Exit For
                End If

            Next

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

        Return aintNewMoveCoordinates

    End Function

#End Region



	' -------------------------------------------------------------------------
	' Name: HardMove
	' Abstract: Gets the hard move coordinates
	' -------------------------------------------------------------------------
    Private Function HardMove() As Integer

        Dim aintHardMove As Integer = -1

        Try

            aintHardMove = FindNextMove(8)

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

        Return aintHardMove

    End Function





#Region "Get / Set Methods"

    Public Function GetWhoMovesFirst() As Boolean

        Return m_blnWhoMovesFirst

    End Function


	' -------------------------------------------------------------------------
	' Name: GetPiece
	' Abstract: Returns the piece
	' -------------------------------------------------------------------------
	Public Function GetPiece(ByVal intSquare As Integer) As Integer

		Dim intResult As Integer = 0

		Try

			If intSquare > 8 Then intSquare = 8
			If intSquare < 0 Then intSquare = 0

			intResult = m_aintPlayerMoves(intSquare)

		Catch excError As Exception

			WriteLog(excError.ToString)

		End Try

		Return intResult

	End Function



	' -------------------------------------------------------------------------
	' Name: SetPiece
	' Abstract: Sets the piece
	' -------------------------------------------------------------------------
	Public Sub SetPiece(ByVal intSquare As Integer, ByVal intPlayer As Integer)

		Try

			If intSquare > 8 Then intSquare = 8
			If intSquare < 0 Then intSquare = 0
			If intPlayer < 0 Then intPlayer = 0
			If intPlayer > 2 Then intPlayer = 2

			m_aintPlayerMoves(intSquare) = intPlayer

		Catch excError As Exception

			WriteLog(excError.ToString)

		End Try

	End Sub



	' -------------------------------------------------------------------------
	' Name: GetBoard
	' Abstract: Returns the board
	' -------------------------------------------------------------------------
	Public Function GetBoard() As Integer()

		Return m_aintPlayerMoves

	End Function



	' -------------------------------------------------------------------------
	' Name: SetBoard
	' Abstract: sets the board
	' -------------------------------------------------------------------------
	Public Sub SetBoard(ByVal aintNewBoard() As Integer)

		Try

			m_aintPlayerMoves = aintNewBoard

		Catch excError As Exception

			WriteLog(excError.ToString)

		End Try

	End Sub


	' -------------------------------------------------------------------------
	' Name: SetWinner
	' Abstract: Sets The Winner
	' -------------------------------------------------------------------------
	Public Sub SetWinner(ByVal intWinner As Integer)

		Try

			If intWinner < 0 Then intWinner = 0
			If intWinner > 3 Then intWinner = 3

			m_intWhoWon = intWinner

		Catch excError As Exception

			WriteLog(excError.ToString)

		End Try

	End Sub



	' -------------------------------------------------------------------------
	' Name: GetWinner
	' Abstract: Returns the Winner
	' -------------------------------------------------------------------------
	Public Function GetWinner() As Integer

		Return m_intWhoWon

	End Function

#End Region

End Class
