using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicketModel;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

        private string _titleEmptyText = "filter_title_empty";

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
            _titleEmptyText = localisation.Get(_titleEmptyText);

            // status
            _statusCB.Text = _allStatus;
            _statusCB.Items.Add(_allStatus);
            _statusCB.Items.Add(_openStatus);
            _statusCB.Items.Add(_blockedStatus);
            _statusCB.Items.Add(_openOrBlockedStatus);
            _statusCB.Items.Add(_closedStatus);

            // title
            _titleTB.Text = _titleEmptyText;
            _titleTB.ForeColor = SystemColors.InactiveCaption;

            // tag
            _tagCB.Items.Add("");
            foreach (var tag in _model.Tags)
            {
                _tagCB.Items.Add(tag);
            }
        }

        private string TitleText
        {
            get
            {
                return _titleTB.ForeColor == SystemColors.InactiveCaption ? "" : _titleTB.Text;
            }

            set
            {
                if (value == "")
                {
                    _titleTB.ForeColor = SystemColors.InactiveCaption;
                    _titleTB.Text = _titleEmptyText;
                }
                else
                {
                    _titleTB.ForeColor = SystemColors.ControlText;
                    _titleTB.Text = value;
                }
            }
        }

        public void EnterTitle()
        {
            if (_titleTB.ForeColor == SystemColors.InactiveCaption)
            {
                _titleTB.ForeColor = SystemColors.ControlText;
                _titleTB.Text = "";
            }
        }

        public void LeaveTitle()
        {
            TitleText = _titleTB.Text;
        }

        public bool OnUpdate()
        {
            var result = false;
            var currentState = ToTicketState(_statusCB.Text);
            if (_filter.State != currentState)
            {
                _filter.State = currentState;
                result = true;
            }
            if (_filter.Title != TitleText)
            {
                _filter.Title = TitleText;
                result = true;
            }
            if (_filter.Tag != _tagCB.Text)
            {
                _filter.Tag = _tagCB.Text;
                result = true;
            }
            return result;
        }

        private Filter.TicketState ToTicketState(string text)
        {
            if (text == _openStatus)
            {
                return Filter.TicketState.Open;
            }
            if (text == _openOrBlockedStatus)
            {
                return Filter.TicketState.OpenOrBlocked;
            }
            if (text == _blockedStatus)
            {
                return Filter.TicketState.Blocked;
            }
            if (text == _closedStatus)
            {
                return Filter.TicketState.Closed;
            }
            return Filter.TicketState.All;
        }

        public List<Ticket> Apply()
        {
            return _filter.Apply(_model);
        }
    }
}
