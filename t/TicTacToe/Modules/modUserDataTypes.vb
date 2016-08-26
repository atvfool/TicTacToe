' -------------------------------------------------------------------------
' Module: modTicTacToe
' Author: Andrew Hayden
' Abstract: Tic tac toe module
'
' Revision        Owner   Changes:
' 1  2013/05/30   A.H.    Created.
' -------------------------------------------------------------------------

' -------------------------------------------------------------------------
' Options
' -------------------------------------------------------------------------
Option Explicit On

' -------------------------------------------------------------------------
' Imports
' -------------------------------------------------------------------------
Imports System.Runtime.CompilerServices


Public Module modUserDataTypes

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


    <Extension()>
    Public Function GetJointLetter(ByVal enuJoint As enuJointType)

        Dim strJointLetter As String = ""

        ' Add joint
        Select Case enuJoint

            Case enuJointType.JointA : strJointLetter = "A"
            Case enuJointType.JointB : strJointLetter = "B"
            Case enuJointType.JointC : strJointLetter = "C"
            Case enuJointType.JointD : strJointLetter = "D"
            Case enuJointType.JointE : strJointLetter = "E"
            Case enuJointType.JointF : strJointLetter = "F"

        End Select

        Return strJointLetter

    End Function


    Public Enum enuLocationType

        Home = 1
        AboveBoard
        AboveTray
        Square1
        Square2
        Square3
        Square4
        Square5
        Square6
        Square7
        Square8
        Square9
        X1
        X2
        X3
        X4
        X5
        O1
        O2
        O3
        O4
        O5
        
    End Enum


    'If m_enuCurrentLocation = enuLocationType.Home Then
    'ElseIf m_enuCurrentLocation = enuLocationType.AboveBoard Then
    'ElseIf m_enuCurrentLocation = enuLocationType.AboveTray Then
    'ElseIf m_enuCurrentLocation = enuLocationType.Square1 Then
    'ElseIf m_enuCurrentLocation = enuLocationType.Square2 Then
    'ElseIf m_enuCurrentLocation = enuLocationType.Square3 Then
    'ElseIf m_enuCurrentLocation = enuLocationType.Square4 Then
    'ElseIf m_enuCurrentLocation = enuLocationType.Square5 Then
    'ElseIf m_enuCurrentLocation = enuLocationType.Square6 Then
    'ElseIf m_enuCurrentLocation = enuLocationType.Square7 Then
    'ElseIf m_enuCurrentLocation = enuLocationType.Square8 Then
    'ElseIf m_enuCurrentLocation = enuLocationType.Square9 Then
    'ElseIf m_enuCurrentLocation = enuLocationType.O1 Then
    'ElseIf m_enuCurrentLocation = enuLocationType.O2 Then
    'ElseIf m_enuCurrentLocation = enuLocationType.O3 Then
    'ElseIf m_enuCurrentLocation = enuLocationType.O4 Then
    'ElseIf m_enuCurrentLocation = enuLocationType.O5 Then
    'ElseIf m_enuCurrentLocation = enuLocationType.X1 Then
    'ElseIf m_enuCurrentLocation = enuLocationType.X2 Then
    'ElseIf m_enuCurrentLocation = enuLocationType.X3 Then
    'ElseIf m_enuCurrentLocation = enuLocationType.X4 Then
    'ElseIf m_enuCurrentLocation = enuLocationType.X5 Then
    'End If

End Module
