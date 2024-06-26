﻿namespace TinyTicketSystem
{
	partial class TicketWindow
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
            this.components = new System.ComponentModel.Container();
            this.mainSplit = new System.Windows.Forms.SplitContainer();
            this.additionalInfoSplitContainer = new System.Windows.Forms.SplitContainer();
            this.blockingTicketsListBox = new System.Windows.Forms.ListBox();
            this.newTagComboBox = new System.Windows.Forms.ComboBox();
            this.tagsListBox = new System.Windows.Forms.ListBox();
            this.closeReopenButton = new System.Windows.Forms.Button();
            this.detailsTextBox = new System.Windows.Forms.TextBox();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.blockingIdsCMS = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newTicketToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addTicketToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeTicketTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.tagsCMS = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeTagTSMI = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplit)).BeginInit();
            this.mainSplit.Panel1.SuspendLayout();
            this.mainSplit.Panel2.SuspendLayout();
            this.mainSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.additionalInfoSplitContainer)).BeginInit();
            this.additionalInfoSplitContainer.Panel1.SuspendLayout();
            this.additionalInfoSplitContainer.Panel2.SuspendLayout();
            this.additionalInfoSplitContainer.SuspendLayout();
            this.blockingIdsCMS.SuspendLayout();
            this.tagsCMS.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainSplit
            // 
            this.mainSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplit.Location = new System.Drawing.Point(0, 0);
            this.mainSplit.Name = "mainSplit";
            // 
            // mainSplit.Panel1
            // 
            this.mainSplit.Panel1.Controls.Add(this.additionalInfoSplitContainer);
            this.mainSplit.Panel1.Controls.Add(this.closeReopenButton);
            // 
            // mainSplit.Panel2
            // 
            this.mainSplit.Panel2.Controls.Add(this.detailsTextBox);
            this.mainSplit.Panel2.Controls.Add(this.titleTextBox);
            this.mainSplit.Size = new System.Drawing.Size(784, 561);
            this.mainSplit.SplitterDistance = 261;
            this.mainSplit.TabIndex = 0;
            // 
            // additionalInfoSplitContainer
            // 
            this.additionalInfoSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.additionalInfoSplitContainer.Location = new System.Drawing.Point(0, 26);
            this.additionalInfoSplitContainer.Name = "additionalInfoSplitContainer";
            this.additionalInfoSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // additionalInfoSplitContainer.Panel1
            // 
            this.additionalInfoSplitContainer.Panel1.Controls.Add(this.blockingTicketsListBox);
            // 
            // additionalInfoSplitContainer.Panel2
            // 
            this.additionalInfoSplitContainer.Panel2.Controls.Add(this.newTagComboBox);
            this.additionalInfoSplitContainer.Panel2.Controls.Add(this.tagsListBox);
            this.additionalInfoSplitContainer.Size = new System.Drawing.Size(261, 535);
            this.additionalInfoSplitContainer.SplitterDistance = 246;
            this.additionalInfoSplitContainer.TabIndex = 1;
            // 
            // blockingTicketsListBox
            // 
            this.blockingTicketsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.blockingTicketsListBox.FormattingEnabled = true;
            this.blockingTicketsListBox.Location = new System.Drawing.Point(0, 0);
            this.blockingTicketsListBox.Name = "blockingTicketsListBox";
            this.blockingTicketsListBox.Size = new System.Drawing.Size(261, 246);
            this.blockingTicketsListBox.TabIndex = 0;
            this.blockingTicketsListBox.DoubleClick += new System.EventHandler(this.BlockingTicketsListBox_DoubleClick);
            // 
            // newTagComboBox
            // 
            this.newTagComboBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.newTagComboBox.FormattingEnabled = true;
            this.newTagComboBox.Location = new System.Drawing.Point(0, 264);
            this.newTagComboBox.MaxDropDownItems = 16;
            this.newTagComboBox.Name = "newTagComboBox";
            this.newTagComboBox.Size = new System.Drawing.Size(261, 21);
            this.newTagComboBox.TabIndex = 2;
            this.newTagComboBox.SelectedIndexChanged += new System.EventHandler(this.newTagComboBox_SelectedIndexChanged);
            this.newTagComboBox.Enter += new System.EventHandler(this.newTagComboBox_Enter);
            this.newTagComboBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.newTagComboBox_KeyUp);
            this.newTagComboBox.Leave += new System.EventHandler(this.newTagComboBox_Leave);
            // 
            // tagsListBox
            // 
            this.tagsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tagsListBox.FormattingEnabled = true;
            this.tagsListBox.Location = new System.Drawing.Point(0, 0);
            this.tagsListBox.Name = "tagsListBox";
            this.tagsListBox.Size = new System.Drawing.Size(261, 285);
            this.tagsListBox.TabIndex = 1;
            // 
            // closeReopenButton
            // 
            this.closeReopenButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.closeReopenButton.Location = new System.Drawing.Point(0, 0);
            this.closeReopenButton.Name = "closeReopenButton";
            this.closeReopenButton.Size = new System.Drawing.Size(261, 26);
            this.closeReopenButton.TabIndex = 0;
            this.closeReopenButton.Text = "Close";
            this.closeReopenButton.UseVisualStyleBackColor = true;
            this.closeReopenButton.Click += new System.EventHandler(this.CloseReopenButton_Click);
            // 
            // detailsTextBox
            // 
            this.detailsTextBox.AcceptsReturn = true;
            this.detailsTextBox.AcceptsTab = true;
            this.detailsTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detailsTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.detailsTextBox.Location = new System.Drawing.Point(0, 20);
            this.detailsTextBox.Multiline = true;
            this.detailsTextBox.Name = "detailsTextBox";
            this.detailsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.detailsTextBox.Size = new System.Drawing.Size(519, 541);
            this.detailsTextBox.TabIndex = 1;
            this.detailsTextBox.Text = "Details";
            // 
            // titleTextBox
            // 
            this.titleTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.titleTextBox.Location = new System.Drawing.Point(0, 0);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(519, 20);
            this.titleTextBox.TabIndex = 0;
            this.titleTextBox.Text = "Title";
            // 
            // blockingIdsCMS
            // 
            this.blockingIdsCMS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newTicketToolStripMenuItem,
            this.addTicketToolStripMenuItem,
            this.removeTicketTSMI});
            this.blockingIdsCMS.Name = "contextMenuStrip1";
            this.blockingIdsCMS.Size = new System.Drawing.Size(133, 70);
            // 
            // newTicketToolStripMenuItem
            // 
            this.newTicketToolStripMenuItem.Name = "newTicketToolStripMenuItem";
            this.newTicketToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.newTicketToolStripMenuItem.Text = "New Ticket";
            this.newTicketToolStripMenuItem.Click += new System.EventHandler(this.NewTicketToolStripMenuItem_Click);
            // 
            // addTicketToolStripMenuItem
            // 
            this.addTicketToolStripMenuItem.Name = "addTicketToolStripMenuItem";
            this.addTicketToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.addTicketToolStripMenuItem.Text = "Add Ticket";
            this.addTicketToolStripMenuItem.Click += new System.EventHandler(this.AddTicketToolStripMenuItem_Click);
            // 
            // removeTicketTSMI
            // 
            this.removeTicketTSMI.Name = "removeTicketTSMI";
            this.removeTicketTSMI.Size = new System.Drawing.Size(132, 22);
            this.removeTicketTSMI.Text = "Remove";
            this.removeTicketTSMI.Click += new System.EventHandler(this.RemoveTicketToolStripMenuItem_Click);
            // 
            // tagsCMS
            // 
            this.tagsCMS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeTagTSMI});
            this.tagsCMS.Name = "tagsCMS";
            this.tagsCMS.Size = new System.Drawing.Size(118, 26);
            // 
            // removeTagTSMI
            // 
            this.removeTagTSMI.Name = "removeTagTSMI";
            this.removeTagTSMI.Size = new System.Drawing.Size(117, 22);
            this.removeTagTSMI.Text = "Remove";
            this.removeTagTSMI.Click += new System.EventHandler(this.RemoveTagTSMI_Click);
            // 
            // TicketWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.mainSplit);
            this.Name = "TicketWindow";
            this.Text = "TicketWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TicketWindow_FormClosing);
            this.mainSplit.Panel1.ResumeLayout(false);
            this.mainSplit.Panel2.ResumeLayout(false);
            this.mainSplit.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplit)).EndInit();
            this.mainSplit.ResumeLayout(false);
            this.additionalInfoSplitContainer.Panel1.ResumeLayout(false);
            this.additionalInfoSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.additionalInfoSplitContainer)).EndInit();
            this.additionalInfoSplitContainer.ResumeLayout(false);
            this.blockingIdsCMS.ResumeLayout(false);
            this.tagsCMS.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer mainSplit;
		private System.Windows.Forms.TextBox detailsTextBox;
		private System.Windows.Forms.TextBox titleTextBox;
		private System.Windows.Forms.Button closeReopenButton;
		private System.Windows.Forms.SplitContainer additionalInfoSplitContainer;
        private System.Windows.Forms.ListBox blockingTicketsListBox;
        private System.Windows.Forms.ContextMenuStrip blockingIdsCMS;
        private System.Windows.Forms.ToolStripMenuItem removeTicketTSMI;
        private System.Windows.Forms.ContextMenuStrip tagsCMS;
        private System.Windows.Forms.ToolStripMenuItem removeTagTSMI;
        private System.Windows.Forms.ListBox tagsListBox;
		private System.Windows.Forms.ToolStripMenuItem newTicketToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addTicketToolStripMenuItem;
        private System.Windows.Forms.ComboBox newTagComboBox;
    }
}