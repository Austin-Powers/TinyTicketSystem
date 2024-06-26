﻿namespace TinyTicketSystem
{
	partial class MainWindow
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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.idColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.titleColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastChangedColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tagsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newTicketToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteTicketToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setTicketDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusFilterTSCB = new System.Windows.Forms.ToolStripComboBox();
            this.titleFilterTSTB = new System.Windows.Forms.ToolStripTextBox();
            this.tagFilterTSCB = new System.Windows.Forms.ToolStripComboBox();
            this.resetFilterTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idColumn,
            this.titleColumn,
            this.lastChangedColumn,
            this.statusColumn,
            this.tagsColumn});
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.Size = new System.Drawing.Size(800, 401);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellDoubleClick);
            // 
            // idColumn
            // 
            this.idColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.idColumn.HeaderText = "ID";
            this.idColumn.Name = "idColumn";
            this.idColumn.ReadOnly = true;
            this.idColumn.Width = 43;
            // 
            // titleColumn
            // 
            this.titleColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.titleColumn.HeaderText = "Title";
            this.titleColumn.Name = "titleColumn";
            this.titleColumn.ReadOnly = true;
            this.titleColumn.Width = 52;
            // 
            // lastChangedColumn
            // 
            this.lastChangedColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.lastChangedColumn.HeaderText = "Last Changed";
            this.lastChangedColumn.Name = "lastChangedColumn";
            this.lastChangedColumn.ReadOnly = true;
            this.lastChangedColumn.Width = 98;
            // 
            // statusColumn
            // 
            this.statusColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.statusColumn.HeaderText = "Status";
            this.statusColumn.Name = "statusColumn";
            this.statusColumn.ReadOnly = true;
            this.statusColumn.Width = 62;
            // 
            // tagsColumn
            // 
            this.tagsColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.tagsColumn.HeaderText = "Tags";
            this.tagsColumn.Name = "tagsColumn";
            this.tagsColumn.ReadOnly = true;
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.statusFilterTSCB,
            this.titleFilterTSTB,
            this.tagFilterTSCB,
            this.resetFilterTSMI});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(800, 27);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newTicketToolStripMenuItem,
            this.deleteTicketToolStripMenuItem,
            this.setTicketDirectoryToolStripMenuItem,
            this.refreshToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 23);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newTicketToolStripMenuItem
            // 
            this.newTicketToolStripMenuItem.Name = "newTicketToolStripMenuItem";
            this.newTicketToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.newTicketToolStripMenuItem.Text = "New Ticket";
            this.newTicketToolStripMenuItem.Click += new System.EventHandler(this.NewTicketToolStripMenuItem_Click);
            // 
            // deleteTicketToolStripMenuItem
            // 
            this.deleteTicketToolStripMenuItem.Name = "deleteTicketToolStripMenuItem";
            this.deleteTicketToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.deleteTicketToolStripMenuItem.Text = "Delete Ticket";
            this.deleteTicketToolStripMenuItem.Click += new System.EventHandler(this.DeleteTicketToolStripMenuItem_Click);
            // 
            // setTicketDirectoryToolStripMenuItem
            // 
            this.setTicketDirectoryToolStripMenuItem.Name = "setTicketDirectoryToolStripMenuItem";
            this.setTicketDirectoryToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.setTicketDirectoryToolStripMenuItem.Text = "Set Ticket Directory";
            this.setTicketDirectoryToolStripMenuItem.Click += new System.EventHandler(this.SetTicketDirectoryToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.RefreshToolStripMenuItem_Click);
            // 
            // statusFilterTSCB
            // 
            this.statusFilterTSCB.Name = "statusFilterTSCB";
            this.statusFilterTSCB.Size = new System.Drawing.Size(121, 23);
            // 
            // titleFilterTSTB
            // 
            this.titleFilterTSTB.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.titleFilterTSTB.Name = "titleFilterTSTB";
            this.titleFilterTSTB.Size = new System.Drawing.Size(150, 23);
            // 
            // tagFilterTSCB
            // 
            this.tagFilterTSCB.Name = "tagFilterTSCB";
            this.tagFilterTSCB.Size = new System.Drawing.Size(121, 23);
            // 
            // resetFilterTSMI
            // 
            this.resetFilterTSMI.Name = "resetFilterTSMI";
            this.resetFilterTSMI.Size = new System.Drawing.Size(76, 23);
            this.resetFilterTSMI.Text = "Reset Filter";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 428);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(800, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel.Text = "Ready";
            // 
            // panel
            // 
            this.panel.Controls.Add(this.dataGridView);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 27);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(800, 401);
            this.panel.TabIndex = 3;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainWindow";
            this.Text = "Tiny Ticket System";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridView;
		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newTicketToolStripMenuItem;
		private System.Windows.Forms.DataGridViewTextBoxColumn idColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn titleColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn lastChangedColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn statusColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn tagsColumn;
        private System.Windows.Forms.ToolStripMenuItem setTicketDirectoryToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
		private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deleteTicketToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox statusFilterTSCB;
        private System.Windows.Forms.ToolStripTextBox titleFilterTSTB;
        private System.Windows.Forms.ToolStripComboBox tagFilterTSCB;
        private System.Windows.Forms.ToolStripMenuItem resetFilterTSMI;
        private System.Windows.Forms.Panel panel;
    }
}

