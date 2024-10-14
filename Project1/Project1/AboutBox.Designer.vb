<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AboutBox
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AboutBox))
        Me.LabelAppName = New System.Windows.Forms.Label()
        Me.LabelVersion = New System.Windows.Forms.Label()
        Me.LabelAuthor = New System.Windows.Forms.Label()
        Me.LabelDescription = New System.Windows.Forms.Label()
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'LabelAppName
        '
        Me.LabelAppName.AutoSize = True
        Me.LabelAppName.Location = New System.Drawing.Point(34, 18)
        Me.LabelAppName.Name = "LabelAppName"
        Me.LabelAppName.Size = New System.Drawing.Size(39, 13)
        Me.LabelAppName.TabIndex = 0
        Me.LabelAppName.Text = "Label1"
        '
        'LabelVersion
        '
        Me.LabelVersion.AutoSize = True
        Me.LabelVersion.Location = New System.Drawing.Point(34, 51)
        Me.LabelVersion.Name = "LabelVersion"
        Me.LabelVersion.Size = New System.Drawing.Size(39, 13)
        Me.LabelVersion.TabIndex = 1
        Me.LabelVersion.Text = "Label1"
        '
        'LabelAuthor
        '
        Me.LabelAuthor.AutoSize = True
        Me.LabelAuthor.Location = New System.Drawing.Point(34, 79)
        Me.LabelAuthor.Name = "LabelAuthor"
        Me.LabelAuthor.Size = New System.Drawing.Size(39, 13)
        Me.LabelAuthor.TabIndex = 2
        Me.LabelAuthor.Text = "Label1"
        '
        'LabelDescription
        '
        Me.LabelDescription.AutoSize = True
        Me.LabelDescription.Location = New System.Drawing.Point(34, 108)
        Me.LabelDescription.Name = "LabelDescription"
        Me.LabelDescription.Size = New System.Drawing.Size(39, 13)
        Me.LabelDescription.TabIndex = 3
        Me.LabelDescription.Text = "Label1"
        '
        'ButtonOK
        '
        Me.ButtonOK.Location = New System.Drawing.Point(188, 147)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(75, 23)
        Me.ButtonOK.TabIndex = 4
        Me.ButtonOK.Text = "OK"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'AboutBox
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(446, 233)
        Me.Controls.Add(Me.ButtonOK)
        Me.Controls.Add(Me.LabelDescription)
        Me.Controls.Add(Me.LabelAuthor)
        Me.Controls.Add(Me.LabelVersion)
        Me.Controls.Add(Me.LabelAppName)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "AboutBox"
        Me.Text = "About"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LabelAppName As Windows.Forms.Label
    Friend WithEvents LabelVersion As Windows.Forms.Label
    Friend WithEvents LabelAuthor As Windows.Forms.Label
    Friend WithEvents LabelDescription As Windows.Forms.Label
    Friend WithEvents ButtonOK As Windows.Forms.Button
End Class
