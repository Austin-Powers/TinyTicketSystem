namespace TinyTicketSystem
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
            this.mainSplit = new System.Windows.Forms.SplitContainer();
            this.additionalInfoSplitContainer = new System.Windows.Forms.SplitContainer();
            this.openCloseButton = new System.Windows.Forms.Button();
            this.detailsTextBox = new System.Windows.Forms.TextBox();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplit)).BeginInit();
            this.mainSplit.Panel1.SuspendLayout();
            this.mainSplit.Panel2.SuspendLayout();
            this.mainSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.additionalInfoSplitContainer)).BeginInit();
            this.additionalInfoSplitContainer.SuspendLayout();
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
            this.mainSplit.Panel1.Controls.Add(this.openCloseButton);
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
            this.additionalInfoSplitContainer.Size = new System.Drawing.Size(261, 535);
            this.additionalInfoSplitContainer.SplitterDistance = 246;
            this.additionalInfoSplitContainer.TabIndex = 1;
            // 
            // openCloseButton
            // 
            this.openCloseButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.openCloseButton.Location = new System.Drawing.Point(0, 0);
            this.openCloseButton.Name = "openCloseButton";
            this.openCloseButton.Size = new System.Drawing.Size(261, 26);
            this.openCloseButton.TabIndex = 0;
            this.openCloseButton.Text = "Close";
            this.openCloseButton.UseVisualStyleBackColor = true;
            this.openCloseButton.Click += new System.EventHandler(this.openCloseButton_Click);
            // 
            // detailsTextBox
            // 
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
            // TicketWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.mainSplit);
            this.Name = "TicketWindow";
            this.Text = "TicketWindow";
            this.mainSplit.Panel1.ResumeLayout(false);
            this.mainSplit.Panel2.ResumeLayout(false);
            this.mainSplit.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplit)).EndInit();
            this.mainSplit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.additionalInfoSplitContainer)).EndInit();
            this.additionalInfoSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer mainSplit;
		private System.Windows.Forms.TextBox detailsTextBox;
		private System.Windows.Forms.TextBox titleTextBox;
		private System.Windows.Forms.Button openCloseButton;
		private System.Windows.Forms.SplitContainer additionalInfoSplitContainer;
	}
}