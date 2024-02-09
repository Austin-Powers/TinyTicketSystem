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
			CreateModel();
        }

		private void newTicketToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if(_model != null)
			{
				_model.AddEmptyTicket();
				UpdateTable();
            }
        }

        private void setTicketDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
			var dialog = new FolderBrowserDialog();
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				Properties.Settings.Default.TicketDirectory = dialog.SelectedPath;
				Properties.Settings.Default.Save();
				CreateModel();
            }
        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			var ticketNumber = dataGridView.Rows[e.RowIndex].Cells[0].Value;
            toolStripStatusLabel.Text = "Editing ticket " + ticketNumber;
            var ticketView = new TicketWindow(_model, Convert.ToUInt32(ticketNumber));
			ticketView.ShowDialog(this);
            toolStripStatusLabel.Text = "Ticket " + ticketNumber + " saved";
        }

        private void CreateModel()
		{
			try
			{
                _model = new Model(Properties.Settings.Default.TicketDirectory);
				UpdateTable();
                newTicketToolStripMenuItem.Enabled = true;
				toolStripStatusLabel.Text = "Loaded tickets from " + Properties.Settings.Default.TicketDirectory;
            }
            catch (Exception)
			{
				_model = null;
				dataGridView.Rows.Clear();
                newTicketToolStripMenuItem.Enabled = false;
				toolStripStatusLabel.Text = "Loading tickets from \"" + Properties.Settings.Default.TicketDirectory + "\" failed";
            }
        }

		private void UpdateTable()
		{
			dataGridView.Rows.Clear();
			foreach(var ticket in _model.TicketList)
			{
				var row = new DataGridViewRow();
				var cell = new DataGridViewTextBoxCell
				{
					Value = ticket.ID
				};
				row.Cells.Add(cell);
				dataGridView.Rows.Add(row);
			}
		}
    }
}
