' -------------------------------------------------------------------------
' Class: CParameter
' Author: Andrew Hayden
' Abstract: Parameter Class
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
Imports System.Data


Public Class CParameter

	' -------------------------------------------------------------------------
	' Class Properties
	' -------------------------------------------------------------------------
	Private m_strParameterName As String
	Private m_pdtParameterDataType As SqlDbType
	Private m_varParameterValue As Object



	' -------------------------------------------------------------------------
	' Name: New
	' Abstract: Constructor
	' -------------------------------------------------------------------------
    Public Sub New(ByVal strParameterName As String, ByVal pdtParameterDataType As DbType, ByVal varParameterValue As Object)

        Try

            m_strParameterName = strParameterName
            m_pdtParameterDataType = pdtParameterDataType
            m_varParameterValue = varParameterValue

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub



	' -------------------------------------------------------------------------
	' Name: ReturnParameter
	' Abstract: Returns a parameter
	' -------------------------------------------------------------------------
	Public Function ReturnParameter() As SqlClient.SqlParameter

		Dim prmParameter As New SqlClient.SqlParameter

		Try

			prmParameter.DbType = m_pdtParameterDataType
			prmParameter.ParameterName = m_strParameterName

			Select Case m_pdtParameterDataType

				Case SqlDbType.Int

					Integer.TryParse(m_varParameterValue, prmParameter.Value)

				Case SqlDbType.NVarChar

					prmParameter.Value = CStr(m_varParameterValue)

				Case SqlDbType.Bit

					Boolean.TryParse(m_varParameterValue, prmParameter.Value)

				Case Else

					prmParameter.Value = m_varParameterValue

			End Select

		Catch excError As Exception

			WriteLog(excError.ToString)

		End Try

		Return prmParameter

	End Function




End Class
