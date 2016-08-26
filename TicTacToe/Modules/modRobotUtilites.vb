' -------------------------------------------------------------------------
' Module: FPendant
' Author: Andrew Hayden
' Abstract: Pendant Remake
'
' Revision        Owner   Changes:
' 1  2013/05/09   A.H.    Created.
' Make class with following properties
' CMove - move any one joint distance
' Properties
' m_enuJoint A-F
' m_intDistance
' m_intDirection -1, +1
' m_intStepCount Distance/Maximum_Step_Distance
' m_intLastStepDistance %
' m_intCurrentStep
' -------------------------------------------------------------------------
' Methods
' New(enujoint, intDistance)
' LongSmoothMoveForward Serial
' LongSmoothMoveBackward Serial
' -------------------------------------------------------------------------
' -------------------------------------------------------------------------
' CSuperMove - All 6 joints and individual distance in parralled
' Properties
' m_aclsMoves(6)
' -------------------------------------------------------------------------
' Methods
' New(intDistanceA, ......, intDistanceF
' m_aclsMoves(1) = New CMove(enuJointTypeA, intDistanceA)
' .
' .
' .
' m_aclsMoves(1) = New CMove(enuJointTypeA, intDistanceA)
' MoveForward()
' MoveBackward()
' -------------------------------------------------------------------------
' -------------------------------------------------------------------------
' CPath - An Array of SuperMoves
' Properties
' 



'''''''''''''''''''''''''''''''''''''''''''''
' TODO: Make X and O go to Above Tray thenabove board
'''''''''''''''''''''''''''''''''''''''''''''
' -------------------------------------------------------------------------

' -------------------------------------------------------------------------
' Options
' -------------------------------------------------------------------------
Option Explicit On

' -------------------------------------------------------------------------
' Imports
' -------------------------------------------------------------------------
Imports System.IO.Ports

Public Module modRobotUtilites



    ' -------------------------------------------------------------------------
    ' Form variables
    ' -------------------------------------------------------------------------
    Private m_serRobotController As New SerialPort

    Private m_clsHomeToAboveBoard As CPath
    Private m_clsAboveBoardToAboveTray As CPath
    Private m_clsAboveBoardToSquare1 As CPath
    Private m_clsAboveBoardToSquare2 As CPath
    Private m_clsAboveBoardToSquare3 As CPath
    Private m_clsAboveBoardToSquare4 As CPath
    Private m_clsAboveBoardToSquare5 As CPath
    Private m_clsAboveBoardToSquare6 As CPath
    Private m_clsAboveBoardToSquare7 As CPath
    Private m_clsAboveBoardToSquare8 As CPath
    Private m_clsAboveBoardToSquare9 As CPath
    Private m_clsAboveTrayToX1 As CPath
    Private m_clsAboveTrayToX2 As CPath
    Private m_clsAboveTrayToX3 As CPath
    Private m_clsAboveTrayToX4 As CPath
    Private m_clsAboveTrayToX5 As CPath
	Private m_clsAboveTrayToO1 As CPath
	Private m_clsAboveTrayToO2 As CPath
	Private m_clsAboveTrayToO3 As CPath
    Private m_clsAboveTrayToO4 As CPath
    Private m_clsAboveTrayToO5 As CPath

    Private m_enuCurrentLocation As enuLocationType
    Private m_blnGripperIsOpen As Boolean = True


#Region "Open / Close Connection"



    ' -------------------------------------------------------------------------
    ' Name: OpenRobotConnection
    ' Abstract: Opens the connection to the robot
    ' -------------------------------------------------------------------------
    Public Sub OpenRobotConnection()

        Try

            ' COM3: 9600,E,7,2
            m_serRobotController.PortName = "COM4"
            m_serRobotController.BaudRate = 9600
            m_serRobotController.Parity = Parity.Even
            m_serRobotController.DataBits = 7
            m_serRobotController.StopBits = StopBits.Two

            m_serRobotController.Open()

            InitializePaths()

        Catch excError As Exception

            ' Display error message
            WriteLog(excError.ToString)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: CloseRobotConnection
    ' Abstract: Closes the connection to the robot
    ' -------------------------------------------------------------------------
    Public Sub CloseRobotConnection()

        Try

            m_serRobotController.Close()

        Catch excError As Exception

            ' Display error message
            WriteLog(excError.ToString)

        End Try

    End Sub

