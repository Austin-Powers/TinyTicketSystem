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

        public TicketWindow(Model model, uint ticketID, Localisation localisation)
		{
			// WinForms setup
			InitializeComponent();
			_localisation = localisation;
			_myLocalisation = _localisation.TicketLoc;
            blockingTicketsListBox.ContextMenuStrip = blockingIdsCMS;
            tagsListBox.ContextMenuStrip = tagsCMS;
			newTagTextBox.Text = _myLocalisation.TagEmpty;
			newTagTextBox.ForeColor = SystemColors.InactiveCaption;
			newTicketToolStripMenuItem.Text = _myLocalisation.NewTicket;
			addTicketToolStripMenuItem.Text = _myLocalisation.AddTicket;
			removeTagTSMI.Text = _myLocalisation.Remove;
			removeTicketTSMI.Text = _myLocalisation.Remove;

			removeTagTSMI.ShortcutKeys = Keys.Delete;
            removeTicketTSMI.ShortcutKeys = Keys.Delete;

			// Load Ticket
			_model = model;
			_ticket = _model.GetTicket(ticketID);

			TitleText = _ticket.Title;
			DetailsText = _ticket.Details;
			BlockingTicketIDs = _ticket.BlockingTicketsIDs;
			TicketTags = _ticket.Tags;
			UpdateStatus();
		}

		private void TicketWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			_ticket.Title = TitleText;
			_ticket.Details = DetailsText;
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
					titleTextBox.Text = _myLocalisation.TitleEmpty;
					Text = _myLocalisation.NewTicket;
				}
				else
				{
					titleTextBox.ForeColor = SystemColors.ControlText;
					titleTextBox.Text = value;
					Text = value;
				}
			}
		}

		private void TitleTextBox_Enter(object sender, EventArgs e)
		{
			if (titleTextBox.ForeColor == SystemColors.InactiveCaption)
			{
				titleTextBox.ForeColor = SystemColors.ControlText;
				titleTextBox.Text = "";
			}
		}

		private void TitleTextBox_Leave(object sender, EventArgs e)
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
					detailsTextBox.Text = _myLocalisation.DetailsEmpty;
				}
				else
				{
					detailsTextBox.ForeColor = SystemColors.ControlText;
					detailsTextBox.Text = value;
				}
			}
		}

		private void DetailsTextBox_Enter(object sender, EventArgs e)
		{
			if (detailsTextBox.ForeColor == SystemColors.InactiveCaption)
			{
				detailsTextBox.ForeColor = SystemColors.ControlText;
				detailsTextBox.Text = "";
			}
		}

		private void DetailsTextBox_Leave(object sender, EventArgs e)
		{
			DetailsText = detailsTextBox.Text;
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
			var selector = new TicketSelectorWindow(_model, BlockingTicketIDs, _localisation);
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
		private void NewTagTextBox_Enter(object sender, EventArgs e)
		{
			if (newTagTextBox.ForeColor == SystemColors.InactiveCaption)
			{
				newTagTextBox.ForeColor = SystemColors.ControlText;
				newTagTextBox.Text = "";
			}
		}

		private void NewTagTextBox_Leave(object sender, EventArgs e)
		{
			if (newTagTextBox.Text == "")
			{
				newTagTextBox.ForeColor = SystemColors.InactiveCaption;
				newTagTextBox.Text = _localisation.Get("ticket_tag_empty");
			}
		}

		private void NewTagTextBox_TextChanged(object sender, EventArgs e)
		{
        }

        private void RemoveTagTSMI_Click(object sender, EventArgs e)
		{
			var toRemove = tagsListBox.SelectedItem;
			if (toRemove != null)
			{
				tagsListBox.Items.Remove(toRemove);
			}
        }

		private int _downButtons = 0;

        private void NewTagTextBox_KeyDown(object sender, KeyEventArgs e)
        {
			_downButtons++;
        }

        private void NewTagTextBox_KeyUp(object sender, KeyEventArgs e)
        {
			_downButtons--;
			if (_downButtons == 0)
			{
				var text = newTagTextBox.Text;
				switch (e.KeyCode)
				{
					case Keys.Return:
				        AddTagToList(text.Trim().Replace("\r", "").Replace("\n", ""));
				        newTagTextBox.Text = "";
				        break;
					case Keys.Tab:
						newTagTextBox.Text = TagAutoComplete(text.Trim());
						newTagTextBox.SelectionStart = newTagTextBox.Text.Length;
				        break;
					case Keys.Delete:
					case Keys.Back:
					case Keys.Up: 
					case Keys.Down:
					case Keys.Left:
					case Keys.Right:
				        break;
				    default:
						var fill = TagAutoComplete(text);
				        newTagTextBox.Text = fill;
				        newTagTextBox.SelectionStart = text.Length;
				        newTagTextBox.SelectionLength = fill.Length - text.Length;
				        break;
				}
			}
        }

        private string TagAutoComplete(string text)
		{
            // auto completion, as the built in one does not work as expected
            if (text.Length > 0)
            {
                foreach (var tag in _model.Tags)
                {
                    if (tag.StartsWith(text))
                    {
                        // skip already included ones
                        if (tagsListBox.Items.Contains(tag))
                        {
                            continue;
                        }
						return tag;
                    }
                }
            }
			return text;
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
