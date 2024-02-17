using System;
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
		private static readonly string TitleEmptyString = "Enter the ticket title here";

		private static readonly string DetailsEmptyString = "Enter the details of the ticket here";

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
			detailsTextBox.Text = _ticket.Details;
			UpdateStatus();
			UpdateTitle();
			UpdateDetails();
		}

		private void TicketWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			// Copy updated values into ticket, if form is closing before leaving textboxes
			_ticket.Title = titleTextBox.Text;
			_ticket.Details = detailsTextBox.Text;
			_ticket.Save();
		}

		#region Status
		private void closeReopenButton_Click(object sender, EventArgs e)
        {
			_ticket.Closed = !_ticket.Closed;
			UpdateStatus();
        }

		private void UpdateStatus()
		{
			if (_ticket.Closed)
			{
				closeReopenButton.Text = "Reopen";
			}
			else
			{
				closeReopenButton.Text = "Close";
			}
		}
		#endregion

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

		#region Details
		private void detailsTextBox_Enter(object sender, EventArgs e)
		{
			if (detailsTextBox.ForeColor == SystemColors.InactiveCaption)
			{
				detailsTextBox.ForeColor = SystemColors.ControlText;
				detailsTextBox.Text = "";
			}
		}

		private void detailsTextBox_Leave(object sender, EventArgs e)
		{
			_ticket.Details = detailsTextBox.Text;
			UpdateDetails();
		}

		private void UpdateDetails()
		{
			if (_ticket.Details.Length > 0)
			{
				detailsTextBox.ForeColor = SystemColors.ControlText;
			}
			else
			{
				detailsTextBox.ForeColor = SystemColors.InactiveCaption;
				detailsTextBox.Text = DetailsEmptyString;
			}
		}
		#endregion
	}
}
