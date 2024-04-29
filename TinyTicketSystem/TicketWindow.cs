using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TicketModel;

namespace TinyTicketSystem
{
	public partial class TicketWindow : Form
	{
		private readonly Model _model;
		private readonly Ticket _ticket;

        private readonly Localisation _localisation;
        private readonly Localisation.TicketLocalisation _myLocalisation;

		private readonly TextBoxHelper _titleText;
		private readonly TextBoxHelper _detailsText;

        public TicketWindow(Model model, uint ticketID, Localisation localisation)
		{
			// WinForms setup
			InitializeComponent();
			_localisation = localisation;
			_myLocalisation = _localisation.TicketLoc;
            blockingTicketsListBox.ContextMenuStrip = blockingIdsCMS;
            tagsListBox.ContextMenuStrip = tagsCMS;
			newTagComboBox.Text = _myLocalisation.TagEmpty;
			newTagComboBox.ForeColor = SystemColors.InactiveCaption;
			foreach (var tag in model.Tags)
			{
				newTagComboBox.Items.Add(tag);
			}
			newTicketToolStripMenuItem.Text = _myLocalisation.NewTicket;
			addTicketToolStripMenuItem.Text = _myLocalisation.AddTicket;
			removeTagTSMI.Text = _myLocalisation.Remove;
			removeTicketTSMI.Text = _myLocalisation.Remove;

			removeTagTSMI.ShortcutKeys = Keys.Delete;
            removeTicketTSMI.ShortcutKeys = Keys.Delete;

			// Load Ticket
			_model = model;
			_ticket = _model.GetTicket(ticketID);
			_titleText = new TextBoxHelper(titleTextBox, _myLocalisation.TitleEmpty, _ticket.Title);
			_titleText.TextChanged += new EventHandler(TitleUpdated);
			TitleUpdated(this, EventArgs.Empty);
			_detailsText = new TextBoxHelper(detailsTextBox, _myLocalisation.DetailsEmpty, _ticket.Details);
			BlockingTicketIDs = _ticket.BlockingTicketsIDs;
			TicketTags = _ticket.Tags;
			UpdateStatus();
		}

		private void TicketWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			_ticket.Title = _titleText.Text;
			_ticket.Details = _detailsText.Text;
			_ticket.BlockingTicketsIDs = BlockingTicketIDs;
			_ticket.Tags = TicketTags;
			_ticket.CommitChanges();
		}

		#region Status
		private void CloseReopenButton_Click(object sender, EventArgs e)
        {
			_ticket.Closed = !_ticket.Closed;
			UpdateStatus();
        }

		private void UpdateStatus()
		{
            if (_model.IsBlocked(_ticket))
            {
				_ticket.Closed = false;
				closeReopenButton.Text = _myLocalisation.StatusBlocked;
                closeReopenButton.Enabled = false;
            }
			else
			{
                closeReopenButton.Enabled = true;
				if (_ticket.Closed)
				{
                    closeReopenButton.Text = _myLocalisation.StatusReopen;
                }
				else
				{
                    closeReopenButton.Text = _myLocalisation.StatusClose;
                }
            }
		}
		#endregion

		#region Title
		private void TitleUpdated(object sender, EventArgs e)
		{
			var title = _titleText.Text;
			Text = (title.Length == 0) ? _myLocalisation.NewTicket : title;
		}
		#endregion

		#region Blocking Tickets
		private HashSet<uint> BlockingTicketIDs
		{
			get
			{
				var set = new HashSet<uint>();
				foreach (var ticket in blockingTicketsListBox.Items)
				{
					set.Add(Convert.ToUInt32(ticket.ToString().Split(' ')[0]));
				}
				return set;
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
			var ticket = _model.GetTicket(id);
			if (ticket != null)
			{
				blockingTicketsListBox.Items.Add(ticket.ToString());
            }
        }

		private void NewTicketToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var id = _model.AddEmptyTicket();
			var ticket = _model.GetTicket(id);
			ticket.Tags = TicketTags;
            var ticketView = new TicketWindow(_model, id, _localisation);
			ticketView.ShowDialog(this);
			if (!_model.RemoveTicketIfEmpty(id))
			{
				AddBlockingTicket(id);
			}
			UpdateStatus();
		}

		private void AddTicketToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var selector = new TicketSelectorWindow(_model, _ticket.ID, BlockingTicketIDs, _localisation);
			selector.ShowDialog(this);
			foreach (var ticket in selector.SelectedTickets)
			{
				blockingTicketsListBox.Items.Add(ticket);
			}
			UpdateStatus();
        }

        private void RemoveTicketToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var toRemove = blockingTicketsListBox.SelectedItem;
			if (toRemove != null)
			{
				blockingTicketsListBox.Items.Remove(toRemove);
			}
			UpdateStatus();
        }

        private void BlockingTicketsListBox_DoubleClick(object sender, EventArgs e)
		{
			var selected = blockingTicketsListBox.SelectedItem;
			if (selected != null)
			{
				var id = Convert.ToUInt32(selected.ToString().Split(' ')[0]);
				var ticketView = new TicketWindow(_model, id, _localisation);
				ticketView.ShowDialog(this);
			}
			UpdateStatus();
        }
        #endregion

        #region Tags
        private HashSet<string> TicketTags
        {
			get
			{
				var list = new HashSet<string>();
				foreach (var tag in tagsListBox.Items)
				{
					list.Add(tag.ToString());
				}
				return list;
			}

			set
			{
				tagsListBox.Items.Clear();
				foreach (var tag in value)
				{
					AddTagToList(tag);
				}
			}
		}
        private void RemoveTagTSMI_Click(object sender, EventArgs e)
		{
			var toRemove = tagsListBox.SelectedItem;
			if (toRemove != null)
			{
				tagsListBox.Items.Remove(toRemove);
			}
        }

        private void newTagComboBox_Enter(object sender, EventArgs e)
        {
            newTagComboBox.Text = "";
            newTagComboBox.ForeColor = SystemColors.ControlText;
        }

        private void newTagComboBox_Leave(object sender, EventArgs e)
        {
            newTagComboBox.Text = _myLocalisation.TagEmpty;
            newTagComboBox.ForeColor = SystemColors.InactiveCaption;
        }
        private void newTagComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
			AddTagToList(newTagComboBox.Text);
        }

        private void newTagComboBox_KeyUp(object sender, KeyEventArgs e)
        {
			if (e.KeyCode == Keys.Return)
			{
                AddTagToList(newTagComboBox.Text.Trim());
				newTagComboBox.Text = "";
            }
        }

        /// <summary>
        /// Adds the given tag to the list, if it is not already included.
        /// </summary>
        /// <param name="tag">The tag to add.</param>
        private void AddTagToList(string tag)
		{
			if (!tagsListBox.Items.Contains(tag))
			{
				tagsListBox.Items.Add(tag);
			}
		}
        #endregion
    }
}
