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
	public partial class TicketSelectorWindow : Form
	{
		private readonly List<string> selectedTickets = new List<string>();

		public List<string> SelectedTickets { get { return selectedTickets; } }

		public TicketSelectorWindow(Model model, HashSet<uint> alreadyBlockingTickets, Localisation localisation)
		{
			InitializeComponent();
			Text = localisation.Get("selector_title");
			addButton.Text = localisation.Get("selector_add");
			foreach (var ticketId in model.TicketIds)
			{
				if (!alreadyBlockingTickets.Contains(ticketId))
				{
					checkedListBox.Items.Add(model.GetTicket(ticketId).ToString());
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
