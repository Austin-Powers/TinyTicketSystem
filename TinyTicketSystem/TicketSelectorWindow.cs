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

        private HashSet<uint> _alreadyBlockingTickets = null;

        private FilterController _filterController = null;

        public TicketSelectorWindow(Model model, HashSet<uint> alreadyBlockingTickets, Localisation localisation)
        {
			InitializeComponent();
            _alreadyBlockingTickets = alreadyBlockingTickets;
            _filterController = new FilterController(model, localisation, statusFilterTSCB, titleFilterTSTB, tagFilterTSCB);
            Text = localisation.Selector.Title;
			addButton.Text = localisation.Selector.Add;
            UpdateList();
        }

        #region Filter
        private void titleFilterTSTB_Enter(object sender, EventArgs e)
        {
            if (_filterController != null)
            {
                _filterController.EnterTitle();
            }
        }

        private void titleFilterTSTB_Leave(object sender, EventArgs e)
        {
            if (_filterController != null)
            {
                _filterController.LeaveTitle();
            }
        }

        private void statusFilterTSCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFilter();
        }

        private void titleFilterTSTB_TextChanged(object sender, EventArgs e)
        {
            UpdateFilter();
        }

        private void tagFilterTSCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFilter();
        }

        private void UpdateFilter()
        {
            if ((_filterController != null) && (_filterController.OnUpdate()))
            {
                UpdateList();
            }
        }
        #endregion

        private void UpdateList()
        {
            checkedListBox.Items.Clear();
            foreach (var ticket in _filterController.Apply())
            {
                if (!_alreadyBlockingTickets.Contains(ticket.ID))
                {
                    checkedListBox.Items.Add(ticket.ToString());
                }
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
