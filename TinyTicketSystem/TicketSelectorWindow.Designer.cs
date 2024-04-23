namespace TinyTicketSystem
{
	partial class TicketSelectorWindow
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.checkedListBox = new System.Windows.Forms.CheckedListBox();
            this.addButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.statusFilterTSCB = new System.Windows.Forms.ToolStripComboBox();
            this.titleFilterTSTB = new System.Windows.Forms.ToolStripTextBox();
            this.tagFilterTSCB = new System.Windows.Forms.ToolStripComboBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkedListBox
            // 
            this.checkedListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBox.FormattingEnabled = true;
            this.checkedListBox.Location = new System.Drawing.Point(0, 27);
            this.checkedListBox.Name = "checkedListBox";
            this.checkedListBox.Size = new System.Drawing.Size(434, 334);
            this.checkedListBox.TabIndex = 0;
            // 
            // addButton
            // 
            this.addButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.addButton.Location = new System.Drawing.Point(0, 338);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(434, 23);
            this.addButton.TabIndex = 1;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusFilterTSCB,
            this.titleFilterTSTB,
            this.tagFilterTSCB});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(434, 27);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // statusFilterTSCB
            // 
            this.statusFilterTSCB.Name = "statusFilterTSCB";
            this.statusFilterTSCB.Size = new System.Drawing.Size(121, 23);
            this.statusFilterTSCB.SelectedIndexChanged += new System.EventHandler(this.statusFilterTSCB_SelectedIndexChanged);
            // 
            // titleFilterTSTB
            // 
            this.titleFilterTSTB.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.titleFilterTSTB.Name = "titleFilterTSTB";
            this.titleFilterTSTB.Size = new System.Drawing.Size(150, 23);
            this.titleFilterTSTB.Enter += new System.EventHandler(this.titleFilterTSTB_Enter);
            this.titleFilterTSTB.Leave += new System.EventHandler(this.titleFilterTSTB_Leave);
            this.titleFilterTSTB.TextChanged += new System.EventHandler(this.titleFilterTSTB_TextChanged);
            // 
            // tagFilterTSCB
            // 
            this.tagFilterTSCB.Name = "tagFilterTSCB";
            this.tagFilterTSCB.Size = new System.Drawing.Size(121, 23);
            this.tagFilterTSCB.SelectedIndexChanged += new System.EventHandler(this.tagFilterTSCB_SelectedIndexChanged);
            // 
            // TicketSelectorWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 361);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.checkedListBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TicketSelectorWindow";
            this.Text = "Choose tickets to add";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TicketSelectorWindow_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckedListBox checkedListBox;
		private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripComboBox statusFilterTSCB;
        private System.Windows.Forms.ToolStripTextBox titleFilterTSTB;
        private System.Windows.Forms.ToolStripComboBox tagFilterTSCB;
    }
}