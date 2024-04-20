using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicketModel;

namespace TinyTicketSystem
{
	public partial class MainWindow : Form
	{
		private Model _model = null;

		private readonly Localisation _localisation = new Localisation();

		public MainWindow()
		{
			InitializeComponent();
			fileToolStripMenuItem.Text = _localisation.Get("main_file");
			newTicketToolStripMenuItem.Text = _localisation.Get("main_file_new_ticket");
			deleteTicketToolStripMenuItem.Text = _localisation.Get("main_file_delete_ticket");
			setTicketDirectoryToolStripMenuItem.Text = _localisation.Get("main_file_set_ticket_dir");
			refreshToolStripMenuItem.Text = _localisation.Get("main_file_refresh");
			dataGridView.Columns[0].HeaderCell.Value = _localisation.Get("main_table_id");
			dataGridView.Columns[1].HeaderCell.Value = _localisation.Get("main_table_title");
			dataGridView.Columns[2].HeaderCell.Value = _localisation.Get("main_table_last_changed");
			dataGridView.Columns[3].HeaderCell.Value = _localisation.Get("main_table_status");
			dataGridView.Columns[4].HeaderCell.Value = _localisation.Get("main_table_tags");

            newTicketToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
			deleteTicketToolStripMenuItem.ShortcutKeys = Keys.Delete;
			refreshToolStripMenuItem.ShortcutKeys = Keys.F5;
			CreateModel();
        }

		#region Toolstrip
		private void NewTicketToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if(_model != null)
				{
					var id = _model.AddEmptyTicket();
					var ticketView = new TicketWindow(_model, id, _localisation);
					ticketView.ShowDialog(this);
					if (_model.RemoveTicketIfEmpty(id))
					{
						DisplayInfo(_localisation.Get("main_new_discarded"));
					}
					else
					{
						DisplayInfo(_localisation.Get("main_new_created"));
					}
					UpdateTable();
				}
			}
			catch (Exception ex)
			{
				DisplayError(_localisation.Get("main_new_error", ex.Message));
            }
		}

		private void DeleteTicketToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				var selectedTicketIndex = dataGridView.SelectedCells[0].RowIndex;
				var selectedTicketNumber = dataGridView.Rows[selectedTicketIndex].Cells[0].Value;

				var result = MessageBox.Show(
					_localisation.Get("main_delete_confirm",
					selectedTicketNumber), _localisation.Get("main_delete_question"),
					MessageBoxButtons.YesNo);
				if (result == DialogResult.Yes)
				{
					_model.RemoveTicket(Convert.ToUInt32(selectedTicketNumber));
					UpdateTable();
				}
			}
			catch (Exception ex)
			{
				DisplayError(_localisation.Get("main_delete_error", ex.Message));
			}
		}

		private void SetTicketDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
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
                DisplayError(_localisation.Get("main_dir_error", ex.Message));
            }
		}

		private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CreateModel();
		}

        private void statusFilterTSCB_TextUpdate(object sender, EventArgs e)
        {
			DisplayInfo(statusFilterTSCB.Text);
        }
        #endregion

        private void DataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				var ticketNumber = dataGridView.Rows[e.RowIndex].Cells[0].Value;
				DisplayInfo(_localisation.Get("main_edit_open", ticketNumber));
				var ticketView = new TicketWindow(_model, Convert.ToUInt32(ticketNumber), _localisation);
				ticketView.ShowDialog(this);
				DisplayInfo(_localisation.Get("main_edit_close", ticketNumber));
				UpdateTable();
			}
        }

        private void CreateModel()
		{
			try
			{
                _model = new Model(Properties.Settings.Default.TicketDirectory);
				UpdateTable();
                newTicketToolStripMenuItem.Enabled = true;
				DisplayInfo(_localisation.Get("main_load_success", Properties.Settings.Default.TicketDirectory));
            }
            catch (Exception ex)
			{
				_model = null;
				dataGridView.Rows.Clear();
                newTicketToolStripMenuItem.Enabled = false;
				DisplayError(_localisation.Get("main_load_failure", Properties.Settings.Default.TicketDirectory, ex.Message));
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
                row.Cells.Add(CreateStatusCellFor(ticket));
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

		private DataGridViewTextBoxCell CreateStatusCellFor(Ticket ticket)
		{
			var statusCell = new DataGridViewTextBoxCell();
            if (_model.IsBlocked(ticket))
			{
                statusCell.Style.ForeColor = Color.Red;
                statusCell.Value = _localisation.Get("main_table_blocked");
            }
            else if (ticket.Closed)
			{
                statusCell.Style.ForeColor = Color.Green;
                statusCell.Value = _localisation.Get("main_table_closed");
            }
			else
			{
                statusCell.Style.ForeColor = Color.Blue;
                statusCell.Value = _localisation.Get("main_table_open");
            }
			return statusCell;
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
