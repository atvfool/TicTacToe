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



Public Class CSuperMove


    ' -------------------------------------------------------------------------
    ' Form variables
    ' -------------------------------------------------------------------------
    Private m_aclsMoves(6) As CMove


    ' -------------------------------------------------------------------------
    ' Name: New
    ' Abstract: constructor
    ' -------------------------------------------------------------------------
    Public Sub New(ByVal intJointA As Integer, _
                   ByVal intJointB As Integer, _
                   ByVal intJointC As Integer, _
                   ByVal intJointD As Integer, _
                   ByVal intJointE As Integer, _
                   ByVal intJointF As Integer)

        Try

            m_aclsMoves(1) = New CMove(enuJointType.JointA, intJointA)
            m_aclsMoves(2) = New CMove(enuJointType.JointB, intJointB)
            m_aclsMoves(3) = New CMove(enuJointType.JointC, intJointC)
            m_aclsMoves(4) = New CMove(enuJointType.JointD, intJointD)
            m_aclsMoves(5) = New CMove(enuJointType.JointE, intJointE)
            m_aclsMoves(6) = New CMove(enuJointType.JointF, intJointF)

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
            Dim blnMoveForwardComplete As Boolean = False

            Do

                For intIndex = 1 To 6

                    m_aclsMoves(intIndex).StepForwardWithoutWait()

                Next

                blnMoveForwardComplete = m_aclsMoves(1).IsMoveForwardComplete() _
                                    And m_aclsMoves(2).IsMoveForwardComplete() _
                                    And m_aclsMoves(3).IsMoveForwardComplete() _
                                    And m_aclsMoves(4).IsMoveForwardComplete() _
                                    And m_aclsMoves(5).IsMoveForwardComplete() _
                                    And m_aclsMoves(6).IsMoveForwardComplete()

            Loop While blnMoveForwardComplete = False

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
            Dim blnMoveBackwardComplete As Boolean = False

            Do

                For intIndex = 6 To 1 Step -1

                    m_aclsMoves(intIndex).StepBackwardWithoutWait()

                Next

                blnMoveBackwardComplete = m_aclsMoves(1).IsMoveBackwardComplete() _
                                    And m_aclsMoves(2).IsMoveBackwardComplete() _
                                    And m_aclsMoves(3).IsMoveBackwardComplete() _
                                    And m_aclsMoves(4).IsMoveBackwardComplete() _
                                    And m_aclsMoves(5).IsMoveBackwardComplete() _
                                    And m_aclsMoves(6).IsMoveBackwardComplete()

            Loop While blnMoveBackwardComplete = False


        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub




End Class
