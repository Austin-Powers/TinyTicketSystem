using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicketModel;

namespace TinyTicketSystem
{
	public partial class TicketSelectorWindow : Form
	{
		public List<string> SelectedTickets { get { return selectedTickets; } }
        
		private readonly List<string> selectedTickets = new List<string>();

        private readonly uint _ownerID;

        private HashSet<uint> _alreadyBlockingTickets = null;

        private FilterController _filterController = null;

        public TicketSelectorWindow(Model model, uint ownerID, HashSet<uint> alreadyBlockingTickets, Localisation localisation)
        {
			InitializeComponent();
            _ownerID = ownerID;
            _alreadyBlockingTickets = alreadyBlockingTickets;
            _filterController = new FilterController(
                model,
                localisation,
                statusFilterTSCB,
                titleFilterTSTB,
                tagFilterTSCB,
                resetFilterTSMI);
            _filterController.FilterUpdated += new EventHandler(FilterUpdated);
            Text = localisation.Selector.Title;
			addButton.Text = localisation.Selector.Add;
            UpdateList();
        }

        private void FilterUpdated(object sender, EventArgs e)
        {
            UpdateList();
        }

        private void UpdateList()
        {
            checkedListBox.Items.Clear();
            foreach (var ticket in _filterController.Apply())
            {
                if (ticket.ID == _ownerID)
                {
                    continue;
                }
                if (_alreadyBlockingTickets.Contains(ticket.ID))
                {
                    continue;
                }
                checkedListBox.Items.Add(ticket.ToString());
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void TicketSelectorWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			foreach (var item in checkedListBox.CheckedItems)
			{
				selectedTickets.Add(item.ToString());
			}
		}
	}
}
