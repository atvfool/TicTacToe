' -------------------------------------------------------------------------
' Module: FTicTacToe
' Author: Andrew Hayden
' Abstract: Tic tac toe game
'
' Revision        Owner   Changes:
' 1  2013/05/29   A.H.    Created.
' -------------------------------------------------------------------------

' -------------------------------------------------------------------------
' Options
' -------------------------------------------------------------------------
Option Explicit On

' -------------------------------------------------------------------------
' Imports
' -------------------------------------------------------------------------

Public Class FTicTacToe

    ' -------------------------------------------------------------------------
    ' Form Vairables
    ' -------------------------------------------------------------------------
    Private f_clsGame As CTicTacToeGame
	Private f_strPlayerSymbol As String = "X"
	Private f_strComputerSymbol As String = "O"

    ' 0 = Game isn't over, 1 = Player, 2 = CPU, 3 = Tie
    Private f_intWinner As Integer
    

    ' -------------------------------------------------------------------------
    ' Name: <name>
    ' Abstract: <description>
    ' -------------------------------------------------------------------------

#Region "Events"



    ' -------------------------------------------------------------------------
    ' Name: FTicTacToe_Load
    ' Abstract: Readies a game of tic-tac-toe on the easy difficulty
    ' -------------------------------------------------------------------------
    Private Sub FTicTacToe_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            InitializeTicTacToeGame()
            ToggleFormLock(True, True)

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: NewGameToolStripMenuItem_Click
    ' Abstract: Places the computer move
    ' -------------------------------------------------------------------------
    Private Sub NewGameToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewGameToolStripMenuItem.Click

        Try

            InitializeTicTacToeGame()
            ToggleFormLock(True, True)


        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: ViewRecentGamesToolStripMenuItem_Click
    ' Abstract: Opens the recent games dialog
    ' -------------------------------------------------------------------------
    Private Sub ViewRecentGamesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewRecentGamesToolStripMenuItem.Click

        Try

            Dim daRecentGames As New FRecentGames
            daRecentGames.Show(Me)

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: btn1_Click
    ' Abstract: Will place a players piece in this spot if there isn't one already
    ' -------------------------------------------------------------------------
    Private Sub btn1_Click(sender As Object, e As EventArgs) Handles btn1.Click

        Try

            If DoMove(0, 1) = True Then

                btn1.Enabled = False
                btn1.Text = f_strPlayerSymbol

            End If

            If CheckBoard() = 0 Then

                PlaceComputerMove()

            End If

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: btn2_Click
    ' Abstract: Will place a players piece in this spot if there isn't one already
    ' -------------------------------------------------------------------------
    Private Sub btn2_Click(sender As Object, e As EventArgs) Handles btn2.Click

        Try

            If DoMove(1, 1) = True Then

                btn2.Enabled = False
                btn2.Text = f_strPlayerSymbol

            End If

            If CheckBoard() = 0 Then

                PlaceComputerMove()

            End If

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: btn3_Click
    ' Abstract: Will place a players piece in this spot if there isn't one already
    ' -------------------------------------------------------------------------
    Private Sub btn3_Click(sender As Object, e As EventArgs) Handles btn3.Click

        Try

            If DoMove(2, 1) = True Then

                btn3.Enabled = False
                btn3.Text = f_strPlayerSymbol

            End If

            If CheckBoard() = 0 Then

                PlaceComputerMove()

            End If

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: btn4_Click
    ' Abstract: Will place a players piece in this spot if there isn't one already
    ' -------------------------------------------------------------------------
    Private Sub btn4_Click(sender As Object, e As EventArgs) Handles btn4.Click

        Try

            If DoMove(3, 1) = True Then

                btn4.Enabled = False
                btn4.Text = f_strPlayerSymbol


            End If

            If CheckBoard() = 0 Then

                PlaceComputerMove()

            End If

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: btn5_Click
    ' Abstract: Will place a players piece in this spot if there isn't one already
    ' -------------------------------------------------------------------------
    Private Sub btn5_Click(sender As Object, e As EventArgs) Handles btn5.Click

        Try

            If DoMove(4, 1) = True Then

                btn5.Enabled = False
                btn5.Text = f_strPlayerSymbol

            End If

            If CheckBoard() = 0 Then

                PlaceComputerMove()

            End If

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: btn6_Click
    ' Abstract: Will place a players piece in this spot if there isn't one already
    ' -------------------------------------------------------------------------
    Private Sub btn6_Click(sender As Object, e As EventArgs) Handles btn6.Click

        Try

            If DoMove(5, 1) = True Then

                btn6.Enabled = False
                btn6.Text = f_strPlayerSymbol

            End If

            If CheckBoard() = 0 Then

                PlaceComputerMove()

            End If

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: btn7_Click
    ' Abstract: Will place a players piece in this spot if there isn't one already
    ' -------------------------------------------------------------------------
    Private Sub btn7_Click(sender As Object, e As EventArgs) Handles btn7.Click

        Try

            If DoMove(6, 1) = True Then

                btn7.Enabled = False
                btn7.Text = f_strPlayerSymbol

            End If

            If CheckBoard() = 0 Then

                PlaceComputerMove()

            End If

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: btn8_Click
    ' Abstract: Will place a players piece in this spot if there isn't one already
    ' -------------------------------------------------------------------------
    Private Sub btn8_Click(sender As Object, e As EventArgs) Handles btn8.Click

        Try

            If DoMove(7, 1) = True Then

                btn8.Enabled = False
                btn8.Text = f_strPlayerSymbol

            End If

            If CheckBoard() = 0 Then

                PlaceComputerMove()

            End If

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: btn9_Click
    ' Abstract: Will place a players piece in this spot if there isn't one already
    ' -------------------------------------------------------------------------
    Private Sub btn9_Click(sender As Object, e As EventArgs) Handles btn9.Click

        Try

            If DoMove(8, 1) = True Then

                btn9.Enabled = False
                btn9.Text = f_strPlayerSymbol

            End If

            If CheckBoard() = 0 Then

                PlaceComputerMove()

            End If

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub

