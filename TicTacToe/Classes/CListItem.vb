' --------------------------------------------------------------------------------
' Name: CListItem
' Abstract: stores list items
' --------------------------------------------------------------------------------

' --------------------------------------------------------------------------------
' Imports
' --------------------------------------------------------------------------------

Public Class CListItem

    ' --------------------------------------------------------------------------------
    ' Variables
    ' --------------------------------------------------------------------------------
    Dim m_intID As Integer
    Dim m_strName As Integer



    ' --------------------------------------------------------------------------------
    ' Name: SetID
    ' Abstract: sets the id
    ' --------------------------------------------------------------------------------
    Public Sub SetID(ByVal intID As Integer)

        Try

            m_intID = intID

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub



    ' --------------------------------------------------------------------------------
    ' Name: SetName
    ' Abstract: Sets the name
    ' --------------------------------------------------------------------------------
    Public Sub SetName(ByVal strName As String)

        Try

            m_strName = strName

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub



    ' --------------------------------------------------------------------------------
    ' Name: GetID
    ' Abstract: Gets the ID
    ' --------------------------------------------------------------------------------
    Public Function GetID() As Integer

        Return m_intID

    End Function



    ' --------------------------------------------------------------------------------
    ' Name: GetName
    ' Abstract: Gets teh name
    ' --------------------------------------------------------------------------------
    Public Function GetName() As String

        Return m_strName

    End Function



    ' --------------------------------------------------------------------------------
    ' Name: New
    ' Abstract: Makes a new ListItem and sets ID and Name
    ' --------------------------------------------------------------------------------
    Public Sub New(ByVal intID As Integer, ByVal strString As String)

        Try

            m_intID = intID
            m_strName = m_strName

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub



    ' --------------------------------------------------------------------------------
    ' Name: ToString
    ' Abstract: Overrides the ToString method
    ' --------------------------------------------------------------------------------
    Public Overrides Function ToString() As String

        Dim strStringToReturn As String

        Try

            strStringToReturn = m_intID & " - " & m_strName

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

        Return strStringToReturn

    End Function

End Class
