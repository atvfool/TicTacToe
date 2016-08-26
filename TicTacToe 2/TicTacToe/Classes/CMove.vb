' -------------------------------------------------------------------------
' Class: CMove
' Author: Andrew Hayden
' Abstract: Moves any joint 1 distance
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



Public Class CMove



    ' -------------------------------------------------------------------------
    ' Constants
    ' -------------------------------------------------------------------------
    ' Break large moves down into smaller steps
    Public Const intMAXIMUM_STEP As Integer = 100
    Public Const intSTEP_DONE_OR_ALMOST_DONE As Integer = 10



    ' -------------------------------------------------------------------------
    ' Form variables
    ' -------------------------------------------------------------------------
    Private m_enuJoint As enuJointType
    Private m_intDistance As Integer
    Private m_intDirection As Integer
    Private m_intStepCount As Integer
    Private m_intLastStepDistance As Integer
    Private m_intCurrentStep As Integer



    ' -------------------------------------------------------------------------
    ' Name: New
    ' Abstract: constructor
    ' -------------------------------------------------------------------------
    Public Sub New(ByVal enuJoint As enuJointType, ByVal intDistance As Integer)

        Try

            Initialize(enuJoint, intDistance)

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try


    End Sub



    ' -------------------------------------------------------------------------
    ' Name: Initialize
    ' Abstract: set the distance and joints
    ' -------------------------------------------------------------------------
    Private Sub Initialize(ByVal enuJoint As enuJointType, ByVal intDistance As Integer)

        Try

            ' HUGE BUG but you need to find it so that you'll remember forever and ever and ever

            m_enuJoint = enuJoint

            If intDistance < 0 Then
                m_intDirection = -1
            Else
                m_intDirection = 1
            End If

            m_intDistance = Math.Abs(intDistance)

            ' How many full steps
            m_intStepCount = m_intDistance \ intMAXIMUM_STEP

            ' Partial step at end?
            m_intLastStepDistance = m_intDistance Mod intMAXIMUM_STEP
            'If m_intLastStepDistance > 0 And m_intLastStepDistance < 50 Then m_intStepCount += 1
            If m_intLastStepDistance > 0 Then m_intStepCount += 1


            ' We haven't moved yet
            m_intCurrentStep = 0

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub






    ' -------------------------------------------------------------------------
    ' Name: StepForwardWithoutWait
    ' Abstract: Take a step if we can
    ' -------------------------------------------------------------------------
    Public Sub StepForwardWithoutWait()

        Try

            Dim intDistance As Integer

            ' Do we need to take a step?
            If m_intCurrentStep < m_intStepCount Then

                ' Is last step done or almost done?
                If IsLastStepDoneOrAlmostDone() = True Then

                    ' Yes, take a step

                    ' Count the step
                    m_intCurrentStep += 1

                    ' Partial or Full step?
                    If m_intCurrentStep = m_intStepCount And m_intLastStepDistance <> 0 Then

                        ' Partial
                        intDistance = m_intLastStepDistance

                    Else

                        ' Full
                        intDistance = intMAXIMUM_STEP

                    End If

                    ' Forward
                    intDistance *= m_intDirection

                    MoveJoint(m_enuJoint, intDistance)

                End If

            End If

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub


    ' -------------------------------------------------------------------------
    ' Name: IsMoveForwardComplete
    ' Abstract: 
    ' -------------------------------------------------------------------------
    Public Function IsMoveForwardComplete()

        Dim blnIsMoveForwardComplete As Boolean = False

        Try

            If m_intCurrentStep >= m_intStepCount Then

                blnIsMoveForwardComplete = True

            End If

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

        Return blnIsMoveForwardComplete

    End Function


    ' -------------------------------------------------------------------------
    ' Name: StepBackwardWithoutWait
    ' Abstract: Take a step if we can
    ' -------------------------------------------------------------------------
    Public Sub StepBackwardWithoutWait()

        Try

            Dim intDistance As Integer

            ' Do we need to take a step?
            If m_intCurrentStep > 0 Then

                ' Is last step done or almost done?
                If IsLastStepDoneOrAlmostDone() = True Then

                    ' Yes, take a step

                    ' Partial or Full step?
                    If m_intCurrentStep = m_intStepCount And m_intLastStepDistance <> 0 Then

                        ' Partial
                        intDistance = m_intLastStepDistance

                    Else

                        ' Full
                        intDistance = intMAXIMUM_STEP

                    End If

                    ' Backward -1
                    intDistance *= m_intDirection * -1

                    MoveJoint(m_enuJoint, intDistance)

                    ' Count the step
                    m_intCurrentStep -= 1

                End If

            End If

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: IsMoveBackwardComplete
    ' Abstract: 
    ' -------------------------------------------------------------------------
    Public Function IsMoveBackwardComplete()

        Dim blnIsMoveBackwardComplete As Boolean = False

        Try

            If m_intCurrentStep <= 0 Then

                blnIsMoveBackwardComplete = True

            End If

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

        Return blnIsMoveBackwardComplete

    End Function



    ' -------------------------------------------------------------------------
    ' Name: IsLastStepDoneOrAlmostDone
    ' Abstract: 
    ' -------------------------------------------------------------------------
    Private Function IsLastStepDoneOrAlmostDone()

        Dim blnIsLastStepDoneOrAlmostDone As Boolean = False

        Try

            Dim intRemainingDistance As Integer

            intRemainingDistance = GetRemaingMoveDistance(m_enuJoint)

            If intRemainingDistance <= intSTEP_DONE_OR_ALMOST_DONE Then

                blnIsLastStepDoneOrAlmostDone = True

            End If

        Catch excError As Exception

            WriteLog(excError.ToString)

        End Try

        Return blnIsLastStepDoneOrAlmostDone

    End Function

End Class
