Public Class AboutBox
    ' Constructor to set the FormBorderStyle
    Public Sub New()
        InitializeComponent()
        ' Disable resizing by setting the border style
        Me.FormBorderStyle = FormBorderStyle.FixedSingle ' This style does not allow resizing
    End Sub

    Private Sub AboutBox_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set the text for the labels
        LabelAppName.Text = "Wonderware InTouch Project Launcher"
        LabelVersion.Text = "Version 1.0.0"
        LabelAuthor.Text = "Author: Nazmirul"
        LabelDescription.Text = "This application helps in launching Wonderware InTouch multiple 
projects efficiently."
    End Sub

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
        ' Close the About dialog when the button is clicked
        Me.Close()
    End Sub
End Class
