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
    Private m_aaintPlayerMoves(2, 2) As Integer ' 0 = Empty, 1 = player, 2 = cpu
    ' Decides if the player or cpu goes first, 1 = player, 2 = CPU
	Private m_blnWhoMovesFirst As Boolean = True
	' Decices what piece the player uses, True = Player, False = Computer
    Private m_intPlayerPiece As Integer
    ' 0 = Not Over, 1 = Player, 2 = CPU, 3 = Tie
    Private m_intWhoWon As Integer
    ' 1 = Easy, 2 = Medium, 3 = Hard
	Private m_intDifficulty As Integer = 3
	Private m_enuGridEntries(8) As enuGridEntry
	Private m_intScore As Integer
	Private m_blnGameOver As Boolean



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

            Dim intColumnIndex As Integer = 0
            Dim intRowIndex As Integer = 0

            For intColumnIndex = m_aaintPlayerMoves.GetLowerBound(0) To m_aaintPlayerMoves.GetUpperBound(0)

                For intRowIndex = m_aaintPlayerMoves.GetLowerBound(1) To m_aaintPlayerMoves.GetUpperBound(1)

					m_aaintPlayerMoves(intColumnIndex, intRowIndex) = -1

                Next

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
    Public Function PlaceMove(ByVal intRows As Integer, ByVal intColumns As Integer, ByVal intPlayer As Integer) As Boolean

        Dim blnResult As Boolean = False

        Try

            If IsGameOver() = False Then

                If CheckMove(intRows, intColumns) = True Then

                    SetPiece(intRows, intColumns, intPlayer)

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
    Private Function CheckMove(ByVal intRows As Integer, ByVal intColumns As Integer) As Boolean

        Dim blnResult As Boolean = False

        Try

			If GetPiece(intRows, intColumns) = 0 Then

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
            If GetPiece(0, 0) = intPlayerType And _
               GetPiece(0, 1) = intPlayerType And _
               GetPiece(0, 2) = intPlayerType Then

                blnResult = True

            ElseIf GetPiece(1, 0) = intPlayerType And _
                   GetPiece(1, 1) = intPlayerType And _
                   GetPiece(1, 2) = intPlayerType Then

                blnResult = True

            ElseIf GetPiece(2, 0) = intPlayerType And _
                   GetPiece(2, 1) = intPlayerType And _
                   GetPiece(2, 2) = intPlayerType Then

                blnResult = True

                ' Check all the columns
            ElseIf GetPiece(0, 0) = intPlayerType And _
                   GetPiece(1, 0) = intPlayerType And _
                   GetPiece(1, 0) = intPlayerType Then

                blnResult = True

            ElseIf GetPiece(0, 1) = intPlayerType And _
                   GetPiece(1, 1) = intPlayerType And _
                   GetPiece(2, 1) = intPlayerType Then

                blnResult = True

            ElseIf GetPiece(0, 2) = intPlayerType And _
                   GetPiece(1, 2) = intPlayerType And _
                   GetPiece(2, 2) = intPlayerType Then

                blnResult = True

                ' Diagonal
            ElseIf GetPiece(0, 0) = intPlayerType And _
                   GetPiece(1, 1) = intPlayerType And _
                   GetPiece(2, 2) = intPlayerType Then

                blnResult = True

            ElseIf GetPiece(0, 2) = intPlayerType And _
                   GetPiece(1, 1) = intPlayerType And _
                   GetPiece(2, 0) = intPlayerType Then

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
			If GetPiece(0, 0) = -1 Or _
				   GetPiece(0, 1) = -1 Or _
				   GetPiece(0, 2) = -1 Or _
				   GetPiece(1, 0) = -1 Or _
				   GetPiece(1, 1) = -1 Or _
				   GetPiece(1, 2) = -1 Or _
				   GetPiece(2, 0) = -1 Or _
				   GetPiece(2, 1) = -1 Or _
				   GetPiece(2, 2) = -1 Then
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
	Public Function ComputerPlaceMove() As Integer()

		Dim intComputerMove(2) As Integer

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
	Private Function EasyMove() As Integer()

		Dim intComputerMove(2) As Integer

		Try

			Dim rndNumber As Random
			rndNumber = New Random
			Dim intRow As Integer
			Dim intColumn As Integer

			Do

				intRow = rndNumber.Next(0, 3)
				intColumn = rndNumber.Next(0, 3)

			Loop Until PlaceMove(intRow, intColumn, 2) = True

			intComputerMove(0) = intRow
			intComputerMove(1) = intColumn

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

				intRecursiveScore = m_intScore
				Exit Try
			End If

			For Each clsChildBoard As CTicTacToeGame In GetChildren()

				Dim clsTest As CTicTacToeGame
				intScore = 0
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



	' -------------------------------------------------------------------------
	' Name: GetChildren
	' Abstract: returns the current board
	' -------------------------------------------------------------------------	
	Public Function GetChildren() As Collection

		Dim aclsReturn As New Collection

		Try

			Dim intClassCount As Integer = 0

			For intIndex As Integer = 0 To m_enuGridEntries.Length - 1

				If m_enuGridEntries(intIndex) = enuGridEntry.Empty Then

					Dim enuNewValues As enuGridEntry() = DirectCast(m_enuGridEntries.Clone(), enuGridEntry())
					enuNewValues(intIndex) = m_enuGridEntries(intIndex)
					aclsReturn.Add(New CTicTacToeGame(enuNewValues, Not m_blnWhoMovesFirst))
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
	Private Function FindNextMove(ByVal intDepth As Integer) As Integer()

		Dim aintNewCoordinates() As Integer = {-1, -1}

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
	Private Function GetNextMoveFromNewBoard(ByRef clsNewBoard As CTicTacToeGame) As Integer()

		Dim aintNewMoveCoordinates() As Integer = {-1, -1}

		Try

			For intRow = m_aaintPlayerMoves.GetLowerBound(0) To m_aaintPlayerMoves.GetUpperBound(0)

				For intColumn = m_aaintPlayerMoves.GetLowerBound(1) To m_aaintPlayerMoves.GetUpperBound(1)

					' Compare each piece
					If Me.GetPiece(intRow, intColumn) <> clsNewBoard.GetPiece(intRow, intColumn) Then

						aintNewMoveCoordinates(0) = intRow
						aintNewMoveCoordinates(1) = intColumn

						Exit For

					End If

				Next

				' If a piece was set
				If aintNewMoveCoordinates(0) <> -1 Then
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
	Private Function HardMove() As Integer()

		Dim aintHardMove As Integer() = {-1, -1}

		Try

			Do

				aintHardMove = FindNextMove(8)

			Loop While aintHardMove(0) = -1 Or aintHardMove(1) = -1





		Catch excError As Exception

			WriteLog(excError.ToString)

		End Try

		Return aintHardMove

	End Function





