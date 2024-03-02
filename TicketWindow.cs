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

		private static readonly string TagEmptyString = "Enter new tag here, Press Enter to submit";

		private Model _model;

		private Ticket _ticket;

		public TicketWindow(Model model, uint ticketID)
		{
			// WinForms setup
			InitializeComponent();
			blockingTicketsListBox.ContextMenuStrip = blockingIdsCMS;
            tagsListBox.ContextMenuStrip = tagsCMS;
			newTagTextBox.Text = TagEmptyString;
			newTagTextBox.ForeColor = SystemColors.InactiveCaption;

			removeTicketTSMI.ShortcutKeys = Keys.Delete;
			removeTagTSMI.ShortcutKeys = Keys.Delete;

			// Load Ticket
			_model = model;
			_ticket = _model.GetTicket(ticketID);
			TitleText = _ticket.Title;
			DetailsText = _ticket.Details;
			BlockingTicketIDs = _ticket.IDsOfTicketsBlockingThisTicket;
			TicketTags = _ticket.Tags;
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
			var blockingIds = BlockingTicketIDs;
			if (!ContentIsSame(_ticket.IDsOfTicketsBlockingThisTicket, blockingIds))
			{
				_ticket.IDsOfTicketsBlockingThisTicket = blockingIds;
				edited = true;
			}
			var tags = TicketTags;
			if (!ContentIsSame(_ticket.Tags, tags))
			{
				_ticket.Tags = tags;
				_model.AddTagsOf(_ticket);
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
					Text = "New Ticket";
				}
				else
				{
					titleTextBox.ForeColor = SystemColors.ControlText;
					titleTextBox.Text = value;
					Text = value;
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
		private List<uint> BlockingTicketIDs
		{
			get
			{
				var list = new List<uint>();
				foreach (var ticket in blockingTicketsListBox.Items)
				{
					list.Add(Convert.ToUInt32(ticket.ToString().Split(' ')[0]));
				}
				list.Sort();
				return list;
			}

			set
			{
				blockingTicketsListBox.Items.Clear();
				foreach (var ticket in value)
				{
					AddBlockingTicket(ticket);
				}
			}
		}

		private void AddBlockingTicket(uint id)
		{
			blockingTicketsListBox.Items.Add(_model.GetTicket(id).ToString());
		}

		private bool ContentIsSame(List<uint> a, List<uint> b)
		{
			if (a.Count != b.Count)
			{
				return false;
			}
			for (var i = 0; i < a.Count; ++i)
			{
				if (a[i] != b[i])
				{
					return false;
				}
			}
			return true;
		}

		private void newTicketToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var id = _model.AddEmptyTicket();
			var ticketView = new TicketWindow(_model, id);
			ticketView.ShowDialog(this);
			if (!_model.RemoveTicketIfEmpty(id))
			{
				AddBlockingTicket(id);
			}
		}

		private void addTicketToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var selector = new TicketSelectorWindow(_model, BlockingTicketIDs);
			selector.ShowDialog(this);
			foreach (var ticket in selector.SelectedTickets)
			{
				blockingTicketsListBox.Items.Add(ticket);
			}
		}

		private void removeTicketToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var toRemove = blockingTicketsListBox.SelectedItem;
			if (toRemove != null)
			{
				blockingTicketsListBox.Items.Remove(toRemove);
			}
		}

		private void blockingTicketsListBox_DoubleClick(object sender, EventArgs e)
		{
			var selected = blockingTicketsListBox.SelectedItem;
			if (selected != null)
			{
				var id = Convert.ToUInt32(selected.ToString().Split(' ')[0]);
				var ticketView = new TicketWindow(_model, id);
				ticketView.ShowDialog(this);
			}
		}
		#endregion

		#region Tags
		private List<string> TicketTags
		{
			get
			{
				var list = new List<string>();
				foreach (var tag in tagsListBox.Items)
				{
					list.Add(tag.ToString());
				}
				list.Sort();
				return list;
			}

			set
			{
				tagsListBox.Items.Clear();
				foreach (var tag in value)
				{
					tagsListBox.Items.Add(tag);
				}
			}
		}

		private bool ContentIsSame(List<string> a, List<string> b)
		{
			if (a.Count != b.Count)
			{
				return false;
			}
			for (var i = 0; i < a.Count; ++i)
			{
				if (a[i] != b[i])
				{
					return false;
				}
			}
			return true;
		}

		private void newTagTextBox_Enter(object sender, EventArgs e)
		{
			if (newTagTextBox.ForeColor == SystemColors.InactiveCaption)
			{
				newTagTextBox.ForeColor = SystemColors.ControlText;
				newTagTextBox.Text = "";
			}
		}

		private void newTagTextBox_Leave(object sender, EventArgs e)
		{
			if (newTagTextBox.Text == "")
			{
				newTagTextBox.ForeColor = SystemColors.InactiveCaption;
				newTagTextBox.Text = TagEmptyString;
			}
		}

		private void newTagTextBox_TextChanged(object sender, EventArgs e)
		{
			var text = newTagTextBox.Text;

			// Enter was hit
            if (text.Contains("\n"))
			{
				tagsListBox.Items.Add(newTagTextBox.Text.Trim().Replace("\r", "").Replace("\n", ""));
				newTagTextBox.Text = "";
			}
        }

        private void removeTagTSMI_Click(object sender, EventArgs e)
		{
			var toRemove = tagsListBox.SelectedItem;
			if (toRemove != null)
			{
				tagsListBox.Items.Remove(toRemove);
			}
		}
		#endregion
	}
}
