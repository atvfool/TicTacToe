Public Class FRecentGames



    ' -------------------------------------------------------------------------
    ' Name: btnClose_Click
    ' Abstract: Closes the form
    ' -------------------------------------------------------------------------
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click

        Try

            Me.Close()

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: FRecentGames_Load
    ' Abstract: Loads all the games into the Listbox
    ' -------------------------------------------------------------------------
    Private Sub FRecentGames_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            Dim dtTicTacToeGames As DataTable
            Dim clsTicTacToe As New CDatabaseUtilites
            Dim lstGame As CListItem

            Dim clsParameters As New CParameterCollection

            dtTicTacToeGames = clsTicTacToe.RunStoredProcedure("spGetRecentGames", clsParameters)

            For Each Row As DataRow In dtTicTacToeGames.Rows

                lstGame = New CListItem(Val(Row("intGameID").ToString), Row("strPlayerName").ToString & "(" & Row("dtmPlayed").ToString & ")")
                lstGame.SetID(Val(Row("intGameID").ToString))
                lstGame.SetName(Row("strPlayerName").ToString & "(" & Row("dtmPlayed").ToString & ")")
                lstRecentGames.Items.Add(lstGame.GetName)
                
            Next

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub

End Class