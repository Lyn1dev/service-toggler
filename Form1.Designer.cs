namespace ServiceToggler;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.listViewServices = new System.Windows.Forms.ListView();
        this.lblStatus = new System.Windows.Forms.Label();
        this.btnSave = new System.Windows.Forms.Button();
        // 
        // Set properties for buttons and label
        // 
        int y = 20;
        int x1 = 30;
        int lvWidth = 300, lvHeight = 200, gapY = 40;
        int btnWidth = 100, btnHeight = 30;
        // listViewServices
        this.listViewServices.Location = new System.Drawing.Point(x1, y);
        this.listViewServices.Size = new System.Drawing.Size(lvWidth, lvHeight);
        this.listViewServices.View = System.Windows.Forms.View.Details;
        this.listViewServices.FullRowSelect = true;
        this.listViewServices.MultiSelect = false;
        y += lvHeight + gapY;
        // Status label
        this.lblStatus.Text = "Status: Ready";
        this.lblStatus.Location = new System.Drawing.Point(x1, y);
        this.lblStatus.Size = new System.Drawing.Size(300, 30);
        y += gapY;
        // Save button
        this.btnSave.Text = "Save";
        this.btnSave.Location = new System.Drawing.Point(x1, y);
        this.btnSave.Size = new System.Drawing.Size(btnWidth, btnHeight);
        // Add controls
        this.Controls.Add(this.listViewServices);
        this.Controls.Add(this.lblStatus);
        this.Controls.Add(this.btnSave);
        // Form properties
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(350, y + 60);
        this.Text = "Service Toggler";
        // Event handlers
        this.btnSave.Click += new System.EventHandler(this.OnSaveButtonClick);
    }

    #endregion
}
