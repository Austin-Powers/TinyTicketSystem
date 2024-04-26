using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TicketModel;

namespace TinyTicketSystem
{
    /// <summary>
    /// Manages the components for setting the filter.
    /// </summary>
    public class FilterController
    {
        public event EventHandler FilterUpdated;

        private readonly Model _model;
        private readonly ToolStripComboBox _statusCB;
        private readonly ToolStripTextBox _titleTB;
        private readonly ToolStripComboBox _tagCB;
        private readonly Localisation.FilterLocalisation _localisation;
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
            _localisation = localisation.Filter;

            // status
            _statusCB.Text = _localisation.StatusAll;
            _statusCB.Items.Add(_localisation.StatusAll);
            _statusCB.Items.Add(_localisation.StatusOpen);
            _statusCB.Items.Add(_localisation.StatusBlocked);
            _statusCB.Items.Add(_localisation.StatusOpenOrBlocked);
            _statusCB.Items.Add(_localisation.StatusClosed);
            _statusCB.SelectedIndexChanged += new EventHandler(FilterInputChanged);

            // title
            _titleTB.Text = _localisation.TitleEmpty;
            _titleTB.ForeColor = SystemColors.InactiveCaption;
            _titleTB.TextChanged += new EventHandler(FilterInputChanged);
            _titleTB.Enter += new EventHandler(EnterTitle);
            _titleTB.Leave += new EventHandler(LeaveTitle);

            // tag
            _tagCB.Items.Add("");
            foreach (var tag in _model.Tags)
            {
                _tagCB.Items.Add(tag);
            }
            _tagCB.SelectedIndexChanged += new EventHandler(FilterInputChanged);
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
                    _titleTB.Text = _localisation.TitleEmpty;
                }
                else
                {
                    _titleTB.ForeColor = SystemColors.ControlText;
                    _titleTB.Text = value;
                }
            }
        }

        public void EnterTitle(object sender, EventArgs e)
        {
            if (_titleTB.ForeColor == SystemColors.InactiveCaption)
            {
                _titleTB.ForeColor = SystemColors.ControlText;
                _titleTB.Text = "";
            }
        }

        public void LeaveTitle(object sender, EventArgs e)
        {
            TitleText = _titleTB.Text;
        }

        public void FilterInputChanged(object sender, EventArgs e)
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
            if (result)
            {
                FilterUpdated?.Invoke(this, EventArgs.Empty);
            }
        }

        private Filter.TicketState ToTicketState(string text)
        {
            if (text == _localisation.StatusOpen)
            {
                return Filter.TicketState.Open;
            }
            if (text == _localisation.StatusOpenOrBlocked)
            {
                return Filter.TicketState.OpenOrBlocked;
            }
            if (text == _localisation.StatusBlocked)
            {
                return Filter.TicketState.Blocked;
            }
            if (text == _localisation.StatusClosed)
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
