' -------------------------------------------------------------------------
' Class: CDatabaseUtilities
' Author: Andrew Hayden
' Abstract: Database Class
'
' Revision        Owner   Changes:
' 1  2013/08/01   A.H.    Created.
' -------------------------------------------------------------------------

' -------------------------------------------------------------------------
' Options
' -------------------------------------------------------------------------
Option Explicit On

' -------------------------------------------------------------------------
' Imports
' -------------------------------------------------------------------------
Imports System.Data.SqlClient
Imports System.Data

Public Class CDatabaseUtilites

	' -------------------------------------------------------------------------
	' Class Properties
	' -------------------------------------------------------------------------
	Private m_connDatabaseConnection As SqlConnection



	' -------------------------------------------------------------------------
	' Name: New
	' Abstract: Constructor
	' -------------------------------------------------------------------------
	Public Sub New()

		Try

			m_connDatabaseConnection = New SqlConnection
			m_connDatabaseConnection.ConnectionString = "Server=(local);Database=dbTicTacToe;User Id=TicTacToeUser;Password=password;"

			m_connDatabaseConnection.Open()

		Catch excError As Exception

			WriteLog(excError.ToString)

		End Try

	End Sub



	' -------------------------------------------------------------------------
	' Name: RunQueryOnDatabase
	' Abstract: runs a query on the database
	' -------------------------------------------------------------------------
	Public Function RunQueryOnDatabase(ByVal strSQLStatement As String, Optional ByRef colParameters As CParameterCollection = Nothing) As DataTable

		Dim dtTable As New DataTable

		Try

			Dim cmdQuery As New SqlCommand
			cmdQuery.Connection = m_connDatabaseConnection
			cmdQuery.CommandText = strSQLStatement
			cmdQuery.CommandType = CommandType.Text
			AddParametersToCommand(cmdQuery, colParameters)

			Dim daAdapter As New SqlDataAdapter
			daAdapter.SelectCommand = cmdQuery

			daAdapter.Fill(dtTable)

		Catch excError As Exception

			WriteLog(excError.ToString)

		End Try

		Return dtTable

	End Function



	' -------------------------------------------------------------------------
	' Name: RunStoredProcedure
	' Abstract: runs a stored procedure on the database
	' -------------------------------------------------------------------------
	Public Function RunStoredProcedure(ByVal strStoredProcedure As String, Optional ByRef colParameters As CParameterCollection = Nothing) As DataTable

		Dim dtTable As New DataTable

		Try

			Dim cmdQuery As New SqlCommand
			cmdQuery.Connection = m_connDatabaseConnection
			cmdQuery.CommandText = strStoredProcedure
			cmdQuery.CommandType = CommandType.StoredProcedure
			AddParametersToCommand(cmdQuery, colParameters)

			Dim daAdapter As New SqlDataAdapter
			daAdapter.SelectCommand = cmdQuery

			daAdapter.Fill(dtTable)

			colParameters.Clear()

		Catch excError As Exception

			WriteLog(excError.ToString)

		End Try

		Return dtTable

	End Function



	' -------------------------------------------------------------------------
	' Name: AddParametersToCommand
	' Abstract: adds parameters to a command
	' -------------------------------------------------------------------------
	Public Sub AddParametersToCommand(ByRef cmdCommand As SqlCommand, ByRef colParameters As CParameterCollection)

		Try

			For Each clsParameter As CParameter In colParameters.colParameterCollection

				cmdCommand.Parameters.Add(clsParameter.ReturnParameter)

			Next

		Catch excError As Exception

			WriteLog(excError.ToString)

		End Try

	End Sub



	' -------------------------------------------------------------------------
    ' Name: Dispose
    ' Abstract: Makes sure the connection is closed
	' -------------------------------------------------------------------------
	Public Sub Dispose()

		Try

			m_connDatabaseConnection.Close()

		Catch excError As Exception

			WriteLog(excError.ToString)

		End Try

	End Sub



    ' -------------------------------------------------------------------------
    ' Name: AddMoveToGame
    ' Abstract: Adds a move to game
    ' -------------------------------------------------------------------------
    Public Function AddMoveToGame(ByRef ParameterCollection As CParameterCollection) As Boolean

        Dim blnResult As Boolean = False

        Try

            Dim dtResultsTable As DataTable
            dtResultsTable = RunStoredProcedure("spInsertNewMove", ParameterCollection)

            If dtResultsTable.Rows.Count > 0 Then
                blnResult = True
            End If

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

        Return blnResult

    End Function



    ' -------------------------------------------------------------------------
    ' Name: AddMoveToGame
    ' Abstract: Adds a move to game
    ' -------------------------------------------------------------------------
    Public Function UpdateGameWinner(ByVal intGameID As Integer, ByVal intWinnerID As Integer) As Boolean

        Dim blnResult As Boolean = False

        Try

            Dim clsGameParameters As New CParameterCollection
            clsGameParameters.Add("@intGameID", DbType.Int32, intGameID)
            clsGameParameters.Add("@intGameOutcomeID", DbType.Int32, intWinnerID)

            RunStoredProcedure("spUpdateWhoWon", clsGameParameters)

            blnResult = True

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

        Return blnResult

    End Function

End Class
