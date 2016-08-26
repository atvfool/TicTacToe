' -------------------------------------------------------------------------
' Module: FMain
' Author: Patrick Callahan
' Abstract: Start application for SET Project class.
'
' Revision        Owner   Changes:
' 1  2006/05/01   P.C.    Created.
' -------------------------------------------------------------------------

' -------------------------------------------------------------------------
' Options
' -------------------------------------------------------------------------
Option Explicit On

' -------------------------------------------------------------------------
' Imports
' -------------------------------------------------------------------------
Imports System.IO.Ports

Public Class FMain

    ' -------------------------------------------------------------------------
    ' Constants
    ' -------------------------------------------------------------------------
    ' Break large moves down into smaller steps
    Public Const intMAXIMUM_STEP As Integer = 100


    ' -------------------------------------------------------------------------
    ' User Defined Types
    ' -------------------------------------------------------------------------
    Public Enum enuJointType

        JointA = 1
        JointB = 2
        JointC = 3
        JointD = 4
        JointE = 5
        JointF = 6

    End Enum


    ' -------------------------------------------------------------------------
    ' Form variables
    ' -------------------------------------------------------------------------



#Region "Form Open / Close"

    ' -------------------------------------------------------------------------
    ' Name: FMain_Load
    ' Abstract: Open a com port to talk to the robot controller.  If you use
    '           the USB interface the com port may be any number from 1-9.  If
    '           you plug the cable into a different USB slot the com port can
    '           change.
    ' -------------------------------------------------------------------------
    Private Sub FMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            modRobotUtilites.OpenRobotConnection()

        Catch excError As Exception

            ' Display error message
            WriteLog(excError.ToString)

        End Try

    End Sub


    ' -------------------------------------------------------------------------
    ' Name: FMain_FormClosing
    ' Abstract: Close com1
    ' -------------------------------------------------------------------------
    Private Sub FMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Try

            modRobotUtilites.CloseRobotConnection()

        Catch excError As Exception

            ' Display error message
            WriteLog(excError.ToString)

        End Try

    End Sub

#End Region

#Region "In / Out"

    ' -------------------------------------------------------------------------
    ' Name: btnIn_Click
    ' Abstract: Move the arm in
    ' -------------------------------------------------------------------------
    Private Sub btnIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIn.Click

        Try

            MoveJoint(enuJointType.JointE, -50)

        Catch excError As Exception

            ' Display error message
            WriteLog(excError.ToString)

        End Try

    End Sub


    ' -------------------------------------------------------------------------
    ' Name: btnOut_Click
    ' Abstract: Move the arm out
    ' -------------------------------------------------------------------------
    Private Sub btnOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOut.Click

        Try

            MoveJoint(enuJointType.JointE, 50)

        Catch excError As Exception

            ' Display error message
            WriteLog(excError.ToString)

        End Try


    End Sub

#End Region

#Region "Clockwise / Counter Clockwise"

    ' -------------------------------------------------------------------------
    ' Name: btnClockwise_Click
    ' Abstract: Twist the base clockwise
    ' -------------------------------------------------------------------------
    Private Sub btnClockwise_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClockwise.Click

        Try

            MoveJoint(enuJointType.JointF, -50)

        Catch excError As Exception

            ' Display error message
            WriteLog(excError.ToString)

        End Try

    End Sub


    ' -------------------------------------------------------------------------
    ' Name: btnCounterClockwise_Click
    ' Abstract: Twist the base counter clockwise
    ' -------------------------------------------------------------------------
    Private Sub btnCounterClockwise_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCounterClockwise.Click

        Try

            MoveJoint(enuJointType.JointF, 50)

        Catch excError As Exception

            ' Display error message
            WriteLog(excError.ToString)

        End Try

    End Sub

#End Region


    Private Sub btnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click

        'Dim clsPath As CPath

        'clsPath = New CPath

        InitializePaths()

        'clsPath.AddSuperMoveToPath(0, 0, 0, -606, 0, 0)
        'clsPath.AddSuperMoveToPath(0, 0, 0, -532, 603, 5)
        'clsPath.AddSuperMoveToPath(0, 100, 100, 0, 0, 0)

        SetBusyCursor(Me, True)

        MoveToAboveBoard()

        'clsPath.MoveForward()

        MessageBox.Show("It worked! Now To X 1!")

        MoveToXOne()

        MessageBox.Show("Now To Square One!")

        MoveToSquareOne()

        MessageBox.Show("It Worked as well!, Back home!")

        MoveToHome()

        'clsPath.MoveBackward()

        MessageBox.Show("It un-worked!")

        SetBusyCursor(Me, False)

    End Sub
End Class
