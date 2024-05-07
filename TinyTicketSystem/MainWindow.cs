using System;
using System.Drawing;
using System.Windows.Forms;
using TicketModel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace TinyTicketSystem
{
	public partial class MainWindow : Form
	{
		private readonly Localisation _localisation = new Localisation();
		private readonly DataGridViewUpdater _dataGridUpdater;
        
		private Model _model = null;

		private FilterController _filterController = null;

		public MainWindow()
		{
			InitializeComponent();
			fileToolStripMenuItem.Text = _localisation.Main.File;
			newTicketToolStripMenuItem.Text = _localisation.Main.FileNewTicket;
			deleteTicketToolStripMenuItem.Text = _localisation.Main.FileDeleteTicket;
			setTicketDirectoryToolStripMenuItem.Text = _localisation.Main.FileSetTicketDir;
			refreshToolStripMenuItem.Text = _localisation.Main.FileRefresh;
			dataGridView.Columns[0].HeaderCell.Value = _localisation.Main.TableID;
			dataGridView.Columns[1].HeaderCell.Value = _localisation.Main.TableTitle;
			dataGridView.Columns[2].HeaderCell.Value = _localisation.Main.TableLastChanged;
			dataGridView.Columns[3].HeaderCell.Value = _localisation.Main.TableStatus;
			dataGridView.Columns[4].HeaderCell.Value = _localisation.Main.TableTags;
			_dataGridUpdater = new DataGridViewUpdater(dataGridView, _localisation);

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
						DisplayInfo(_localisation.Main.NewTicketDiscarded);
					}
					else
					{
						DisplayInfo(_localisation.Main.NewTicketCreated);
					}
					UpdateTable();
				}
			}
			catch (Exception ex)
			{
				DisplayError(_localisation.Main.NewTicketError(ex.Message));
            }
		}

		private void DeleteTicketToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				var selectedTicketIndex = dataGridView.SelectedCells[0].RowIndex;
				var selectedTicketNumber = dataGridView.Rows[selectedTicketIndex].Cells[0].Value;

				var result = MessageBox.Show(
					_localisation.Main.DeleteConfirm(selectedTicketNumber),
					_localisation.Main.DeleteQuestion,
					MessageBoxButtons.YesNo);
				if (result == DialogResult.Yes)
				{
					_model.RemoveTicket(Convert.ToUInt32(selectedTicketNumber));
					UpdateTable();
				}
			}
			catch (Exception ex)
			{
				DisplayError(_localisation.Main.DeleteError(ex.Message));
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
                DisplayError(_localisation.Main.DirError(ex.Message));
            }
		}

		private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CreateModel();
		}

		private void FilterUpdated(object sender, EventArgs e)
		{
            UpdateTable();
        }
        #endregion

        private void DataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				var ticketNumber = dataGridView.Rows[e.RowIndex].Cells[0].Value;
				DisplayInfo(_localisation.Main.EditOpen(ticketNumber));
				var ticketView = new TicketWindow(_model, Convert.ToUInt32(ticketNumber), _localisation);
				ticketView.ShowDialog(this);
				DisplayInfo(_localisation.Main.EditClose(ticketNumber));
				UpdateTable();
			}
        }

        private void CreateModel()
		{
			try
			{
                _model = new Model(Properties.Settings.Default.TicketDirectory);
				_filterController = new FilterController(
					_model,
					_localisation,
					statusFilterTSCB,
					titleFilterTSTB,
					tagFilterTSCB,
					resetFilterTSMI);
				_filterController.Status = Properties.Settings.Default.FilterStatus;
				_filterController.Title = Properties.Settings.Default.FilterTitle;
				_filterController.Tag = Properties.Settings.Default.FilterTag;
				_filterController.FilterUpdated += new EventHandler(FilterUpdated);
                UpdateTable();
                newTicketToolStripMenuItem.Enabled = true;
				DisplayInfo(_localisation.Main.LoadSuccess(Properties.Settings.Default.TicketDirectory));
            }
            catch (Exception ex)
			{
				_model = null;
				dataGridView.Rows.Clear();
                newTicketToolStripMenuItem.Enabled = false;
				DisplayError(_localisation.Main.LoadFailure(Properties.Settings.Default.TicketDirectory, ex.Message));
            }
        }

		private void UpdateTable()
		{
			dataGridView.Rows.Clear();
			foreach(var ticket in _filterController.Apply())
			{
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
                statusCell.Value = _localisation.Main.TableBlocked;
            }
            else if (ticket.Closed)
			{
                statusCell.Style.ForeColor = Color.Green;
                statusCell.Value = _localisation.Main.TableClosed;
            }
			else
			{
                statusCell.Style.ForeColor = Color.Blue;
                statusCell.Value = _localisation.Main.TableOpen;
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
            if (_filterController != null)
			{
                Properties.Settings.Default.FilterStatus = _filterController.Status;
                Properties.Settings.Default.FilterTitle = _filterController.Title;
                Properties.Settings.Default.FilterTag = _filterController.Tag;
                Properties.Settings.Default.Save();
            }
		}
    }
}
