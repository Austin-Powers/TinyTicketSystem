using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TinyTicketSystem
{
	public partial class TicketWindow : Form
	{
		private static readonly string TitleEmptyString = "Enter the ticket title here";

		private static readonly string DetailsEmptyString = "Enter the details of the ticket here";

		private Model _model;

		private Ticket _ticket;

		public TicketWindow(Model model, UInt32 ticketID)
		{
			// WinForms setup
			InitializeComponent();
			blockingTicketsListBox.ContextMenuStrip = blockingIdsCMS;
            tagsListBox.ContextMenuStrip = tagsCMS;

			// Load Ticket
			_model = model;
			_ticket = _model.GetTicket(ticketID);
			TitleText = _ticket.Title;
			DetailsText = _ticket.Details;
			UpdateStatus();
		}

		private void TicketWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			bool edited = false;
			if (_ticket.Title != TitleText)
			{
				_ticket.Title = TitleText;
				edited = true;
			}
			if (_ticket.Details != DetailsText)
			{
				_ticket.Details = DetailsText;
				edited = true;
			}
			if (edited)
			{
				_ticket.Save();
			}
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
		private string TitleText
		{
			get
			{
				return titleTextBox.ForeColor == SystemColors.InactiveCaption ? "" : titleTextBox.Text;
			}

			set
			{
				if (value == "")
				{
					titleTextBox.ForeColor = SystemColors.InactiveCaption;
					titleTextBox.Text = TitleEmptyString;
				}
				else
				{
					titleTextBox.ForeColor = SystemColors.ControlText;
					titleTextBox.Text = value;
				}
			}
		}

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
			TitleText = titleTextBox.Text;
		}
		#endregion

		#region Details
		private string DetailsText
		{
			get
			{
				return detailsTextBox.ForeColor == SystemColors.InactiveCaption ? "" : detailsTextBox.Text;
			}

			set
			{
				if (value == "")
				{
					detailsTextBox.ForeColor = SystemColors.InactiveCaption;
					detailsTextBox.Text = DetailsEmptyString;
				}
				else
				{
					detailsTextBox.ForeColor = SystemColors.ControlText;
					detailsTextBox.Text = value;
				}
			}
		}

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
			DetailsText = detailsTextBox.Text;
		}
		#endregion

		#region Blocking Tickets
		private void newTicketToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var id = _model.AddEmptyTicket();
			var ticketView = new TicketWindow(_model, id);
			ticketView.ShowDialog(this);
			_ticket.IDsOfTicketsBlockingThisTicket.Add(id);
		}
		#endregion
	}
}
