﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TinyTicketSystem
{
	public partial class TicketWindow : Form
	{
		private static readonly string TitleEmptyString = "Enter the ticket Title here";

		private Ticket _ticket;

		public TicketWindow(Model model, UInt32 ticketID)
		{
			// WinForms setup
			InitializeComponent();
			blockingTicketsListBox.ContextMenuStrip = blockingIdsCMS;
            tagsListBox.ContextMenuStrip = tagsCMS;

			// Load Ticket
			_ticket = model.GetTicket(ticketID);
			titleTextBox.Text = _ticket.Title;
			UpdateTitle();
		}

		private void TicketWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			_ticket.Save();
		}

		private void openCloseButton_Click(object sender, EventArgs e)
        {

        }

		#region Title
		private void titleTextBox_Enter(object sender, EventArgs e)
		{
			if (titleTextBox.ForeColor == SystemColors.InactiveCaption)
			{
				titleTextBox.ForeColor = SystemColors.ControlText;
				titleTextBox.Text = "";
			}
		}

		private void titleTextBox_Leave(object sender, EventArgs e)
		{
			_ticket.Title = titleTextBox.Text;
			UpdateTitle();
		}

		private void UpdateTitle()
		{
			if (_ticket.Title.Length > 0)
			{
				titleTextBox.ForeColor = SystemColors.ControlText;
			}
			else
			{
				titleTextBox.ForeColor = SystemColors.InactiveCaption;
				titleTextBox.Text = TitleEmptyString;
			}
		}
		#endregion
	}
}
