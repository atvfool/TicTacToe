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


Public Class CParameterCollection

	Private m_colParameterCollection As Collection

	Public ReadOnly Property colParameterCollection As Collection
		Get
			Return m_colParameterCollection
		End Get
	End Property


	' -------------------------------------------------------------------------
	' Name: New
	' Abstract: Constructor
	' -------------------------------------------------------------------------
	Public Sub New()

		Try

			m_colParameterCollection = New Collection

		Catch excError As Exception

			WriteLog(excError.ToString)

		End Try

	End Sub



	' -------------------------------------------------------------------------
	' Name: Add
	' Abstract: Adds a parameter to the collection
	' -------------------------------------------------------------------------
    Public Sub Add(ByVal strParameterName As String, ByVal pdtParameterDataType As DbType, ByVal varParameterValue As Object)

        Try

            Dim clsParameter As New CParameter(strParameterName, pdtParameterDataType, varParameterValue)
            m_colParameterCollection.Add(clsParameter)

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub



	' -------------------------------------------------------------------------
	' Name: Clear
	' Abstract: Clears all parameters
	' -------------------------------------------------------------------------
	Public Sub Clear()

		Try

			m_colParameterCollection.Clear()

		Catch excError As Exception

			WriteLog(excError.ToString)

		End Try

	End Sub



End Class
