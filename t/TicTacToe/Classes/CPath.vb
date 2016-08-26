' -------------------------------------------------------------------------
' Class: CSuperMove
' Author: Andrew Hayden
' Abstract: Moves every joint their own distance
'
' Revision        Owner   Changes:
' 1  2013/06/13   A.H.    Created.
' -------------------------------------------------------------------------

' -------------------------------------------------------------------------
' Options
' -------------------------------------------------------------------------
Option Explicit On

' -------------------------------------------------------------------------
' Imports
' -------------------------------------------------------------------------



Public Class CPath


    ' -------------------------------------------------------------------------
    ' Form variables
    ' -------------------------------------------------------------------------
    Private m_aclsSuperMove(0) As CSuperMove
    Private m_intSuperMoveCount As Integer = 0


    ' -------------------------------------------------------------------------
    ' Name: New
    ' Abstract: constructor
    ' -------------------------------------------------------------------------
    Public Sub New()

        Try

            ReDim m_aclsSuperMove(m_intSuperMoveCount)

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub


    ' -------------------------------------------------------------------------
    ' Name: New
    ' Abstract: constructor
    ' -------------------------------------------------------------------------
    Public Sub AddSuperMoveToPath(ByVal intJointA As Integer, _
                                   ByVal intJointB As Integer, _
                                   ByVal intJointC As Integer, _
                                   ByVal intJointD As Integer, _
                                   ByVal intJointE As Integer, _
                                   ByVal intJointF As Integer)

        Try

            Dim clsSuperMove As CSuperMove

            clsSuperMove = New CSuperMove(intJointA, intJointB, intJointC, intJointD, intJointE, intJointF)

            AddSuperMoveToPath(clsSuperMove)

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: AddSuperMoveToPath
    ' Abstract: constructor
    ' -------------------------------------------------------------------------
    Public Sub AddSuperMoveToPath(clsSuperMove As CSuperMove)

        Try

            ' Count it
            m_intSuperMoveCount += 1

            ' Make space
            ReDim Preserve m_aclsSuperMove(m_intSuperMoveCount)

            ' Add it
            m_aclsSuperMove(m_intSuperMoveCount) = clsSuperMove

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub


    ' -------------------------------------------------------------------------
    ' Name: MoveForward
    ' Abstract: moves all the joints forward
    ' -------------------------------------------------------------------------
    Public Sub MoveForward()

        Try

            Dim intIndex As Integer

            For intIndex = 1 To m_intSuperMoveCount

                m_aclsSuperMove(intIndex).MoveForward()

            Next

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub


    ' -------------------------------------------------------------------------
    ' Name: MoveBackward
    ' Abstract: moves all the joints forward
    ' -------------------------------------------------------------------------
    Public Sub MoveBackward()

        Try

            Dim intIndex As Integer

            For intIndex = m_intSuperMoveCount To 1 Step -1

                m_aclsSuperMove(intIndex).MoveBackward()

            Next

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub


End Class
