Imports System.Diagnostics
Imports System.Data.OleDb
Imports System.Windows.Forms
Imports System.Drawing
' Author: Nazmirul
Public Class Form1
    ' Define the path to the Access database
    Private Const DatabasePath As String = "D:\databaseWW.mdb"
    Private WithEvents statusTimer As New Timer() ' Timer for auto-refresh
    Private Const RefreshInterval As Integer = 1000 ' Refresh every 1000 ms (1 second)

    Public Sub New()
        InitializeComponent()
        ' Set form border style to Sizable to ensure Help button visibility
        Me.FormBorderStyle = FormBorderStyle.Sizable
        ' Enable Help button in title bar
        Me.HelpButton = True
        ' Disable the Maximize button
        Me.MaximizeBox = False
        ' Optionally, you can disable the Minimize button as well
        Me.MinimizeBox = False

        ' Configure the timer
        statusTimer.Interval = RefreshInterval
        statusTimer.Start() ' Start the timer
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        ' Load plant names into ComboBox on form load
        LoadPlantNames()

        ' Check if HMI is running when the form loads
        Dim hmiRunning As Boolean = Process.GetProcessesByName("view").Length > 0
        UpdateHMIStatusLabel(hmiRunning)

        ' Update process status on form load
        UpdateProcessStatus()

        ' Subscribe to the Help button click event
        AddHandler Me.HelpButtonClicked, AddressOf TitleBarHelpButtonClicked
    End Sub

    Private Sub TitleBarHelpButtonClicked(sender As Object, e As System.ComponentModel.CancelEventArgs)
        ' Show help documentation
        ShowHelpDocumentation()
    End Sub

    Private Sub ShowHelpDocumentation()
        Dim helpContent As String = "1. Use the ComboBox to select a plant." & Environment.NewLine &
                                     "2. Click 'Launch Project' to start the InTouch application." & Environment.NewLine &
                                     "3. If you need to close running services, use the 'Force Close' button." & Environment.NewLine &
                                     "4. Contact support for further assistance."
        MessageBox.Show(helpContent, "Help Documentation", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub LoadPlantNames()
        ' Connection string to connect to the Access database
        Dim connectionString As String = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={DatabasePath};"
        Using con As New OleDbConnection(connectionString)
            Dim query As String = "SELECT PlantName FROM Projects"
            Dim cmd As New OleDbCommand(query, con)

            Try
                con.Open()
                Dim reader As OleDbDataReader = cmd.ExecuteReader()

                ' Add plant names to the ComboBox
                While reader.Read()
                    ComboBox1.Items.Add(reader("PlantName").ToString())
                End While
            Catch ex As Exception
                MessageBox.Show("Failed to load plant names: " & ex.Message)
            Finally
                con.Close()
            End Try
        End Using
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Check if a plant is selected from the ComboBox
        If ComboBox1.SelectedIndex <> -1 Then
            ' Close running HMI if it exists
            CloseRunningHMI()

            ' Get the selected plant name from the ComboBox
            Dim plantName As String = ComboBox1.SelectedItem.ToString()

            ' Retrieve the folder path for the selected plant name
            Dim folderPath As String = GetFolderPath(plantName)
            If Not String.IsNullOrEmpty(folderPath) Then
                ' Define the path to view.exe
                Dim viewExePath As String = "C:\Program Files (x86)\Wonderware\InTouch\view.exe"

                ' Launch the InTouch application
                Try
                    Process.Start(viewExePath, $"""{folderPath}""")
                    UpdateHMIStatusLabel(True) ' HMI is running
                Catch ex As Exception
                    MessageBox.Show("Failed to launch InTouch application: " & ex.Message)
                    UpdateHMIStatusLabel(False) ' HMI failed to launch
                End Try
            Else
                MessageBox.Show("Folder path not found for the selected plant.")
            End If
        Else
            MessageBox.Show("Please select a plant.")
        End If
    End Sub

    Private Function GetFolderPath(plantName As String) As String
        ' Constructing the folder path
        Return $"D:\Wonderware Database\{plantName}\Project\{plantName}"
    End Function

    ' Close any running HMI (view.exe) before launching another
    Private Sub CloseRunningHMI()
        KillProcess("view")
        KillProcess("wwalmlogger")
        KillProcess("almprt")
    End Sub

    Private Sub KillProcess(processName As String)
        Dim processes = Process.GetProcessesByName(processName)

        If processes.Length > 0 Then
            For Each proc As Process In processes
                Try
                    proc.Kill() ' Kill the process
                    UpdateHMIStatusLabel(False) ' HMI closed
                Catch ex As Exception
                    MessageBox.Show($"Failed to close {processName}: " & ex.Message)
                End Try
            Next
        End If
    End Sub

    ' Update the HMI status label to show whether HMI is running or closed
    Private Sub UpdateHMIStatusLabel(isRunning As Boolean)
        If isRunning Then
            LabelHMIStatus.Text = "HMI Running"
            LabelHMIStatus.BackColor = Color.Green
        Else
            LabelHMIStatus.Text = "HMI Closed"
            LabelHMIStatus.BackColor = Color.Red
        End If
    End Sub

    ' Button2 click event to kill all services with confirmation
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Confirmation dialog
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to kill the running processes?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            ' Close all specified processes
            CloseRunningHMI()
            ' Update process status after killing
            UpdateProcessStatus()
        End If
    End Sub

    ' Update the status of view.exe, wwalmlogger, and almprt.exe
    Private Sub UpdateProcessStatus()
        ' Update the status of view.exe
        Dim viewRunning As Boolean = Process.GetProcessesByName("view").Length > 0
        LabelViewStatus.Text = If(viewRunning, "Running", "Not Running")
        LabelViewStatus.BackColor = If(viewRunning, Color.Green, Color.Red)

        ' Update the status of wwalmlogger
        Dim wwalmloggerRunning As Boolean = Process.GetProcessesByName("wwalmlogger").Length > 0
        LabelWwalmloggerStatus.Text = If(wwalmloggerRunning, "Running", "Not Running")
        LabelWwalmloggerStatus.BackColor = If(wwalmloggerRunning, Color.Green, Color.Red)

        ' Update the status of almprt.exe
        Dim almprtRunning As Boolean = Process.GetProcessesByName("almprt").Length > 0
        LabelAlmprtStatus.Text = If(almprtRunning, "Running", "Not Running")
        LabelAlmprtStatus.BackColor = If(almprtRunning, Color.Green, Color.Red)
    End Sub

    ' Timer tick event to update process status periodically
    Private Sub statusTimer_Tick(sender As Object, e As EventArgs) Handles statusTimer.Tick
        UpdateProcessStatus() ' Call to update process statuses
    End Sub

    ' Event handlers for labels can remain empty or handle other events if necessary
    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
    End Sub

    Private Sub LabelHMIStatus_Click(sender As Object, e As EventArgs) Handles LabelHMIStatus.Click
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
    End Sub

    Private Sub Form1_Load_1(sender As Object, e As EventArgs)
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles LabelViewStatus.Click
    End Sub

    Private Sub Label3_Click_1(sender As Object, e As EventArgs) Handles Label3.Click
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    End Sub

    Private Sub ButtonAbout_Click(sender As Object, e As EventArgs) Handles ButtonAbout.Click
        Dim aboutForm As New AboutBox()
        aboutForm.ShowDialog() ' Show the About dialog as a modal dialog
    End Sub

End Class

Module Program
    Sub Main()
        ' Launch the main form when the application starts
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        Application.Run(New Form1())
    End Sub
End Module