#End Region



    ' -------------------------------------------------------------------------
    ' Name: InitializePaths
    ' Abstract: Sets up the paths for the robot
    ' -------------------------------------------------------------------------
    Public Sub InitializePaths()

        Try

            m_clsHomeToAboveBoard = New CPath()
            m_clsHomeToAboveBoard.AddSuperMoveToPath(0, 0, 0, -150, 230, 0)

            m_clsAboveBoardToAboveTray = New CPath()
            m_clsAboveBoardToAboveTray.AddSuperMoveToPath(0, 0, 0, 0, 0, -274)

            ' ---------------------------
            '           Board
            ' ---------------------------

            m_clsAboveBoardToSquare1 = New CPath()
            m_clsAboveBoardToSquare1.AddSuperMoveToPath(0, -110, 0, 0, 0, -140)
            'm_clsAboveBoardToSquare1.AddSuperMoveToPath(0, 0, 0, -480, 300, 0)
            m_clsAboveBoardToSquare1.AddSuperMoveToPath(0, 0, 0, -48, 30, 0)
            m_clsAboveBoardToSquare1.AddSuperMoveToPath(0, 0, 0, -48, 30, 0)
            m_clsAboveBoardToSquare1.AddSuperMoveToPath(0, 0, 0, -48, 30, 0)
            m_clsAboveBoardToSquare1.AddSuperMoveToPath(0, 0, 0, -48, 30, 0)
            m_clsAboveBoardToSquare1.AddSuperMoveToPath(0, 0, 0, -48, 30, 0)
            m_clsAboveBoardToSquare1.AddSuperMoveToPath(0, 0, 0, -48, 30, 0)
            m_clsAboveBoardToSquare1.AddSuperMoveToPath(0, 0, 0, -48, 30, 0)
            m_clsAboveBoardToSquare1.AddSuperMoveToPath(0, 0, 0, -48, 30, 0)
            m_clsAboveBoardToSquare1.AddSuperMoveToPath(0, 0, 0, -40, 30, 0)
            m_clsAboveBoardToSquare1.AddSuperMoveToPath(0, 0, 0, -40, 30, 0)

            m_clsAboveBoardToSquare2 = New CPath()
            'm_clsAboveBoardToSquare2.AddSuperMoveToPath(0, 0, 0, -500, 270, 0)
            m_clsAboveBoardToSquare2.AddSuperMoveToPath(0, 0, 0, -70, 20, 0)
            m_clsAboveBoardToSquare2.AddSuperMoveToPath(0, 0, 0, -70, 30, 0)
            m_clsAboveBoardToSquare2.AddSuperMoveToPath(0, 0, 0, -70, 30, 0)
            m_clsAboveBoardToSquare2.AddSuperMoveToPath(0, 0, 0, -60, 40, 0)
            m_clsAboveBoardToSquare2.AddSuperMoveToPath(0, 0, 0, -60, 40, 0)
            m_clsAboveBoardToSquare2.AddSuperMoveToPath(0, 0, 0, -60, 30, 0)
            m_clsAboveBoardToSquare2.AddSuperMoveToPath(0, 0, 0, -40, 30, 0)
            m_clsAboveBoardToSquare2.AddSuperMoveToPath(0, 0, 0, -40, 30, 0)

            m_clsAboveBoardToSquare3 = New CPath()
            m_clsAboveBoardToSquare3.AddSuperMoveToPath(0, 90, 0, 0, 0, 140)
            'm_clsAboveBoardToSquare3.AddSuperMoveToPath(0, 0, 0, -475, 300, 0)
            m_clsAboveBoardToSquare3.AddSuperMoveToPath(0, 0, 0, -48, 30, 0)
            m_clsAboveBoardToSquare3.AddSuperMoveToPath(0, 0, 0, -48, 30, 0)
            m_clsAboveBoardToSquare3.AddSuperMoveToPath(0, 0, 0, -48, 30, 0)
            m_clsAboveBoardToSquare3.AddSuperMoveToPath(0, 0, 0, -48, 30, 0)
            m_clsAboveBoardToSquare3.AddSuperMoveToPath(0, 0, 0, -48, 30, 0)
            m_clsAboveBoardToSquare3.AddSuperMoveToPath(0, 0, 0, -48, 30, 0)
            m_clsAboveBoardToSquare3.AddSuperMoveToPath(0, 0, 0, -48, 30, 0)
            m_clsAboveBoardToSquare3.AddSuperMoveToPath(0, 0, 0, -48, 30, 0)
            m_clsAboveBoardToSquare3.AddSuperMoveToPath(0, 0, 0, -48, 30, 0)
            m_clsAboveBoardToSquare3.AddSuperMoveToPath(0, 0, 0, -48, 30, 0)

            m_clsAboveBoardToSquare4 = New CPath()
            m_clsAboveBoardToSquare4.AddSuperMoveToPath(0, -75, 0, 0, 0, -90)
            'm_clsAboveBoardToSquare4.AddSuperMoveToPath(0, 0, 0, -350, 400, 0)
            m_clsAboveBoardToSquare4.AddSuperMoveToPath(0, 0, 0, -35, 40, 0)
            m_clsAboveBoardToSquare4.AddSuperMoveToPath(0, 0, 0, -35, 40, 0)
            m_clsAboveBoardToSquare4.AddSuperMoveToPath(0, 0, 0, -35, 40, 0)
            m_clsAboveBoardToSquare4.AddSuperMoveToPath(0, 0, 0, -35, 40, 0)
            m_clsAboveBoardToSquare4.AddSuperMoveToPath(0, 0, 0, -35, 40, 0)
            m_clsAboveBoardToSquare4.AddSuperMoveToPath(0, 0, 0, -35, 40, 0)
            m_clsAboveBoardToSquare4.AddSuperMoveToPath(0, 0, 0, -35, 40, 0)
            m_clsAboveBoardToSquare4.AddSuperMoveToPath(0, 0, 0, -35, 40, 0)
            m_clsAboveBoardToSquare4.AddSuperMoveToPath(0, 0, 0, -35, 40, 0)
            m_clsAboveBoardToSquare4.AddSuperMoveToPath(0, 0, 0, -35, 40, 0)

            m_clsAboveBoardToSquare5 = New CPath()
            'm_clsAboveBoardToSquare5.AddSuperMoveToPath(0, 0, 0, -380, 370, 0)
            m_clsAboveBoardToSquare5.AddSuperMoveToPath(0, 0, 0, -38, 37, 0)
            m_clsAboveBoardToSquare5.AddSuperMoveToPath(0, 0, 0, -38, 37, 0)
            m_clsAboveBoardToSquare5.AddSuperMoveToPath(0, 0, 0, -38, 37, 0)
            m_clsAboveBoardToSquare5.AddSuperMoveToPath(0, 0, 0, -38, 37, 0)
            m_clsAboveBoardToSquare5.AddSuperMoveToPath(0, 0, 0, -38, 37, 0)
            m_clsAboveBoardToSquare5.AddSuperMoveToPath(0, 0, 0, -38, 37, 0)
            m_clsAboveBoardToSquare5.AddSuperMoveToPath(0, 0, 0, -38, 37, 0)
            m_clsAboveBoardToSquare5.AddSuperMoveToPath(0, 0, 0, -38, 37, 0)
            m_clsAboveBoardToSquare5.AddSuperMoveToPath(0, 0, 0, -38, 37, 0)
            m_clsAboveBoardToSquare5.AddSuperMoveToPath(0, 0, 0, -38, 37, 0)

            m_clsAboveBoardToSquare6 = New CPath()
            m_clsAboveBoardToSquare6.AddSuperMoveToPath(0, 80, 0, 0, 0, 90)
            'm_clsAboveBoardToSquare6.AddSuperMoveToPath(0, 0, 0, -350, 400, 0)
            m_clsAboveBoardToSquare6.AddSuperMoveToPath(0, 0, 0, -35, 40, 0)
            m_clsAboveBoardToSquare6.AddSuperMoveToPath(0, 0, 0, -35, 40, 0)
            m_clsAboveBoardToSquare6.AddSuperMoveToPath(0, 0, 0, -35, 40, 0)
            m_clsAboveBoardToSquare6.AddSuperMoveToPath(0, 0, 0, -35, 40, 0)
            m_clsAboveBoardToSquare6.AddSuperMoveToPath(0, 0, 0, -35, 40, 0)
            m_clsAboveBoardToSquare6.AddSuperMoveToPath(0, 0, 0, -35, 40, 0)
            m_clsAboveBoardToSquare6.AddSuperMoveToPath(0, 0, 0, -35, 40, 0)
            m_clsAboveBoardToSquare6.AddSuperMoveToPath(0, 0, 0, -35, 40, 0)
            m_clsAboveBoardToSquare6.AddSuperMoveToPath(0, 0, 0, -35, 40, 0)
            m_clsAboveBoardToSquare6.AddSuperMoveToPath(0, 0, 0, -35, 40, 0)

            m_clsAboveBoardToSquare7 = New CPath()
            m_clsAboveBoardToSquare7.AddSuperMoveToPath(0, -60, 0, 0, 0, -75)
            'm_clsAboveBoardToSquare7.AddSuperMoveToPath(0, 0, 0, -210, 520, 0)
            m_clsAboveBoardToSquare7.AddSuperMoveToPath(0, 0, 0, -42, 104, 0)
            m_clsAboveBoardToSquare7.AddSuperMoveToPath(0, 0, 0, -42, 104, 0)
            m_clsAboveBoardToSquare7.AddSuperMoveToPath(0, 0, 0, -21, 104, 0)
            m_clsAboveBoardToSquare7.AddSuperMoveToPath(0, 0, 0, -21, 52, 0)
            m_clsAboveBoardToSquare7.AddSuperMoveToPath(0, 0, 0, -21, 52, 0)
            m_clsAboveBoardToSquare7.AddSuperMoveToPath(0, 0, 0, -21, 52, 0)
            m_clsAboveBoardToSquare7.AddSuperMoveToPath(0, 0, 0, -21, 52, 0)
            m_clsAboveBoardToSquare7.AddSuperMoveToPath(0, 0, 0, -21, 0, 0)

            m_clsAboveBoardToSquare8 = New CPath()
            'm_clsAboveBoardToSquare8.AddSuperMoveToPath(0, 0, 0, -220, 500, 0)
            m_clsAboveBoardToSquare8.AddSuperMoveToPath(0, 0, 0, -44, 200, 0)
            m_clsAboveBoardToSquare8.AddSuperMoveToPath(0, 0, 0, -44, 150, 0)
            m_clsAboveBoardToSquare8.AddSuperMoveToPath(0, 0, 0, -44, 100, 0)
            m_clsAboveBoardToSquare8.AddSuperMoveToPath(0, 0, 0, -44, 50, 0)
            m_clsAboveBoardToSquare8.AddSuperMoveToPath(0, 0, 0, -44, 0, 0)

            m_clsAboveBoardToSquare9 = New CPath()
            m_clsAboveBoardToSquare9.AddSuperMoveToPath(0, 60, 0, 0, 0, 65)
            'm_clsAboveBoardToSquare9.AddSuperMoveToPath(0, 0, 0, -210, 530, 0)
            m_clsAboveBoardToSquare9.AddSuperMoveToPath(0, 0, 0, -42, 205, 0)
            m_clsAboveBoardToSquare9.AddSuperMoveToPath(0, 0, 0, -42, 159, 0)
            m_clsAboveBoardToSquare9.AddSuperMoveToPath(0, 0, 0, -42, 106, 0)
            m_clsAboveBoardToSquare9.AddSuperMoveToPath(0, 0, 0, -42, 56, 0)
            m_clsAboveBoardToSquare9.AddSuperMoveToPath(0, 0, 0, -42, 0, 0)


            ' ---------------------------
            '         End Board
            ' ---------------------------

            ' ---------------------------
            '            Tray
            ' ---------------------------

            m_clsAboveTrayToX1 = New CPath()
            ' m_clsAboveTrayToX1.AddSuperMoveToPath(0, -360, 0, -315, 435, -255)
            m_clsAboveTrayToX1.AddSuperMoveToPath(0, -360, 0, 0, 0, -255)
            m_clsAboveTrayToX1.AddSuperMoveToPath(0, -0, 0, -36, 48, 0)
            m_clsAboveTrayToX1.AddSuperMoveToPath(0, -0, 0, -31, 43, 0)
            m_clsAboveTrayToX1.AddSuperMoveToPath(0, -0, 0, -31, 43, 0)
            m_clsAboveTrayToX1.AddSuperMoveToPath(0, -0, 0, -31, 43, 0)
            m_clsAboveTrayToX1.AddSuperMoveToPath(0, -0, 0, -31, 43, 0)
            m_clsAboveTrayToX1.AddSuperMoveToPath(0, -0, 0, -31, 43, 0)
            m_clsAboveTrayToX1.AddSuperMoveToPath(0, -0, 0, -31, 43, 0)
            m_clsAboveTrayToX1.AddSuperMoveToPath(0, -0, 0, -31, 43, 0)
            m_clsAboveTrayToX1.AddSuperMoveToPath(0, -0, 0, -31, 43, 0)
            m_clsAboveTrayToX1.AddSuperMoveToPath(0, -0, 0, -31, 43, 0)

            m_clsAboveTrayToX2 = New CPath()
            'm_clsAboveTrayToX2.AddSuperMoveToPath(0, -345, 0, -345, 475, -195)
            m_clsAboveTrayToX2.AddSuperMoveToPath(0, -345, 0, 0, 0, -195)
            m_clsAboveTrayToX2.AddSuperMoveToPath(0, 0, 0, -45, 43, 0)
            m_clsAboveTrayToX2.AddSuperMoveToPath(0, 0, 0, -45, 43, 0)
            m_clsAboveTrayToX2.AddSuperMoveToPath(0, 0, 0, -45, 43, 0)
            m_clsAboveTrayToX2.AddSuperMoveToPath(0, 0, 0, -45, 43, 0)
            m_clsAboveTrayToX2.AddSuperMoveToPath(0, 0, 0, -45, 43, 0)
            m_clsAboveTrayToX2.AddSuperMoveToPath(0, 0, 0, -45, 43, 0)
            m_clsAboveTrayToX2.AddSuperMoveToPath(0, 0, 0, -30, 43, 0)
            m_clsAboveTrayToX2.AddSuperMoveToPath(0, 0, 0, -30, 43, 0)
            m_clsAboveTrayToX2.AddSuperMoveToPath(0, 0, 0, -30, 43, 0)

            m_clsAboveTrayToX3 = New CPath()
            'm_clsAboveTrayToX3.AddSuperMoveToPath(0, -315, 0, -375, 375, -125)
            m_clsAboveTrayToX3.AddSuperMoveToPath(0, -315, 0, 0, 0, -125)
            m_clsAboveTrayToX3.AddSuperMoveToPath(0, 0, 0, -40, 40, 0)
            m_clsAboveTrayToX3.AddSuperMoveToPath(0, 0, 0, -30, 30, 0)
            m_clsAboveTrayToX3.AddSuperMoveToPath(0, 0, 0, -30, 30, 0)
            m_clsAboveTrayToX3.AddSuperMoveToPath(0, 0, 0, -30, 30, 0)
            m_clsAboveTrayToX3.AddSuperMoveToPath(0, 0, 0, -30, 30, 0)
            m_clsAboveTrayToX3.AddSuperMoveToPath(0, 0, 0, -30, 30, 0)
            m_clsAboveTrayToX3.AddSuperMoveToPath(0, 0, 0, -30, 30, 0)
            m_clsAboveTrayToX3.AddSuperMoveToPath(0, 0, 0, -30, 30, 0)
            m_clsAboveTrayToX3.AddSuperMoveToPath(0, 0, 0, -30, 30, 0)
            m_clsAboveTrayToX3.AddSuperMoveToPath(0, 0, 0, -30, 30, 0)
            m_clsAboveTrayToX3.AddSuperMoveToPath(0, 0, 0, -30, 30, 0)
            m_clsAboveTrayToX3.AddSuperMoveToPath(0, 0, 0, -30, 30, 0)

            m_clsAboveTrayToX4 = New CPath()
            'm_clsAboveTrayToX4.AddSuperMoveToPath(0, -250, 0, -380, 380, -50)
            m_clsAboveTrayToX4.AddSuperMoveToPath(0, -250, 0, 0, 0, -50)
            m_clsAboveTrayToX4.AddSuperMoveToPath(0, 0, 0, -38, 38, 0)
            m_clsAboveTrayToX4.AddSuperMoveToPath(0, 0, 0, -38, 38, 0)
            m_clsAboveTrayToX4.AddSuperMoveToPath(0, 0, 0, -38, 38, 0)
            m_clsAboveTrayToX4.AddSuperMoveToPath(0, 0, 0, -38, 38, 0)
            m_clsAboveTrayToX4.AddSuperMoveToPath(0, 0, 0, -38, 38, 0)
            m_clsAboveTrayToX4.AddSuperMoveToPath(0, 0, 0, -38, 38, 0)
            m_clsAboveTrayToX4.AddSuperMoveToPath(0, 0, 0, -38, 38, 0)
            m_clsAboveTrayToX4.AddSuperMoveToPath(0, 0, 0, -38, 38, 0)
            m_clsAboveTrayToX4.AddSuperMoveToPath(0, 0, 0, -38, 38, 0)
            m_clsAboveTrayToX4.AddSuperMoveToPath(0, 0, 0, -38, 38, 0)

			m_clsAboveTrayToX5 = New CPath()
            'm_clsAboveTrayToX5.AddSuperMoveToPath(0, -200, 0, -330, 400, 0)
            m_clsAboveTrayToX5.AddSuperMoveToPath(0, -100, 0, 0, 0, 0)
            m_clsAboveTrayToX5.AddSuperMoveToPath(0, -100, 0, 0, 0, 0)
            m_clsAboveTrayToX5.AddSuperMoveToPath(0, 0, 0, -33, 40, 0)
            m_clsAboveTrayToX5.AddSuperMoveToPath(0, 0, 0, -33, 40, 0)
            m_clsAboveTrayToX5.AddSuperMoveToPath(0, 0, 0, -33, 40, 0)
            m_clsAboveTrayToX5.AddSuperMoveToPath(0, 0, 0, -33, 40, 0)
            m_clsAboveTrayToX5.AddSuperMoveToPath(0, 0, 0, -33, 40, 0)
            m_clsAboveTrayToX5.AddSuperMoveToPath(0, 0, 0, -33, 40, 0)
            m_clsAboveTrayToX5.AddSuperMoveToPath(0, 0, 0, -33, 40, 0)
            m_clsAboveTrayToX5.AddSuperMoveToPath(0, 0, 0, -33, 40, 0)
            m_clsAboveTrayToX5.AddSuperMoveToPath(0, 0, 0, -33, 40, 0)
            m_clsAboveTrayToX5.AddSuperMoveToPath(0, 0, 0, -33, 40, 0)

			m_clsAboveTrayToO1 = New CPath()
            'm_clsAboveTrayToO1.AddSuperMoveToPath(0, -400, 0, -400, 400, -300)
            m_clsAboveTrayToO1.AddSuperMoveToPath(0, -400, 0, 0, 0, -300)
            m_clsAboveTrayToO1.AddSuperMoveToPath(0, 0, 0, -40, 40, 0)
            m_clsAboveTrayToO1.AddSuperMoveToPath(0, 0, 0, -40, 40, 0)
            m_clsAboveTrayToO1.AddSuperMoveToPath(0, 0, 0, -40, 40, 0)
            m_clsAboveTrayToO1.AddSuperMoveToPath(0, 0, 0, -40, 40, 0)
            m_clsAboveTrayToO1.AddSuperMoveToPath(0, 0, 0, -40, 40, 0)
            m_clsAboveTrayToO1.AddSuperMoveToPath(0, 0, 0, -40, 40, 0)
            m_clsAboveTrayToO1.AddSuperMoveToPath(0, 0, 0, -40, 40, 0)
            m_clsAboveTrayToO1.AddSuperMoveToPath(0, 0, 0, -40, 40, 0)
            m_clsAboveTrayToO1.AddSuperMoveToPath(0, 0, 0, -40, 40, 0)
            m_clsAboveTrayToO1.AddSuperMoveToPath(0, 0, 0, -50, 0, 0)

            m_clsAboveTrayToO2 = New CPath()
            'm_clsAboveTrayToO2.AddSuperMoveToPath(0, -365, 0, -440, 300, -225)
            m_clsAboveTrayToO2.AddSuperMoveToPath(0, -365, 0, 0, 0, -225)
            m_clsAboveTrayToO2.AddSuperMoveToPath(0, 0, 0, -50, 30, 0)
            m_clsAboveTrayToO2.AddSuperMoveToPath(0, 0, 0, -50, 30, 0)
            m_clsAboveTrayToO2.AddSuperMoveToPath(0, 0, 0, -50, 30, 0)
            m_clsAboveTrayToO2.AddSuperMoveToPath(0, 0, 0, -50, 30, 0)
            m_clsAboveTrayToO2.AddSuperMoveToPath(0, 0, 0, -40, 30, 0)
            m_clsAboveTrayToO2.AddSuperMoveToPath(0, 0, 0, -40, 30, 0)
            m_clsAboveTrayToO2.AddSuperMoveToPath(0, 0, 0, -40, 30, 0)
            m_clsAboveTrayToO2.AddSuperMoveToPath(0, 0, 0, -40, 30, 0)
            m_clsAboveTrayToO2.AddSuperMoveToPath(0, 0, 0, -40, 30, 0)
            m_clsAboveTrayToO2.AddSuperMoveToPath(0, 0, 0, -40, 30, 0)

            m_clsAboveTrayToO3 = New CPath()
            'm_clsAboveTrayToO3.AddSuperMoveToPath(0, -300, 0, -450, 265, -125)
            m_clsAboveTrayToO3.AddSuperMoveToPath(0, -300, 0, 0, 0, -125)
            m_clsAboveTrayToO3.AddSuperMoveToPath(0, 0, 0, -45, 35, 0)
            m_clsAboveTrayToO3.AddSuperMoveToPath(0, 0, 0, -45, 35, 0)
            m_clsAboveTrayToO3.AddSuperMoveToPath(0, 0, 0, -45, 25, 0)
            m_clsAboveTrayToO3.AddSuperMoveToPath(0, 0, 0, -45, 25, 0)
            m_clsAboveTrayToO3.AddSuperMoveToPath(0, 0, 0, -45, 25, 0)
            m_clsAboveTrayToO3.AddSuperMoveToPath(0, 0, 0, -45, 25, 0)
            m_clsAboveTrayToO3.AddSuperMoveToPath(0, 0, 0, -40, 25, 0)
            m_clsAboveTrayToO3.AddSuperMoveToPath(0, 0, 0, -40, 25, 0)
            m_clsAboveTrayToO3.AddSuperMoveToPath(0, 0, 0, -40, 25, 0)
            m_clsAboveTrayToO3.AddSuperMoveToPath(0, 0, 0, -40, 25, 0)
            m_clsAboveTrayToO3.AddSuperMoveToPath(0, 0, 0, -40, 25, 0)
            
            m_clsAboveTrayToO4 = New CPath()
            'm_clsAboveTrayToO4.AddSuperMoveToPath(0, -250, 0, -450, 300, -40)
            m_clsAboveTrayToO4.AddSuperMoveToPath(0, -250, 0, 0, 0, -40)
            m_clsAboveTrayToO4.AddSuperMoveToPath(0, 0, 0, -45, 30, 0)
            m_clsAboveTrayToO4.AddSuperMoveToPath(0, 0, 0, -45, 30, 0)
            m_clsAboveTrayToO4.AddSuperMoveToPath(0, 0, 0, -45, 30, 0)
            m_clsAboveTrayToO4.AddSuperMoveToPath(0, 0, 0, -45, 30, 0)
            m_clsAboveTrayToO4.AddSuperMoveToPath(0, 0, 0, -45, 30, 0)
            m_clsAboveTrayToO4.AddSuperMoveToPath(0, 0, 0, -45, 30, 0)
            m_clsAboveTrayToO4.AddSuperMoveToPath(0, 0, 0, -45, 30, 0)
            m_clsAboveTrayToO4.AddSuperMoveToPath(0, 0, 0, -45, 30, 0)
            m_clsAboveTrayToO4.AddSuperMoveToPath(0, 0, 0, -45, 30, 0)
            m_clsAboveTrayToO4.AddSuperMoveToPath(0, 0, 0, -45, 30, 0)
            m_clsAboveTrayToO4.AddSuperMoveToPath(0, 0, 0, -10, 10, 0)

            m_clsAboveTrayToO5 = New CPath()
            'm_clsAboveTrayToO5.AddSuperMoveToPath(0, -185, 0, -400, 350, 50)
            m_clsAboveTrayToO5.AddSuperMoveToPath(0, -185, 0, 0, 0, 50)
            m_clsAboveTrayToO5.AddSuperMoveToPath(0, 0, 0, -40, 35, 0)
            m_clsAboveTrayToO5.AddSuperMoveToPath(0, 0, 0, -40, 35, 0)
            m_clsAboveTrayToO5.AddSuperMoveToPath(0, 0, 0, -40, 35, 0)
            m_clsAboveTrayToO5.AddSuperMoveToPath(0, 0, 0, -40, 35, 0)
            m_clsAboveTrayToO5.AddSuperMoveToPath(0, 0, 0, -40, 35, 0)
            m_clsAboveTrayToO5.AddSuperMoveToPath(0, 0, 0, -40, 35, 0)
            m_clsAboveTrayToO5.AddSuperMoveToPath(0, 0, 0, -40, 35, 0)
            m_clsAboveTrayToO5.AddSuperMoveToPath(0, 0, 0, -40, 35, 0)
            m_clsAboveTrayToO5.AddSuperMoveToPath(0, 0, 0, -40, 35, 0)
            m_clsAboveTrayToO5.AddSuperMoveToPath(0, 0, 0, -40, 35, 0)


            ' ---------------------------
            '         End Tray
            ' ---------------------------

            m_enuCurrentLocation = enuLocationType.Home

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: MoveJoint
    ' Abstract: Move the joint the specified distance.
    '           Boundary and range check
    ' -------------------------------------------------------------------------
    Public Sub MoveJoint(ByVal enuJoint As enuJointType, ByVal intDistance As Integer)

        Try

            Dim strMoveCommand As String = ""

            strMoveCommand = BuildMoveCommand(enuJoint, intDistance)

            SendCommand(strMoveCommand)

            Wait(65)

        Catch excError As Exception

            ' Display error message
            WriteLog(excError.ToString)

        End Try

    End Sub


    ' -------------------------------------------------------------------------
    ' Name: BuildMoveCommand
    ' Abstract: Build the move command.
    '           Format: <Joint> <+,-> <Distance> <CRLF>
    ' -------------------------------------------------------------------------
    Private Function BuildMoveCommand(enuJoint As enuJointType, intDistance As Integer) As String

        Dim strCommand As String = ""

        Try

            Dim intMoveDistance As Integer = 0

            strCommand = enuJoint.GetJointLetter

            strCommand &= intDistance

            ' All move commands must end with CRLF
            strCommand &= vbCrLf

        Catch excError As Exception

            ' Display error message
            WriteLog(excError.ToString)

        End Try

        Return strCommand

    End Function



    ' -------------------------------------------------------------------------
    ' Name: GetRemaingMoveDistance
    ' Abstract: checks the motors remaining distance and returns it
    ' -------------------------------------------------------------------------
    Public Function GetRemaingMoveDistance(enuJoint As enuJointType) As Integer

        Dim intRemainingDistance As Integer

        Try

            Dim strCommand As String = ""

            strCommand = enuJoint.GetJointLetter & "?"

            SendCommand(strCommand)

            intRemainingDistance = Math.Abs(m_serRobotController.ReadByte)

            If intRemainingDistance >= 32 Then intRemainingDistance -= 32

            'Debug.Print(strCommand & " = " & intRemainingDistance)

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

        Return intRemainingDistance

    End Function


    ' -------------------------------------------------------------------------
    ' Name: SendCommand
    ' Abstract: The only place that writes to the robot controller
    ' -------------------------------------------------------------------------
    Private Sub SendCommand(strCommand As String)

        Try

            m_serRobotController.Write(strCommand)

            ' Always have to wait after each write command
            Wait(10)

            'Debug.Print(strCommand)

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub


    ' -------------------------------------------------------------------------
    ' Name: OpenGripper
    ' Abstract: Open sesame
    ' -------------------------------------------------------------------------
    Public Sub OpenGripper()

        Try

            If m_blnGripperIsOpen = False Then

                MoveJoint(enuJointType.JointA, -60)
                m_blnGripperIsOpen = True

            End If

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub


    ' -------------------------------------------------------------------------
    ' Name: CloseGripper
    ' Abstract: Close sesame
    ' -------------------------------------------------------------------------
    Public Sub CloseGripper()

        Try

            If m_blnGripperIsOpen = True Then

                MoveJoint(enuJointType.JointA, 60)
                m_blnGripperIsOpen = False

            End If

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: MoveTo
    ' Abstract: I like to move it, move it!
    ' -------------------------------------------------------------------------
    Public Sub MoveTo(enuDestination As enuLocationType)

        Try

            If enuDestination = enuLocationType.Home Then

                MoveToHome()

            ElseIf enuDestination = enuLocationType.AboveBoard Then

                MoveToAboveBoard()

            ElseIf enuDestination = enuLocationType.AboveTray Then

                MoveToAboveTray()

            ElseIf enuDestination = enuLocationType.Square1 Then ' Board Section

                MoveToSquareOne()

            ElseIf enuDestination = enuLocationType.Square2 Then

                MoveToSquareTwo()

            ElseIf enuDestination = enuLocationType.Square3 Then

                MoveToSquareThree()

            ElseIf enuDestination = enuLocationType.Square4 Then

                MoveToSquareFour()

            ElseIf enuDestination = enuLocationType.Square5 Then

                MoveToSquareFive()

            ElseIf enuDestination = enuLocationType.Square6 Then

                MoveToSquareSix()

            ElseIf enuDestination = enuLocationType.Square7 Then

                MoveToSquareSeven()

            ElseIf enuDestination = enuLocationType.Square8 Then

                MoveToSquareEight()

            ElseIf enuDestination = enuLocationType.Square9 Then

                MoveToSquareNine()

            ElseIf enuDestination = enuLocationType.X1 Then ' Tray Section

                MoveToXOne()

            ElseIf enuDestination = enuLocationType.X2 Then

                MoveToXTwo()

            ElseIf enuDestination = enuLocationType.X3 Then

                MoveToXThree()

            ElseIf enuDestination = enuLocationType.X4 Then

                MoveToXFour()

            ElseIf enuDestination = enuLocationType.X5 Then

                MoveToXFive()

            ElseIf enuDestination = enuLocationType.O1 Then

                MoveToOOne()


            ElseIf enuDestination = enuLocationType.O2 Then

                MoveToOTwo()

            ElseIf enuDestination = enuLocationType.O3 Then

                MoveToOThree()

            ElseIf enuDestination = enuLocationType.O4 Then

                MoveToOFour()

            ElseIf enuDestination = enuLocationType.O5 Then

                MoveToOFive()

            End If

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub


    ' -------------------------------------------------------------------------
    ' Name: MoveToHome
    ' Abstract: There's no place like 127.0.0.1
    ' -------------------------------------------------------------------------
    Public Sub MoveToHome()

        Try

            If m_enuCurrentLocation = enuLocationType.Home Then

                ' Do Nothing

            ElseIf m_enuCurrentLocation = enuLocationType.AboveBoard Then

                m_clsHomeToAboveBoard.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.AboveTray Then

                m_clsAboveBoardToAboveTray.MoveBackward()
                m_clsHomeToAboveBoard.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square1 Then

                m_clsAboveBoardToSquare1.MoveBackward()
                m_clsHomeToAboveBoard.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square2 Then

                m_clsAboveBoardToSquare2.MoveBackward()
                m_clsHomeToAboveBoard.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square3 Then

                m_clsAboveBoardToSquare3.MoveBackward()
                m_clsHomeToAboveBoard.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square4 Then

                m_clsAboveBoardToSquare4.MoveBackward()
                m_clsHomeToAboveBoard.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square5 Then

                m_clsAboveBoardToSquare5.MoveBackward()
                m_clsHomeToAboveBoard.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square6 Then

                m_clsAboveBoardToSquare6.MoveBackward()
                m_clsHomeToAboveBoard.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square7 Then

                m_clsAboveBoardToSquare7.MoveBackward()
                m_clsHomeToAboveBoard.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square8 Then

                m_clsAboveBoardToSquare8.MoveBackward()
                m_clsHomeToAboveBoard.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square9 Then

                m_clsAboveBoardToSquare9.MoveBackward()
                m_clsHomeToAboveBoard.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.O1 Then

                m_clsAboveTrayToO1.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveBackward()
                m_clsHomeToAboveBoard.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.O2 Then

                m_clsAboveTrayToO2.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveBackward()
                m_clsHomeToAboveBoard.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.O3 Then

                m_clsAboveTrayToO3.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveBackward()
                m_clsHomeToAboveBoard.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.O4 Then

                m_clsAboveTrayToO4.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveBackward()
                m_clsHomeToAboveBoard.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.O5 Then

                m_clsAboveTrayToO5.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveBackward()
                m_clsHomeToAboveBoard.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.X1 Then

                m_clsAboveTrayToX1.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveBackward()
                m_clsHomeToAboveBoard.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.X2 Then

                m_clsAboveTrayToX2.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveBackward()
                m_clsHomeToAboveBoard.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.X3 Then

                m_clsAboveTrayToX3.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveBackward()
                m_clsHomeToAboveBoard.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.X4 Then

                m_clsAboveTrayToX4.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveBackward()
                m_clsHomeToAboveBoard.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.X5 Then

                m_clsAboveTrayToX5.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveBackward()
                m_clsHomeToAboveBoard.MoveBackward()

            End If

            m_enuCurrentLocation = enuLocationType.Home

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub


    ' -------------------------------------------------------------------------
    ' Name: MoveToAboveBoard
    ' Abstract: Moves to above board
    ' -------------------------------------------------------------------------
    Public Sub MoveToAboveBoard()

        Try

            If m_enuCurrentLocation = enuLocationType.Home Then

                m_clsHomeToAboveBoard.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.AboveBoard Then

                ' Do Nothing

            ElseIf m_enuCurrentLocation = enuLocationType.AboveTray Then

                m_clsAboveBoardToAboveTray.MoveBackward()
                
            ElseIf m_enuCurrentLocation = enuLocationType.Square1 Then

                m_clsAboveBoardToSquare1.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square2 Then

                m_clsAboveBoardToSquare2.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square3 Then

                m_clsAboveBoardToSquare3.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square4 Then

                m_clsAboveBoardToSquare4.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square5 Then

                m_clsAboveBoardToSquare5.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square6 Then

                m_clsAboveBoardToSquare6.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square7 Then

                m_clsAboveBoardToSquare7.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square8 Then

                m_clsAboveBoardToSquare8.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square9 Then

                m_clsAboveBoardToSquare9.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.O1 Then

                m_clsAboveTrayToO1.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.O2 Then

                m_clsAboveTrayToO2.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.O3 Then

                m_clsAboveTrayToO3.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.O4 Then

                m_clsAboveTrayToO4.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.O5 Then

                m_clsAboveTrayToO5.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.X1 Then

                m_clsAboveTrayToX1.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.X2 Then

                m_clsAboveTrayToX2.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.X3 Then

                m_clsAboveTrayToX3.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.X4 Then

                m_clsAboveTrayToX4.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.X5 Then

                m_clsAboveTrayToX5.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveBackward()

            End If

            m_enuCurrentLocation = enuLocationType.AboveBoard


        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub


    ' -------------------------------------------------------------------------
    ' Name: MoveToAboveTray
    ' Abstract: Moves to above Tray
    ' -------------------------------------------------------------------------
    Public Sub MoveToAboveTray()

        Try

            If m_enuCurrentLocation = enuLocationType.Home Then

                m_clsHomeToAboveBoard.MoveForward()
                m_clsAboveBoardToAboveTray.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.AboveBoard Then

                m_clsAboveBoardToAboveTray.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.AboveTray Then

                ' Do Nothing

            ElseIf m_enuCurrentLocation = enuLocationType.Square1 Then

                m_clsAboveBoardToSquare1.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square2 Then

                m_clsAboveBoardToSquare2.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square3 Then

                m_clsAboveBoardToSquare3.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square4 Then

                m_clsAboveBoardToSquare4.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square5 Then

                m_clsAboveBoardToSquare5.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square6 Then

                m_clsAboveBoardToSquare6.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square7 Then

                m_clsAboveBoardToSquare7.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square8 Then

                m_clsAboveBoardToSquare8.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square9 Then

                m_clsAboveBoardToSquare9.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.O1 Then

                m_clsAboveTrayToO1.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.O2 Then

                m_clsAboveTrayToO2.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.O3 Then

                m_clsAboveTrayToO3.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.O4 Then

                m_clsAboveTrayToO4.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.O5 Then

                m_clsAboveTrayToO5.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.X1 Then

                m_clsAboveTrayToX1.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.X2 Then

                m_clsAboveTrayToX2.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.X3 Then

                m_clsAboveTrayToX3.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.X4 Then

                m_clsAboveTrayToX4.MoveBackward()

            ElseIf m_enuCurrentLocation = enuLocationType.X5 Then

                m_clsAboveTrayToX5.MoveBackward()

            End If

            m_enuCurrentLocation = enuLocationType.AboveTray


        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: MoveToSquareOne
    ' Abstract: Move to square one
    ' -------------------------------------------------------------------------
    Public Sub MoveToSquareOne()

        Try

            If m_enuCurrentLocation = enuLocationType.Home Then

                m_clsHomeToAboveBoard.MoveForward()
                m_clsAboveBoardToSquare1.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.AboveBoard Then

                m_clsAboveBoardToSquare1.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.AboveTray Then

                m_clsAboveBoardToAboveTray.MoveBackward()
                m_clsAboveBoardToSquare1.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square1 Then

                ' Do Nothing

            ElseIf m_enuCurrentLocation = enuLocationType.Square2 Then

                m_clsAboveBoardToSquare2.MoveBackward()
                m_clsAboveBoardToSquare1.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square3 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square4 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square5 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square6 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square7 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square8 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square9 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O1 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O2 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O3 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O4 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O5 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X1 Then

                m_clsAboveTrayToX1.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveBackward()
                m_clsAboveBoardToSquare1.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.X2 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X3 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X4 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X5 Then
            End If

            m_enuCurrentLocation = enuLocationType.Square1

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: MoveToSquareTwo
    ' Abstract: Move to square two
    ' -------------------------------------------------------------------------
    Public Sub MoveToSquareTwo()

        Try

            If m_enuCurrentLocation = enuLocationType.Home Then

                m_clsHomeToAboveBoard.MoveForward()
                m_clsAboveBoardToSquare2.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.AboveBoard Then

                m_clsAboveBoardToSquare2.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.AboveTray Then

                m_clsAboveBoardToAboveTray.MoveBackward()
                m_clsAboveBoardToSquare2.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square1 Then

                m_clsAboveBoardToSquare1.MoveBackward()
                m_clsAboveBoardToSquare2.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square2 Then

                ' DO Nothing

            ElseIf m_enuCurrentLocation = enuLocationType.Square3 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square4 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square5 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square6 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square7 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square8 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square9 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O1 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O2 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O3 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O4 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O5 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X1 Then

                m_clsAboveTrayToX1.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveBackward()
                m_clsAboveBoardToSquare2.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.X2 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X3 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X4 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X5 Then
            End If

            m_enuCurrentLocation = enuLocationType.Square2

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: MoveToSquareThree
    ' Abstract: Move to square three
    ' -------------------------------------------------------------------------
    Public Sub MoveToSquareThree()

        Try

            If m_enuCurrentLocation = enuLocationType.Home Then

                m_clsHomeToAboveBoard.MoveForward()
                m_clsAboveBoardToSquare3.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.AboveBoard Then

                m_clsAboveBoardToSquare3.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.AboveTray Then

                m_clsAboveBoardToAboveTray.MoveBackward()
                m_clsAboveBoardToSquare3.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square1 Then

            ElseIf m_enuCurrentLocation = enuLocationType.Square2 Then



            ElseIf m_enuCurrentLocation = enuLocationType.Square3 Then

                ' Do Nothing

            ElseIf m_enuCurrentLocation = enuLocationType.Square4 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square5 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square6 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square7 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square8 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square9 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O1 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O2 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O3 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O4 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O5 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X1 Then

                m_clsAboveTrayToX1.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveBackward()
                m_clsAboveBoardToSquare2.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.X2 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X3 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X4 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X5 Then
            End If

            m_enuCurrentLocation = enuLocationType.Square3

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub




    ' -------------------------------------------------------------------------
    ' Name: MoveToSquareFour
    ' Abstract: Move to square Four
    ' -------------------------------------------------------------------------
    Public Sub MoveToSquareFour()

        Try

            If m_enuCurrentLocation = enuLocationType.Home Then

                m_clsHomeToAboveBoard.MoveForward()
                m_clsAboveBoardToSquare4.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.AboveBoard Then

                m_clsAboveBoardToSquare4.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.AboveTray Then

                m_clsAboveBoardToAboveTray.MoveBackward()
                m_clsAboveBoardToSquare4.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square1 Then

            ElseIf m_enuCurrentLocation = enuLocationType.Square2 Then



            ElseIf m_enuCurrentLocation = enuLocationType.Square3 Then



            ElseIf m_enuCurrentLocation = enuLocationType.Square4 Then

                ' Do Nothing

            ElseIf m_enuCurrentLocation = enuLocationType.Square5 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square6 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square7 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square8 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square9 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O1 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O2 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O3 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O4 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O5 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X1 Then

                m_clsAboveTrayToX1.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveBackward()
                m_clsAboveBoardToSquare4.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.X2 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X3 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X4 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X5 Then
            End If

            m_enuCurrentLocation = enuLocationType.Square4

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub




    ' -------------------------------------------------------------------------
    ' Name: MoveToSquareFive
    ' Abstract: Move to square five
    ' -------------------------------------------------------------------------
    Public Sub MoveToSquareFive()

        Try

            If m_enuCurrentLocation = enuLocationType.Home Then

                m_clsHomeToAboveBoard.MoveForward()
                m_clsAboveBoardToSquare5.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.AboveBoard Then

                m_clsAboveBoardToSquare5.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.AboveTray Then

                m_clsAboveBoardToAboveTray.MoveBackward()
                m_clsAboveBoardToSquare5.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square1 Then

            ElseIf m_enuCurrentLocation = enuLocationType.Square2 Then



            ElseIf m_enuCurrentLocation = enuLocationType.Square3 Then



            ElseIf m_enuCurrentLocation = enuLocationType.Square4 Then



            ElseIf m_enuCurrentLocation = enuLocationType.Square5 Then

                ' Do Nothing

            ElseIf m_enuCurrentLocation = enuLocationType.Square6 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square7 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square8 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square9 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O1 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O2 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O3 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O4 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O5 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X1 Then

                m_clsAboveTrayToX1.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveBackward()
                m_clsAboveBoardToSquare5.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.X2 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X3 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X4 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X5 Then
            End If

            m_enuCurrentLocation = enuLocationType.Square5

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub




    ' -------------------------------------------------------------------------
    ' Name: MoveToSquareSix
    ' Abstract: Move to square six
    ' -------------------------------------------------------------------------
    Public Sub MoveToSquareSix()

        Try

            If m_enuCurrentLocation = enuLocationType.Home Then

                m_clsHomeToAboveBoard.MoveForward()
                m_clsAboveBoardToSquare6.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.AboveBoard Then

                m_clsAboveBoardToSquare6.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.AboveTray Then

                m_clsAboveBoardToAboveTray.MoveBackward()
                m_clsAboveBoardToSquare6.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square1 Then

            ElseIf m_enuCurrentLocation = enuLocationType.Square2 Then



            ElseIf m_enuCurrentLocation = enuLocationType.Square3 Then



            ElseIf m_enuCurrentLocation = enuLocationType.Square4 Then



            ElseIf m_enuCurrentLocation = enuLocationType.Square5 Then



            ElseIf m_enuCurrentLocation = enuLocationType.Square6 Then

                ' Do Nothing

            ElseIf m_enuCurrentLocation = enuLocationType.Square7 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square8 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square9 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O1 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O2 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O3 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O4 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O5 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X1 Then

                m_clsAboveTrayToX1.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveBackward()
                m_clsAboveBoardToSquare5.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.X2 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X3 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X4 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X5 Then
            End If

            m_enuCurrentLocation = enuLocationType.Square6

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub




    ' -------------------------------------------------------------------------
    ' Name: MoveToSquareSeven
    ' Abstract: Move to square seven
    ' -------------------------------------------------------------------------
    Public Sub MoveToSquareSeven()

        Try

            If m_enuCurrentLocation = enuLocationType.Home Then

                m_clsHomeToAboveBoard.MoveForward()
                m_clsAboveBoardToSquare7.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.AboveBoard Then

                m_clsAboveBoardToSquare7.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.AboveTray Then

                m_clsAboveBoardToAboveTray.MoveBackward()
                m_clsAboveBoardToSquare7.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square1 Then

            ElseIf m_enuCurrentLocation = enuLocationType.Square2 Then



            ElseIf m_enuCurrentLocation = enuLocationType.Square3 Then



            ElseIf m_enuCurrentLocation = enuLocationType.Square4 Then



            ElseIf m_enuCurrentLocation = enuLocationType.Square5 Then



            ElseIf m_enuCurrentLocation = enuLocationType.Square6 Then



            ElseIf m_enuCurrentLocation = enuLocationType.Square7 Then

                ' Do Nothing

            ElseIf m_enuCurrentLocation = enuLocationType.Square8 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square9 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O1 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O2 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O3 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O4 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O5 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X1 Then

                m_clsAboveTrayToX1.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveBackward()
                m_clsAboveBoardToSquare5.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.X2 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X3 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X4 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X5 Then
            End If

            m_enuCurrentLocation = enuLocationType.Square7

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub




    ' -------------------------------------------------------------------------
    ' Name: MoveToSquareEight
    ' Abstract: Move to square Eight
    ' -------------------------------------------------------------------------
    Public Sub MoveToSquareEight()

        Try

            If m_enuCurrentLocation = enuLocationType.Home Then

                m_clsHomeToAboveBoard.MoveForward()
                m_clsAboveBoardToSquare8.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.AboveBoard Then

                m_clsAboveBoardToSquare8.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.AboveTray Then

                m_clsAboveBoardToAboveTray.MoveBackward()
                m_clsAboveBoardToSquare8.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square1 Then

            ElseIf m_enuCurrentLocation = enuLocationType.Square2 Then



            ElseIf m_enuCurrentLocation = enuLocationType.Square3 Then



            ElseIf m_enuCurrentLocation = enuLocationType.Square4 Then



            ElseIf m_enuCurrentLocation = enuLocationType.Square5 Then



            ElseIf m_enuCurrentLocation = enuLocationType.Square6 Then



            ElseIf m_enuCurrentLocation = enuLocationType.Square7 Then



            ElseIf m_enuCurrentLocation = enuLocationType.Square8 Then

                ' Do Nothing

            ElseIf m_enuCurrentLocation = enuLocationType.Square9 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O1 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O2 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O3 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O4 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O5 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X1 Then

                m_clsAboveTrayToX1.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveBackward()
                m_clsAboveBoardToSquare5.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.X2 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X3 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X4 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X5 Then
            End If

            m_enuCurrentLocation = enuLocationType.Square8

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub




    ' -------------------------------------------------------------------------
    ' Name: MoveToSquareNine
    ' Abstract: Move to square Nine
    ' -------------------------------------------------------------------------
    Public Sub MoveToSquareNine()

        Try

            If m_enuCurrentLocation = enuLocationType.Home Then

                m_clsHomeToAboveBoard.MoveForward()
                m_clsAboveBoardToSquare9.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.AboveBoard Then

                m_clsAboveBoardToSquare9.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.AboveTray Then

                m_clsAboveBoardToAboveTray.MoveBackward()
                m_clsAboveBoardToSquare9.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square1 Then

            ElseIf m_enuCurrentLocation = enuLocationType.Square2 Then



            ElseIf m_enuCurrentLocation = enuLocationType.Square3 Then



            ElseIf m_enuCurrentLocation = enuLocationType.Square4 Then



            ElseIf m_enuCurrentLocation = enuLocationType.Square5 Then



            ElseIf m_enuCurrentLocation = enuLocationType.Square6 Then



            ElseIf m_enuCurrentLocation = enuLocationType.Square7 Then



            ElseIf m_enuCurrentLocation = enuLocationType.Square8 Then

                ' Do Nothing

            ElseIf m_enuCurrentLocation = enuLocationType.Square9 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O1 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O2 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O3 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O4 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O5 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X1 Then

                m_clsAboveTrayToX1.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveBackward()
                m_clsAboveBoardToSquare5.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.X2 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X3 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X4 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X5 Then
            End If

            m_enuCurrentLocation = enuLocationType.Square9

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: MoveToXOne
    ' Abstract: Move to X one
    ' -------------------------------------------------------------------------
    Public Sub MoveToXOne()

        Try

            If m_enuCurrentLocation = enuLocationType.Home Then

                m_clsHomeToAboveBoard.MoveForward()
                m_clsAboveBoardToAboveTray.MoveForward()
                m_clsAboveTrayToX1.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.AboveBoard Then

                m_clsAboveBoardToAboveTray.MoveForward()
                m_clsAboveTrayToX1.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.AboveTray Then

                m_clsAboveTrayToX1.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square1 Then

                m_clsAboveBoardToSquare1.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveForward()
                m_clsAboveTrayToX1.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square2 Then

                

            ElseIf m_enuCurrentLocation = enuLocationType.Square3 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square4 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square5 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square6 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square7 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square8 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square9 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O1 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O2 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O3 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O4 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O5 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X1 Then
                ' Do Nothing
            ElseIf m_enuCurrentLocation = enuLocationType.X2 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X3 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X4 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X5 Then
            End If

            m_enuCurrentLocation = enuLocationType.X1

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: MoveToXTwo
    ' Abstract: Move to X two
    ' -------------------------------------------------------------------------
    Public Sub MoveToXTwo()

        Try

            If m_enuCurrentLocation = enuLocationType.Home Then

                m_clsHomeToAboveBoard.MoveForward()
                m_clsAboveBoardToAboveTray.MoveForward()
                m_clsAboveTrayToX2.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.AboveBoard Then

                m_clsAboveBoardToAboveTray.MoveForward()
                m_clsAboveTrayToX2.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.AboveTray Then

                m_clsAboveTrayToX2.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square1 Then

                m_clsAboveBoardToSquare1.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveForward()
                m_clsAboveTrayToX2.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square2 Then



            ElseIf m_enuCurrentLocation = enuLocationType.Square3 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square4 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square5 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square6 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square7 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square8 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square9 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O1 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O2 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O3 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O4 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O5 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X1 Then

                m_clsAboveTrayToX1.MoveBackward()
                m_clsAboveTrayToX2.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.X2 Then
                ' Do Nothing
            ElseIf m_enuCurrentLocation = enuLocationType.X3 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X4 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X5 Then
            End If

            m_enuCurrentLocation = enuLocationType.X2

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: MoveToXThree
    ' Abstract: Move to X Three
    ' -------------------------------------------------------------------------
    Public Sub MoveToXThree()

        Try

            If m_enuCurrentLocation = enuLocationType.Home Then

                m_clsHomeToAboveBoard.MoveForward()
                m_clsAboveBoardToAboveTray.MoveForward()
                m_clsAboveTrayToX3.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.AboveBoard Then

                m_clsAboveBoardToAboveTray.MoveForward()
                m_clsAboveTrayToX3.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.AboveTray Then

                m_clsAboveTrayToX3.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square1 Then

                m_clsAboveBoardToSquare1.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveForward()
                m_clsAboveTrayToX3.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square2 Then



            ElseIf m_enuCurrentLocation = enuLocationType.Square3 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square4 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square5 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square6 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square7 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square8 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square9 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O1 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O2 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O3 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O4 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O5 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X1 Then

                m_clsAboveTrayToX1.MoveBackward()
                m_clsAboveTrayToX3.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.X2 Then

                m_clsAboveTrayToX2.MoveBackward()
                m_clsAboveTrayToX3.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.X3 Then

                ' Do Nothing

            ElseIf m_enuCurrentLocation = enuLocationType.X4 Then

                m_clsAboveTrayToX4.MoveBackward()
                m_clsAboveTrayToX3.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.X5 Then
            End If

            m_enuCurrentLocation = enuLocationType.X3

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: MoveToXFour
    ' Abstract: Move to X Four
    ' -------------------------------------------------------------------------
    Public Sub MoveToXFour()

        Try

            If m_enuCurrentLocation = enuLocationType.Home Then

                m_clsHomeToAboveBoard.MoveForward()
                m_clsAboveBoardToAboveTray.MoveForward()
                m_clsAboveTrayToX4.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.AboveBoard Then

                m_clsAboveBoardToAboveTray.MoveForward()
                m_clsAboveTrayToX4.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.AboveTray Then

                m_clsAboveTrayToX4.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square1 Then

                m_clsAboveBoardToSquare1.MoveBackward()
                m_clsAboveBoardToAboveTray.MoveForward()
                m_clsAboveTrayToX4.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square2 Then



            ElseIf m_enuCurrentLocation = enuLocationType.Square3 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square4 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square5 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square6 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square7 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square8 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square9 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O1 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O2 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O3 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O4 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O5 Then
            ElseIf m_enuCurrentLocation = enuLocationType.X1 Then

                m_clsAboveTrayToX1.MoveBackward()
                m_clsAboveTrayToX4.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.X2 Then

                m_clsAboveTrayToX2.MoveBackward()
                m_clsAboveTrayToX4.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.X3 Then

                m_clsAboveTrayToX3.MoveBackward()
                m_clsAboveTrayToX4.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.X4 Then

                ' Do Nothing

            ElseIf m_enuCurrentLocation = enuLocationType.X5 Then
            End If

            m_enuCurrentLocation = enuLocationType.X4

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

	End Sub



	' -------------------------------------------------------------------------
	' Name: MoveToXFive
	' Abstract: Move to X five
	' -------------------------------------------------------------------------
	Public Sub MoveToXFive()

		Try

			If m_enuCurrentLocation = enuLocationType.Home Then

				m_clsHomeToAboveBoard.MoveForward()
				m_clsAboveBoardToAboveTray.MoveForward()
				m_clsAboveTrayToX5.MoveForward()

			ElseIf m_enuCurrentLocation = enuLocationType.AboveBoard Then

				m_clsAboveBoardToAboveTray.MoveForward()
				m_clsAboveTrayToX5.MoveForward()

			ElseIf m_enuCurrentLocation = enuLocationType.AboveTray Then

				m_clsAboveTrayToX5.MoveForward()

			ElseIf m_enuCurrentLocation = enuLocationType.Square1 Then

				

			ElseIf m_enuCurrentLocation = enuLocationType.Square2 Then



			ElseIf m_enuCurrentLocation = enuLocationType.Square3 Then
			ElseIf m_enuCurrentLocation = enuLocationType.Square4 Then
			ElseIf m_enuCurrentLocation = enuLocationType.Square5 Then
			ElseIf m_enuCurrentLocation = enuLocationType.Square6 Then
			ElseIf m_enuCurrentLocation = enuLocationType.Square7 Then
			ElseIf m_enuCurrentLocation = enuLocationType.Square8 Then
			ElseIf m_enuCurrentLocation = enuLocationType.Square9 Then
			ElseIf m_enuCurrentLocation = enuLocationType.O1 Then
			ElseIf m_enuCurrentLocation = enuLocationType.O2 Then
			ElseIf m_enuCurrentLocation = enuLocationType.O3 Then
			ElseIf m_enuCurrentLocation = enuLocationType.O4 Then
			ElseIf m_enuCurrentLocation = enuLocationType.O5 Then
			ElseIf m_enuCurrentLocation = enuLocationType.X1 Then

				m_clsAboveTrayToX1.MoveBackward()
				m_clsAboveTrayToX5.MoveForward()

			ElseIf m_enuCurrentLocation = enuLocationType.X2 Then

				m_clsAboveTrayToX2.MoveBackward()
				m_clsAboveTrayToX5.MoveForward()

			ElseIf m_enuCurrentLocation = enuLocationType.X3 Then

				m_clsAboveTrayToX3.MoveBackward()
				m_clsAboveTrayToX5.MoveForward()

			ElseIf m_enuCurrentLocation = enuLocationType.X4 Then

				m_clsAboveTrayToX4.MoveBackward()
				m_clsAboveTrayToX5.MoveForward()

			ElseIf m_enuCurrentLocation = enuLocationType.X5 Then

				' Do nothing

			End If

			m_enuCurrentLocation = enuLocationType.X5

		Catch excError As Exception

			WriteLog(excError.ToString)

		End Try

	End Sub



	' -------------------------------------------------------------------------
	' Name: MoveTo O one
	' Abstract: Move to O one
	' -------------------------------------------------------------------------
	Public Sub MoveToOOne()

		Try

			If m_enuCurrentLocation = enuLocationType.Home Then

				m_clsHomeToAboveBoard.MoveForward()
				m_clsAboveBoardToAboveTray.MoveForward()
				m_clsAboveTrayToO1.MoveForward()

			ElseIf m_enuCurrentLocation = enuLocationType.AboveBoard Then

				m_clsAboveBoardToAboveTray.MoveForward()
				m_clsAboveTrayToO1.MoveForward()

			ElseIf m_enuCurrentLocation = enuLocationType.AboveTray Then

				m_clsAboveTrayToO1.MoveForward()

			ElseIf m_enuCurrentLocation = enuLocationType.Square1 Then



			ElseIf m_enuCurrentLocation = enuLocationType.Square2 Then



			ElseIf m_enuCurrentLocation = enuLocationType.Square3 Then
			ElseIf m_enuCurrentLocation = enuLocationType.Square4 Then
			ElseIf m_enuCurrentLocation = enuLocationType.Square5 Then
			ElseIf m_enuCurrentLocation = enuLocationType.Square6 Then
			ElseIf m_enuCurrentLocation = enuLocationType.Square7 Then
			ElseIf m_enuCurrentLocation = enuLocationType.Square8 Then
			ElseIf m_enuCurrentLocation = enuLocationType.Square9 Then
			ElseIf m_enuCurrentLocation = enuLocationType.O1 Then

				' Do Nothing

			ElseIf m_enuCurrentLocation = enuLocationType.O2 Then

				m_clsAboveTrayToO2.MoveBackward()
				m_clsAboveTrayToO1.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.O3 Then

                m_clsAboveTrayToO3.MoveBackward()
                m_clsAboveTrayToO1.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.O4 Then

                m_clsAboveTrayToO4.MoveBackward()
                m_clsAboveTrayToO1.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.O5 Then

                m_clsAboveTrayToO5.MoveBackward()
                m_clsAboveTrayToO1.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.X1 Then

                m_clsAboveTrayToX1.MoveBackward()
                m_clsAboveTrayToO1.MoveForward()

			ElseIf m_enuCurrentLocation = enuLocationType.X2 Then

				m_clsAboveTrayToX2.MoveBackward()
				m_clsAboveTrayToO1.MoveForward()

			ElseIf m_enuCurrentLocation = enuLocationType.X3 Then

				m_clsAboveTrayToX3.MoveBackward()
				m_clsAboveTrayToO1.MoveForward()

			ElseIf m_enuCurrentLocation = enuLocationType.X4 Then

				m_clsAboveTrayToX4.MoveBackward()
				m_clsAboveTrayToO1.MoveForward()

			ElseIf m_enuCurrentLocation = enuLocationType.X5 Then

				m_clsAboveTrayToX5.MoveBackward()
				m_clsAboveTrayToO1.MoveForward()

			End If

            m_enuCurrentLocation = enuLocationType.O1

		Catch excError As Exception

			WriteLog(excError.ToString)

		End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: MoveTo O two
    ' Abstract: Move to O two
    ' -------------------------------------------------------------------------
    Public Sub MoveToOTwo()

        Try

            If m_enuCurrentLocation = enuLocationType.Home Then

                m_clsHomeToAboveBoard.MoveForward()
                m_clsAboveBoardToAboveTray.MoveForward()
                m_clsAboveTrayToO2.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.AboveBoard Then

                m_clsAboveBoardToAboveTray.MoveForward()
                m_clsAboveTrayToO2.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.AboveTray Then

                m_clsAboveTrayToO2.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square1 Then



            ElseIf m_enuCurrentLocation = enuLocationType.Square2 Then



            ElseIf m_enuCurrentLocation = enuLocationType.Square3 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square4 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square5 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square6 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square7 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square8 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square9 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O1 Then

                m_clsAboveTrayToO1.MoveBackward()
                m_clsAboveTrayToO2.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.O2 Then

                ' Do Nothing

            ElseIf m_enuCurrentLocation = enuLocationType.O3 Then

                m_clsAboveTrayToO3.MoveBackward()
                m_clsAboveTrayToO2.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.O4 Then

                m_clsAboveTrayToO4.MoveBackward()
                m_clsAboveTrayToO2.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.O5 Then

                m_clsAboveTrayToO5.MoveBackward()
                m_clsAboveTrayToO2.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.X1 Then

                m_clsAboveTrayToX1.MoveBackward()
                m_clsAboveTrayToO2.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.X2 Then

                m_clsAboveTrayToX2.MoveBackward()
                m_clsAboveTrayToO2.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.X3 Then

                m_clsAboveTrayToX3.MoveBackward()
                m_clsAboveTrayToO2.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.X4 Then

                m_clsAboveTrayToX4.MoveBackward()
                m_clsAboveTrayToO2.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.X5 Then

                m_clsAboveTrayToX5.MoveBackward()
                m_clsAboveTrayToO2.MoveForward()

            End If

            m_enuCurrentLocation = enuLocationType.O2

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: MoveTo O two
    ' Abstract: Move to O two
    ' -------------------------------------------------------------------------
    Public Sub MoveToOThree()

        Try

            If m_enuCurrentLocation = enuLocationType.Home Then

                m_clsHomeToAboveBoard.MoveForward()
                m_clsAboveBoardToAboveTray.MoveForward()
                m_clsAboveTrayToO3.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.AboveBoard Then

                m_clsAboveBoardToAboveTray.MoveForward()
                m_clsAboveTrayToO3.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.AboveTray Then

                m_clsAboveTrayToO3.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square1 Then



            ElseIf m_enuCurrentLocation = enuLocationType.Square2 Then



            ElseIf m_enuCurrentLocation = enuLocationType.Square3 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square4 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square5 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square6 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square7 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square8 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square9 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O1 Then

                m_clsAboveTrayToO1.MoveBackward()
                m_clsAboveTrayToO3.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.O2 Then

                m_clsAboveTrayToO2.MoveBackward()
                m_clsAboveTrayToO3.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.O3 Then

                ' Do Nothing

            ElseIf m_enuCurrentLocation = enuLocationType.O4 Then

                m_clsAboveTrayToO4.MoveBackward()
                m_clsAboveTrayToO3.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.O5 Then

                m_clsAboveTrayToO5.MoveBackward()
                m_clsAboveTrayToO3.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.X1 Then

                m_clsAboveTrayToX1.MoveBackward()
                m_clsAboveTrayToO3.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.X2 Then

                m_clsAboveTrayToX2.MoveBackward()
                m_clsAboveTrayToO3.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.X3 Then

                m_clsAboveTrayToX3.MoveBackward()
                m_clsAboveTrayToO3.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.X4 Then

                m_clsAboveTrayToX4.MoveBackward()
                m_clsAboveTrayToO3.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.X5 Then

                m_clsAboveTrayToX5.MoveBackward()
                m_clsAboveTrayToO3.MoveForward()

            End If

            m_enuCurrentLocation = enuLocationType.O3

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: MoveToOFour
    ' Abstract: Move to O four
    ' -------------------------------------------------------------------------
    Public Sub MoveToOFour()

        Try

            If m_enuCurrentLocation = enuLocationType.Home Then

                m_clsHomeToAboveBoard.MoveForward()
                m_clsAboveBoardToAboveTray.MoveForward()
                m_clsAboveTrayToO4.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.AboveBoard Then

                m_clsAboveBoardToAboveTray.MoveForward()
                m_clsAboveTrayToO4.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.AboveTray Then

                m_clsAboveTrayToO4.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square1 Then



            ElseIf m_enuCurrentLocation = enuLocationType.Square2 Then



            ElseIf m_enuCurrentLocation = enuLocationType.Square3 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square4 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square5 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square6 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square7 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square8 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square9 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O1 Then

                m_clsAboveTrayToO1.MoveBackward()
                m_clsAboveTrayToO4.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.O2 Then

                m_clsAboveTrayToO2.MoveBackward()
                m_clsAboveTrayToO4.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.O3 Then

                m_clsAboveTrayToO3.MoveBackward()
                m_clsAboveTrayToO4.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.O4 Then

                ' Do Nothing

            ElseIf m_enuCurrentLocation = enuLocationType.O5 Then

                m_clsAboveTrayToO5.MoveBackward()
                m_clsAboveTrayToO4.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.X1 Then

                m_clsAboveTrayToX1.MoveBackward()
                m_clsAboveTrayToO4.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.X2 Then

                m_clsAboveTrayToX2.MoveBackward()
                m_clsAboveTrayToO4.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.X3 Then

                m_clsAboveTrayToX3.MoveBackward()
                m_clsAboveTrayToO4.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.X4 Then

                m_clsAboveTrayToX4.MoveBackward()
                m_clsAboveTrayToO4.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.X5 Then

                m_clsAboveTrayToX5.MoveBackward()
                m_clsAboveTrayToO4.MoveForward()

            End If

            m_enuCurrentLocation = enuLocationType.O4

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: MoveToOFive
    ' Abstract: Move to O five
    ' -------------------------------------------------------------------------
    Public Sub MoveToOFive()

        Try

            If m_enuCurrentLocation = enuLocationType.Home Then

                m_clsHomeToAboveBoard.MoveForward()
                m_clsAboveBoardToAboveTray.MoveForward()
                m_clsAboveTrayToO5.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.AboveBoard Then

                m_clsAboveBoardToAboveTray.MoveForward()
                m_clsAboveTrayToO5.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.AboveTray Then

                m_clsAboveTrayToO5.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.Square1 Then



            ElseIf m_enuCurrentLocation = enuLocationType.Square2 Then



            ElseIf m_enuCurrentLocation = enuLocationType.Square3 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square4 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square5 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square6 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square7 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square8 Then
            ElseIf m_enuCurrentLocation = enuLocationType.Square9 Then
            ElseIf m_enuCurrentLocation = enuLocationType.O1 Then

                m_clsAboveTrayToO1.MoveBackward()
                m_clsAboveTrayToO5.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.O2 Then

                m_clsAboveTrayToO2.MoveBackward()
                m_clsAboveTrayToO5.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.O3 Then

                m_clsAboveTrayToO3.MoveBackward()
                m_clsAboveTrayToO5.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.O4 Then

                m_clsAboveTrayToO4.MoveBackward()
                m_clsAboveTrayToO5.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.O5 Then

                ' Do Nothing

            ElseIf m_enuCurrentLocation = enuLocationType.X1 Then

                m_clsAboveTrayToX1.MoveBackward()
                m_clsAboveTrayToO5.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.X2 Then

                m_clsAboveTrayToX2.MoveBackward()
                m_clsAboveTrayToO5.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.X3 Then

                m_clsAboveTrayToX3.MoveBackward()
                m_clsAboveTrayToO5.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.X4 Then

                m_clsAboveTrayToX4.MoveBackward()
                m_clsAboveTrayToO5.MoveForward()

            ElseIf m_enuCurrentLocation = enuLocationType.X5 Then

                m_clsAboveTrayToX5.MoveBackward()
                m_clsAboveTrayToO5.MoveForward()

            End If

            m_enuCurrentLocation = enuLocationType.O5

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub

End Module
