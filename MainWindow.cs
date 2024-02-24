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
	public partial class MainWindow : Form
	{
		private Model _model = null;
		
		public MainWindow()
		{
			InitializeComponent();
            newTicketToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
			deleteTicketToolStripMenuItem.ShortcutKeys = Keys.Delete;
			refreshToolStripMenuItem.ShortcutKeys = Keys.F5;
			CreateModel();
        }

		#region Toolstrip
		private void newTicketToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if(_model != null)
				{
					var id = _model.AddEmptyTicket();
					UpdateTable();
					var ticketView = new TicketWindow(_model, id);
					ticketView.ShowDialog(this);
					DisplayInfo("Created new Ticket");
				}
			}
			catch (Exception ex)
			{
				DisplayError("Error during creation of new Ticket: " + ex.Message);
            }
		}

		private void deleteTicketToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				var selectedTicketIndex = dataGridView.SelectedCells[0].RowIndex;
				var selectedTicketNumber = dataGridView.Rows[selectedTicketIndex].Cells[0].Value;

				var result = MessageBox.Show("Delete Ticket " + selectedTicketNumber + "?", "Are you sure?", MessageBoxButtons.YesNo);
				if (result == DialogResult.Yes)
				{
					_model.RemoveTicket(Convert.ToUInt32(selectedTicketNumber));
					UpdateTable();
				}
			}
			catch (Exception ex)
			{
				DisplayError("Error while deleting a ticket: " + ex.Message);
			}
		}

		private void setTicketDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
			try
			{
				var dialog = new FolderBrowserDialog();
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					Properties.Settings.Default.TicketDirectory = dialog.SelectedPath;
					Properties.Settings.Default.Save();
					CreateModel();
				}
			}
			catch (Exception ex)
			{
                DisplayError("Error when setting new ticket directory: " + ex.Message);
            }
		}

		private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CreateModel();
		}
		#endregion

		private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			var ticketNumber = dataGridView.Rows[e.RowIndex].Cells[0].Value;
			DisplayInfo("Editing ticket " + ticketNumber);
            var ticketView = new TicketWindow(_model, Convert.ToUInt32(ticketNumber));
			ticketView.ShowDialog(this);
            DisplayInfo("Ticket " + ticketNumber + " saved");
			UpdateTable();
        }

        private void CreateModel()
		{
			try
			{
                _model = new Model(Properties.Settings.Default.TicketDirectory);
				UpdateTable();
                newTicketToolStripMenuItem.Enabled = true;
				DisplayInfo("Loaded tickets from " + Properties.Settings.Default.TicketDirectory);
            }
            catch (Exception ex)
			{
				_model = null;
				dataGridView.Rows.Clear();
                newTicketToolStripMenuItem.Enabled = false;
				DisplayError("Loading tickets from \"" + Properties.Settings.Default.TicketDirectory + "\" failed: " + ex.Message);
            }
        }

		private void UpdateTable()
		{
			dataGridView.Rows.Clear();
			foreach(var id in _model.TicketIds)
			{
				var ticket = _model.GetTicket(id);
				var row = new DataGridViewRow();
				row.Cells.Add(new DataGridViewTextBoxCell
                {
                    Value = ticket.ID
                });
				row.Cells.Add(new DataGridViewTextBoxCell
                {
                    Value = ticket.Title
                });
				row.Cells.Add(new DataGridViewTextBoxCell
				{
					Value = ticket.LastChanged.ToString()
				});
				row.Cells.Add(new DataGridViewTextBoxCell
				{
					Value = ticket.Closed ? "closed" : "open"
				});
				var tagsString = "";
				foreach(var tag in ticket.Tags)
				{
					tagsString += (tag + " ");
				}
				row.Cells.Add(new DataGridViewTextBoxCell
				{
					Value = tagsString
				});
				dataGridView.Rows.Add(row);
			}
		}

		private void DisplayInfo(string info)
		{
            toolStripStatusLabel.Text = info;
            toolStripStatusLabel.ForeColor = Color.Black;
        }

		private void DisplayError(string error)
		{
            toolStripStatusLabel.Text = error;
            toolStripStatusLabel.ForeColor = Color.Red;
        }

		private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
		{
			_model?.SaveIndex();
		}
	}
}