#Region "Get / Set Methods"



	' -------------------------------------------------------------------------
	' Name: GetPiece
	' Abstract: Returns the piece
	' -------------------------------------------------------------------------
	Public Function GetPiece(ByVal intRow As Integer, ByVal intColumn As Integer) As Integer

		Dim intResult As Integer = 0

		Try

			If intRow > 2 Then intRow = 2
			If intRow < 0 Then intRow = 0
			If intColumn > 2 Then intColumn = 2
			If intColumn < 0 Then intColumn = 0

			intResult = m_aaintPlayerMoves(intRow, intColumn)

		Catch excError As Exception

			WriteLog(excError.ToString)

		End Try

		Return intResult

	End Function



	' -------------------------------------------------------------------------
	' Name: SetPiece
	' Abstract: Sets the piece
	' -------------------------------------------------------------------------
	Public Sub SetPiece(ByVal intRow As Integer, ByVal intColumn As Integer, ByVal intPlayer As Integer)

		Try

			If intRow > 2 Then intRow = 2
			If intRow < 0 Then intRow = 0
			If intColumn > 2 Then intColumn = 2
			If intColumn < 0 Then intColumn = 0
			If intPlayer < 0 Then intPlayer = 0
			If intPlayer > 2 Then intPlayer = 2

			m_aaintPlayerMoves(intRow, intColumn) = intPlayer

		Catch excError As Exception

			WriteLog(excError.ToString)

		End Try

	End Sub



	' -------------------------------------------------------------------------
	' Name: GetBoard
	' Abstract: Returns the board
	' -------------------------------------------------------------------------
	Public Function GetBoard() As Integer(,)

		Return m_aaintPlayerMoves

	End Function



	' -------------------------------------------------------------------------
	' Name: SetBoard
	' Abstract: sets the board
	' -------------------------------------------------------------------------
	Public Sub SetBoard(ByVal aaintNewBoard(,) As Integer)

		Try

			m_aaintPlayerMoves = aaintNewBoard

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