#End Region



    ' -------------------------------------------------------------------------
    ' Name: DoMove
    ' Abstract: puts the move on the board virtually, mechanically and records the DB
    ' -------------------------------------------------------------------------
    Public Function DoMove(ByVal intSquare As Integer, _
                           ByVal intPlayer As Integer) As Boolean

        Dim blnResult As Boolean = False

        Try

            If PlaceMoveOnBoard(intSquare, intPlayer) = True Then

                ' Code to move Robot
                blnResult = True

            End If

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

        Return blnResult

    End Function


    ' -------------------------------------------------------------------------
    ' Name: PlaceMoveOnBoard
    ' Abstract: Checks if your move is valid and places it, return true if succesful
    ' -------------------------------------------------------------------------
    Private Function PlaceMoveOnBoard(ByVal intSquare As Integer, _
                                      ByVal intPlayer As Integer) As Boolean

        Dim blnResult As Boolean = False

        Try

            If f_clsGame.PlaceMove(intSquare, intPlayer) = True Then

                ' Code to move robot and record to DB go here
                blnResult = True

            End If

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

        Return blnResult

    End Function



    ' -------------------------------------------------------------------------
    ' Name: CheckBoard
    ' Abstract: Checks if the game is over, 0 = no, 1 = player win, 2 = 
    '           cpu win, 3 =  no winner/draw
    ' -------------------------------------------------------------------------
    Private Function CheckBoard() As Integer

        Dim intResult As Integer = 0

        Try

            If f_clsGame.IsGameOver() = True Then

                intResult = f_clsGame.GetWinner()
                ToggleFormLock(False, False)

            End If

            Select Case intResult
                Case 1
                    MessageBox.Show("You Won!")
                Case 2
                    MessageBox.Show("Computer Won!")
                Case 3
                    MessageBox.Show("Draw!")
                Case Else
                    ' Nothing
            End Select

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

        Return intResult

    End Function



    ' -------------------------------------------------------------------------
    ' Name: LockGamePlay
    ' Abstract: Disables all of the gameplay buttons
    ' -------------------------------------------------------------------------
    Private Sub ToggleFormLock(ByVal blnLock As Boolean, ByVal blnClearText As Boolean)

        Try

            Dim objControl As Control

            For Each objControl In Me.Controls

                If TypeOf objControl Is Button Then

                    objControl.Enabled = blnLock

                    If blnClearText = True Then

                        objControl.Text = String.Empty

                    End If

                End If

            Next

            If f_clsGame.GetWhoMovesFirst = False And blnClearText = True Then
                PlaceComputerMove()
            End If

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: PlaceComputerMove
    ' Abstract: Places the computer move
    ' -------------------------------------------------------------------------
    Public Sub PlaceComputerMove()

        Try

            Dim intComputerMove As Integer

            intComputerMove = f_clsGame.ComputerPlaceMove

            ' Row 1
            If intComputerMove = 0 Then
                ' btn1
                btn1.Enabled = False
                btn1.Text = f_strComputerSymbol
            ElseIf intComputerMove = 1 Then
                ' btn2
                btn2.Enabled = False
                btn2.Text = f_strComputerSymbol
            ElseIf intComputerMove = 2 Then
                ' btn3
                btn3.Enabled = False
                btn3.Text = f_strComputerSymbol
                ' Row 2
            ElseIf intComputerMove = 3 Then
                ' btn4
                btn4.Enabled = False
                btn4.Text = f_strComputerSymbol
            ElseIf intComputerMove = 4 Then
                ' btn5
                btn5.Enabled = False
                btn5.Text = f_strComputerSymbol
            ElseIf intComputerMove = 5 Then
                ' btn6
                btn6.Enabled = False
                btn6.Text = f_strComputerSymbol
                ' Row 3
            ElseIf intComputerMove = 6 Then
                ' btn 7
                btn7.Enabled = False
                btn7.Text = f_strComputerSymbol
            ElseIf intComputerMove = 7 Then
                ' btn8
                btn8.Enabled = False
                btn8.Text = f_strComputerSymbol
            ElseIf intComputerMove = 8 Then
                ' btn9
                btn9.Enabled = False
                btn9.Text = f_strComputerSymbol
            End If

            CheckBoard()

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: NewGameToolStripMenuItem_Click
    ' Abstract: Places the computer move
    ' -------------------------------------------------------------------------
    Private Sub InitializeTicTacToeGame()

        Try

            Dim strPlayersName As String = String.Empty
            Dim blnWhoMovesFirst As Boolean = True
            Dim intGameDifficultyID As Integer = 1
            ' Get the players name
            Do

                strPlayersName = InputBox("Enter Player's Name", "Player's Name")

            Loop While Trim(strPlayersName) = String.Empty

            Dim dlgResult As DialogResult

            ' Get who moves first
            dlgResult = MessageBox.Show("Who Moves First?" & vbNewLine & "Yes: Player, No: Computer", "Who moves first?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If dlgResult = Windows.Forms.DialogResult.Yes Then
                blnWhoMovesFirst = True
            ElseIf dlgResult = Windows.Forms.DialogResult.No Then
                blnWhoMovesFirst = False
            End If

            ' Get The difficulty to play on
            dlgResult = MessageBox.Show("What Difficulty?" & vbNewLine & "Yes: Easy, No: Medium, Cancel: Hard", "Difficulty", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

            If dlgResult = Windows.Forms.DialogResult.Yes Then
                intGameDifficultyID = 1
            ElseIf dlgResult = Windows.Forms.DialogResult.No Then
                intGameDifficultyID = 2
            ElseIf dlgResult = Windows.Forms.DialogResult.Cancel Then
                intGameDifficultyID = 3
            End If

            f_clsGame = New CTicTacToeGame(strPlayersName, blnWhoMovesFirst, intGameDifficultyID)

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub

End Class