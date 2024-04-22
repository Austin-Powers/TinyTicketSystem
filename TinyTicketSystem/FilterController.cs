using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicketModel;

namespace TinyTicketSystem
{
    /// <summary>
    /// Manages the components for setting the filter.
    /// </summary>
    public class FilterController
    {
        private Model _model = null;

        private ToolStripComboBox _statusCB = null;

        private ToolStripTextBox _titleTB = null;

        private ToolStripComboBox _tagCB = null;

        private string _allStatus = "filter_status_all";

        private string _openStatus = "filter_status_open";

        private string _blockedStatus = "filter_status_blocked";

        private string _openOrBlockedStatus = "filter_status_open_or_blocked";

        private string _closedStatus = "filter_status_closed";

        private readonly Filter _filter = new Filter();

        public FilterController(
            Model model,
            Localisation localisation,
            ToolStripComboBox statusCB,
            ToolStripTextBox titleTB,
            ToolStripComboBox tagCB)
        {
            _model = model;
            _statusCB = statusCB;
            _titleTB = titleTB;
            _tagCB = tagCB;

            // localisation
            _allStatus = localisation.Get(_allStatus);
            _openStatus = localisation.Get(_openStatus);
            _openOrBlockedStatus = localisation.Get(_openOrBlockedStatus);
            _blockedStatus = localisation.Get(_blockedStatus);
            _closedStatus = localisation.Get(_closedStatus);

            // status
            _statusCB.Text = _allStatus;
            _statusCB.Items.Add(_allStatus);
            _statusCB.Items.Add(_openStatus);
            _statusCB.Items.Add(_blockedStatus);
            _statusCB.Items.Add(_openOrBlockedStatus);
            _statusCB.Items.Add(_closedStatus);

            // title

            // tag
        }
    }
}
