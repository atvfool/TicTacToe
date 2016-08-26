' -------------------------------------------------------------------------
'   Module: modUtilities
'   Author: Patrick Callahan
'   Abstract: Some general purpose utilities.
'
'   Revision        Owner   Changes:
'1  2001/04/17      P.C.    Created.
'2  2003/04/17      P.C.    Updated to .Net
'3  2007/08/13      P.C.    Updated to .Net 2.0
'4  2012/06/06      P.C.    Updated to .Net 4.0 and Windows 7
' -------------------------------------------------------------------------

' -------------------------------------------------------------------------
'  Options
' -------------------------------------------------------------------------
Option Explicit On

Imports System
Imports System.IO
Imports System.Windows.Forms
Imports System.Text.RegularExpressions

' -------------------------------------------------------------------------
'  Imports
' -------------------------------------------------------------------------


Public Module modUtilities

    ' -------------------------------------------------------------------------
    '  Global constants
    ' -------------------------------------------------------------------------
    ' What log file should we use
    Private Const strLOG_FILE_EXTENSION As String = ".Log"

    ' -------------------------------------------------------------------------
    '  Global variables
    ' -------------------------------------------------------------------------
    Private m_strOldLogFilePath As String           ' Name of the last log file opened
    Private m_fswLogFile As StreamWriter = Nothing  ' File handle of the last log file opened


    ' -------------------------------------------------------------------------
    ' Name: SetBusyCursor
    ' Abstract: Enable/Disable the form and set the cursor to normal or busy.
    ' -------------------------------------------------------------------------
    Public Sub SetBusyCursor(ByRef frmForm As Form, ByVal blnBusy As Boolean)

        Try

            ' Busy?
            If blnBusy = True Then

                ' Yes
                frmForm.Cursor = Cursors.WaitCursor
                frmForm.Enabled = False

            Else

                ' No
                frmForm.Cursor = Cursors.Default
                frmForm.Enabled = True

            End If

        Catch excError As Exception

            ' Display error message
            WriteLog(excError)

        End Try

    End Sub


    ' -------------------------------------------------------------------------
    ' Name: GetVersion
    ' Abstract: Get the major, minor revision numbers. Format at M.m.
    ' -------------------------------------------------------------------------
    Public Function GetVersion() As String

        Dim strVersion As String = ""

        Try

            ' Major
            strVersion &= System.Reflection.Assembly.GetExecutingAssembly.GetName().Version.Major.ToString()

            ' Minor
            strVersion &= "." & System.Reflection.Assembly.GetExecutingAssembly.GetName().Version.Minor.ToString()

            ' or strVersion = Application.Version

        Catch excError As Exception

            ' Display error message
            WriteLog(excError)

        End Try

        Return strVersion

    End Function


    ' -------------------------------------------------------------------------
    ' Name: CheckForPreviousApplicationInstance
    ' Abstract: Check to see if a previous instance exists( true )
    ' -------------------------------------------------------------------------
    Public Function CheckForPreviousApplicationInstance() As Boolean

        Dim blnPreviousInstance As Boolean = False

        Try

            Dim strCurrentProcessName As String = Diagnostics.Process.GetCurrentProcess.ProcessName

            ' Does a previous version exist?
            If UBound(Diagnostics.Process.GetProcessesByName(strCurrentProcessName)) > 0 Then

                ' Yes
                blnPreviousInstance = True

            End If

        Catch excError As Exception

            ' Display error message
            WriteLog(excError)

        End Try

        Return blnPreviousInstance

    End Function



    ' -------------------------------------------------------------------------
    ' Name: Wait
    ' Abstract: Wait a few seconds
    ' -------------------------------------------------------------------------
    Public Sub Wait(ByVal intMilliSeconds As Integer)

        Try

            Dim dtmWaitUntil As Date

            ' Maximum of 10 second wait
            If intMilliSeconds > 10000 Then intMilliSeconds = 10000

            ' Get the current time
            dtmWaitUntil = Now.AddMilliseconds(intMilliSeconds)

            ' Wait 
            Do While Now < dtmWaitUntil

                Application.DoEvents()

            Loop

        Catch excError As Exception

            ' Display error message
            WriteLog(excError)

        End Try

    End Sub


