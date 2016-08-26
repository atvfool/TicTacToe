' -------------------------------------------------------------------------
' Module: FPendant
' Author: Andrew Hayden
' Abstract: Pendant Remake
'
' Revision        Owner   Changes:
' 1  2013/05/09   A.H.    Created.
' -------------------------------------------------------------------------

' -------------------------------------------------------------------------
' Options
' -------------------------------------------------------------------------
Option Explicit On

' -------------------------------------------------------------------------
' Imports
' -------------------------------------------------------------------------
Imports System.IO.Ports

Public Class FPendant

    ' -------------------------------------------------------------------------
    ' Name: <name>
    ' Abstract: <description>
    ' -------------------------------------------------------------------------

#Region "Form Events"



    ' -------------------------------------------------------------------------
    ' Name: FPendant_Load
    ' Abstract: opens the connections and initializes the paths
    ' -------------------------------------------------------------------------
    Private Sub FPendant_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            SetBusyCursor(Me, True)

            OpenRobotConnection()

            InitializePaths()

        Catch excError As Exception

            WriteLog(excError.ToString)

        Finally

            SetBusyCursor(Me, False)

        End Try

    End Sub


    ' -------------------------------------------------------------------------
    ' Name: FPendant_Closing
    ' Abstract: Moves the robot home and closes the connection
    ' -------------------------------------------------------------------------
    Private Sub FPendant_Closing(sender As Object, e As EventArgs) Handles MyBase.FormClosing

        Try

            SetBusyCursor(Me, True)

            MoveTo(enuLocationType.Home)

            CloseRobotConnection()

        Catch excError As Exception

            WriteLog(excError.ToString)

        Finally

            SetBusyCursor(Me, False)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: btnGoToSoftHome_Click
    ' Abstract: sets all motors to 0
    ' -------------------------------------------------------------------------
    Private Sub btnGoToSoftHome_Click(sender As System.Object, e As System.EventArgs) Handles btnGoToSoftHome.Click

        Try

            SetBusyCursor(Me, True)

            MoveTo(enuLocationType.Home)

        Catch excError As Exception

            WriteLog(excError.ToString)

        Finally

            SetBusyCursor(Me, False)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: btnAboveBoard_Click
    ' Abstract: Goes Above the main board
    ' -------------------------------------------------------------------------
    Private Sub btnAboveBoard_Click(sender As Object, e As EventArgs) Handles btnAboveBoard.Click

        Try

            SetBusyCursor(Me, True)

            MoveTo(enuLocationType.AboveTray)

        Catch excError As Exception

            WriteLog(excError.ToString)

        Finally

            SetBusyCursor(Me, False)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: btnTray_Click
    ' Abstract: Goes Above the Tray board
    ' -------------------------------------------------------------------------
    Private Sub btnTray_Click(sender As Object, e As EventArgs) Handles btnTray.Click

        Try

            SetBusyCursor(Me, True)

            MoveTo(enuLocationType.AboveTray)

        Catch excError As Exception

            WriteLog(excError.ToString)

        Finally

            SetBusyCursor(Me, False)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: btnShowOffsets_Click
    ' Abstract: Close the connection  and go home
    ' -------------------------------------------------------------------------
    Private Sub btnShowOffsets_Click(sender As Object, e As EventArgs) Handles btnShowOffsets.Click

        Try


        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: btnPlace1_Click
    ' Abstract: Goes to tic tac toe spot 1
    ' -------------------------------------------------------------------------
    Private Sub btnPlace1_Click(sender As Object, e As EventArgs) Handles btnPlace1.Click

        Try

            SetBusyCursor(Me, True)

            MoveTo(enuLocationType.Square1)

        Catch excError As Exception

            WriteLog(excError.ToString)

        Finally

            SetBusyCursor(Me, False)

        End Try

    End Sub




    ' -------------------------------------------------------------------------
    ' Name: btnPlace2_Click
    ' Abstract: Goes to tic tac toe spot 2
    ' -------------------------------------------------------------------------
    Private Sub btnPlace2_Click(sender As Object, e As EventArgs) Handles btnPlace2.Click

        Try

            SetBusyCursor(Me, True)

            MoveTo(enuLocationType.Square2)

        Catch excError As Exception

            WriteLog(excError.ToString)

        Finally

            SetBusyCursor(Me, False)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: btnPlace3_Click
    ' Abstract: Goes to tic tac toe spot 3
    ' -------------------------------------------------------------------------
    Private Sub btnPlace3_Click(sender As Object, e As EventArgs) Handles btnPlace3.Click

        Try

            SetBusyCursor(Me, True)

            MoveTo(enuLocationType.Square3)


        Catch excError As Exception

            WriteLog(excError.ToString)

        Finally

            SetBusyCursor(Me, False)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: btnPlace4_Click
    ' Abstract: Goes to tic tac toe spot 4
    ' -------------------------------------------------------------------------
    Private Sub btnPlace4_Click(sender As Object, e As EventArgs) Handles btnPlace4.Click

        Try

            SetBusyCursor(Me, True)

            MoveTo(enuLocationType.Square4)

        Catch excError As Exception

            WriteLog(excError.ToString)

        Finally

            SetBusyCursor(Me, False)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: btnPlace5_Click
    ' Abstract: Goes to tic tac toe spot 5
    ' -------------------------------------------------------------------------
    Private Sub btnPlace5_Click(sender As Object, e As EventArgs) Handles btnPlace5.Click

        Try

            SetBusyCursor(Me, True)

            MoveTo(enuLocationType.Square5)

        Catch excError As Exception

            WriteLog(excError.ToString)

        Finally

            SetBusyCursor(Me, False)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: btnPlace6_Click
    ' Abstract: Goes to tic tac toe spot 6
    ' -------------------------------------------------------------------------
    Private Sub btnPlace6_Click(sender As Object, e As EventArgs) Handles btnPlace6.Click

        Try

            SetBusyCursor(Me, True)

            MoveTo(enuLocationType.Square6)

        Catch excError As Exception

            WriteLog(excError.ToString)

        Finally

            SetBusyCursor(Me, False)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: btnPlace7_Click
    ' Abstract: Goes to tic tac toe spot 7
    ' -------------------------------------------------------------------------
    Private Sub btnPlace7_Click(sender As Object, e As EventArgs) Handles btnPlace7.Click

        Try

            SetBusyCursor(Me, True)

            MoveTo(enuLocationType.Square7)

        Catch excError As Exception

            WriteLog(excError.ToString)

        Finally

            SetBusyCursor(Me, False)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: btnPlace8_Click
    ' Abstract: Goes to tic tac toe spot 8
    ' -------------------------------------------------------------------------
    Private Sub btnPlace8_Click(sender As Object, e As EventArgs) Handles btnPlace8.Click

        Try

            SetBusyCursor(Me, True)

            MoveTo(enuLocationType.Square8)

        Catch excError As Exception

            WriteLog(excError.ToString)

        Finally

            SetBusyCursor(Me, False)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: btnPlace9_Click
    ' Abstract: Goes to tic tac toe spot 9
    ' -------------------------------------------------------------------------
    Private Sub btnPlace9_Click(sender As Object, e As EventArgs) Handles btnPlace9.Click

        Try

            SetBusyCursor(Me, True)

            MoveTo(enuLocationType.Square9)

        Catch excError As Exception

            WriteLog(excError.ToString)

        Finally

            SetBusyCursor(Me, False)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: btnTray1_Click
    ' Abstract: Goes to tic tac toe tray spot 1 (X1)
    ' -------------------------------------------------------------------------
    Private Sub btnTray1_Click(sender As Object, e As EventArgs) Handles btnX1.Click

        Try

            SetBusyCursor(Me, True)

            MoveTo(enuLocationType.X1)

        Catch excError As Exception

            WriteLog(excError.ToString)

        Finally

            SetBusyCursor(Me, False)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: btnTray2_Click
    ' Abstract: Goes to tic tac toe tray spot 2
    ' -------------------------------------------------------------------------
    Private Sub btnTray2_Click(sender As Object, e As EventArgs) Handles btnO1.Click

        Try

            SetBusyCursor(Me, True)

            MoveTo(enuLocationType.O1)

        Catch excError As Exception

            WriteLog(excError.ToString)

        Finally

            SetBusyCursor(Me, False)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: btnTray3_Click
    ' Abstract: Goes to tic tac toe tray spot 3
    ' -------------------------------------------------------------------------
    Private Sub btnTray3_Click(sender As Object, e As EventArgs) Handles btnX2.Click

        Try

            SetBusyCursor(Me, True)

            MoveTo(enuLocationType.X2)

        Catch excError As Exception

            WriteLog(excError.ToString)

        Finally

            SetBusyCursor(Me, False)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: btnTray4_Click
    ' Abstract: Goes to tic tac toe tray spot 4
    ' -------------------------------------------------------------------------
    Private Sub btnTray4_Click(sender As Object, e As EventArgs) Handles btnO2.Click

        Try

            SetBusyCursor(Me, True)

            MoveTo(enuLocationType.O2)

        Catch excError As Exception

            WriteLog(excError.ToString)

        Finally

            SetBusyCursor(Me, False)

        End Try

    End Sub




    ' -------------------------------------------------------------------------
    ' Name: btnX3_Click
    ' Abstract: Goes to tic tac toe tray spot 7
    ' -------------------------------------------------------------------------
    Private Sub btnX3_Click(sender As Object, e As EventArgs) Handles btnX3.Click

        Try

            SetBusyCursor(Me, True)

            MoveTo(enuLocationType.X3)

        Catch excError As Exception

            WriteLog(excError.ToString)

        Finally

            SetBusyCursor(Me, False)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: btnO3_Click
    ' Abstract: Goes to tic tac toe tray spot 7
    ' -------------------------------------------------------------------------
    Private Sub btnO3_Click(sender As Object, e As EventArgs) Handles btnO3.Click

        Try

            SetBusyCursor(Me, True)

            MoveTo(enuLocationType.O3)

        Catch excError As Exception

            WriteLog(excError.ToString)

        Finally

            SetBusyCursor(Me, False)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: btnTray7_Click
    ' Abstract: Goes to tic tac toe tray spot 7
    ' -------------------------------------------------------------------------
    Private Sub btnTray7_Click(sender As Object, e As EventArgs) Handles btnX4.Click

        Try

            SetBusyCursor(Me, True)

            MoveTo(enuLocationType.X4)

        Catch excError As Exception

            WriteLog(excError.ToString)

        Finally

            SetBusyCursor(Me, False)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: btnO4_Click
    ' Abstract: Goes to tic tac toe tray spot 8
    ' -------------------------------------------------------------------------
    Private Sub btnO4_Click(sender As Object, e As EventArgs) Handles btnO4.Click

        Try

            SetBusyCursor(Me, True)

            MoveTo(enuLocationType.O4)

        Catch excError As Exception

            WriteLog(excError.ToString)

        Finally

            SetBusyCursor(Me, False)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: btnTray9_Click
    ' Abstract: Goes to tic tac toe tray spot 9
    ' -------------------------------------------------------------------------
    Private Sub btnTray9_Click(sender As Object, e As EventArgs) Handles btnX5.Click

        Try

            SetBusyCursor(Me, True)

            MoveTo(enuLocationType.X5)

        Catch excError As Exception

            WriteLog(excError.ToString)

        Finally

            SetBusyCursor(Me, False)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: btnTray10_Click
    ' Abstract: Goes to tic tac toe tray spot 10
    ' -------------------------------------------------------------------------
    Private Sub btnTray10_Click(sender As Object, e As EventArgs) Handles btnO5.Click

        Try


        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub


#End Region


End Class