#Region "Error Log"


    ' -------------------------------------------------------------------------
    ' Name: WriteLog
    ' Abstract: Overload withd blnDisplay set to true
    ' -------------------------------------------------------------------------
    Public Sub WriteLog(ByVal excErrorToLog As Exception)

        Try

            WriteLog(excErrorToLog.ToString(), True)

        Catch excError As Exception

            ' Display error message
            MessageBox.Show("Error:" & vbNewLine & excError.ToString(), Application.ProductName, _
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: WriteLog
    ' Abstract: Overload withd blnDisplay set to true
    ' -------------------------------------------------------------------------
    Public Sub WriteLog(ByVal strMessageToLog As String)

        Try

            WriteLog(strMessageToLog, True)

        Catch excError As Exception

            ' Display error message
            MessageBox.Show("Error:" & vbNewLine & excError.ToString(), Application.ProductName, _
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)

        End Try

    End Sub



    ' -------------------------------------------------------------------------
    ' Name: WriteLog
    ' Abstract: Write a message to the error log.
    ' -------------------------------------------------------------------------
    Public Sub WriteLog(ByVal strMessageToLog As String, ByVal blnDisplayWarning As Boolean)

        Try

            Dim fswLogFile As StreamWriter = Nothing

            ' Warn the user?
            If blnDisplayWarning = True Then

                ' Yes( ProductName is set in AssemblyInfo )
                MessageBox.Show(strMessageToLog, Application.ProductName, _
                                MessageBoxButtons.OK, MessageBoxIcon.Warning)

            End If

            ' Append a date/time stamp
            strMessageToLog = (DateTime.Now).ToString("yyyy/MM/dd HH:mm:ss") & " - " & strMessageToLog

            ' Get a free file handle
            fswLogFile = GetLogFile()

            ' Is the file OK?
            If Not fswLogFile Is Nothing Then

                ' Yes, Log it
                fswLogFile.WriteLine(strMessageToLog)

                ' Flush the buffer so we can immediately see results in file.  Very important.
                ' Otherwise we have to wait for flush which might be when application closes
                ' or we get another error.  Waiting for the application to close may not be
                ' a good idea if the application is in a production environment
                fswLogFile.Flush()

            End If

        Catch excError As Exception

            ' Display error message
            MessageBox.Show("Error:" & vbNewLine & excError.ToString(), Application.ProductName, _
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)

        End Try

    End Sub


    ' -------------------------------------------------------------------------
    ' Name: DeleteOldFiles
    ' Abstract: Delete any files older than 10 days.
    ' -------------------------------------------------------------------------
    Private Sub DeleteOldFiles()

        Try

            Dim strLogFilePath As String = ""
            Dim dirLogDirectory As DirectoryInfo = Nothing
            Dim dtmFileCreated As DateTime = Now
            Dim intDaysOld As Integer = 0

            ' Path
            strLogFilePath = Application.StartupPath & "\Log\"

            ' Look for any files
            dirLogDirectory = New DirectoryInfo(strLogFilePath)

            ' Are there any?
            For Each finLogFile As FileInfo In dirLogDirectory.GetFiles("*" & strLOG_FILE_EXTENSION)

                ' When was the file created?
                dtmFileCreated = finLogFile.CreationTime

                ' How old is the file?
                intDaysOld = (dtmFileCreated.Subtract(DateTime.Now)).Days

                ' Is the file older than 10 days?
                If intDaysOld > 10 Then

                    ' Yes.  Delete it.
                    finLogFile.Delete()

                End If

            Next

        Catch excError As Exception

            ' Display error message
            MessageBox.Show("Error:" & vbNewLine & excError.ToString(), Application.ProductName, _
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)

        End Try

    End Sub


    ' -------------------------------------------------------------------------
    ' Name: GetLogFile
    ' Abstract: Open the log file for writing.  Use today's date as part of
    '           the file name.  Each day a new log file will be created.
    '           Makes debug easier.
    ' -------------------------------------------------------------------------
    Private Function GetLogFile() As StreamWriter

        Try
            Dim strToday As String = (DateTime.Now).ToString("yyyyMMdd")
            Dim strLogFilePath As String = ""

            ' Log everything in a log directory off of the current application directory
            strLogFilePath = Application.StartupPath & "\Log\" & strToday & strLOG_FILE_EXTENSION

            ' Is this a new day?
            If m_strOldLogFilePath <> strLogFilePath Then

                ' Save the log file name
                m_strOldLogFilePath = strLogFilePath

                ' Does the log directory exist?
                If Directory.Exists(Application.StartupPath & "\Log") = False Then

                    ' No, so create it
                    Directory.CreateDirectory(Application.StartupPath & "\Log")

                End If

                ' Close old log file( if there is one )
                If Not m_fswLogFile Is Nothing Then m_fswLogFile.Close()

                ' Delete old log files
                DeleteOldFiles()

                ' Does the file exist?
                If File.Exists(strLogFilePath) = False Then

                    ' No, create
                    m_fswLogFile = File.CreateText(strLogFilePath)

                Else

                    ' Yes, append
                    m_fswLogFile = File.AppendText(strLogFilePath)

                End If

            End If

        Catch excError As Exception

            ' Display error message
            MessageBox.Show("Error:" & vbNewLine & excError.ToString(), Application.ProductName, _
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)

        End Try

        ' Return result
        Return m_fswLogFile

    End Function

#End Region

End Module
