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
        private readonly ToolStripMenuItem _resetMI;
        private readonly Localisation.FilterLocalisation _localisation;
        private readonly TextBoxHelper _titleText;
        private readonly Filter _filter = new Filter();

        public FilterController(
            Model model,
            Localisation localisation,
            ToolStripComboBox statusCB,
            ToolStripTextBox titleTB,
            ToolStripComboBox tagCB,
            ToolStripMenuItem resetMI)
        {
            _model = model;
            _statusCB = statusCB;
            _titleTB = titleTB;
            _tagCB = tagCB;
            _resetMI = resetMI;
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
            _titleText = new TextBoxHelper(_titleTB, _localisation.TitleEmpty);
            _titleText.TextChanged += new EventHandler(FilterInputChanged);

            // tag
            _tagCB.Items.Add("");
            foreach (var tag in _model.Tags)
            {
                _tagCB.Items.Add(tag);
            }
            _tagCB.SelectedIndexChanged += new EventHandler(FilterInputChanged);

            // reset
            _resetMI.Text = _localisation.Reset;
            _resetMI.Click += new EventHandler(ResetClicked);
        }
        #region FilterAttributes
        public string Status
        {
            get
            {
                return _filter.Status.ToString();
            }
            set
            {
                if (Enum.TryParse(value, true, out Filter.TicketStatus status))
                {
                    _filter.Status = status;
                }
            }
        }
        public string Title { get { return _filter.Title; } set { _filter.Title = value; } }
        public string Tag { get { return _filter.Tag; } set { _filter.Tag = value; } }
        #endregion

        public void FilterInputChanged(object sender, EventArgs e)
        {
            var result = false;
            var currentStatus = ToTicketStatus(_statusCB.Text);
            if (_filter.Status != currentStatus)
            {
                _filter.Status = currentStatus;
                result = true;
            }
            if (_filter.Title != _titleText.Text)
            {
                _filter.Title = _titleText.Text;
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

        private void ResetClicked(object sender, EventArgs e)
        {
            // Set the filter before updating UI so FilterInputChaned does not call FilterUpdated 3 times
            _filter.Status = Filter.TicketStatus.All;
            _filter.Title = "";
            _filter.Tag = "";
            _statusCB.Text = _localisation.StatusAll;
            _titleText.Text = "";
            _tagCB.Text = "";
            // As we suppressed calling FilterUpdated by FilterInputChanged we need to call it ourselfes once
            FilterUpdated?.Invoke(this, EventArgs.Empty);
        }

        private Filter.TicketStatus ToTicketStatus(string text)
        {
            if (text == _localisation.StatusOpen)
            {
                return Filter.TicketStatus.Open;
            }
            if (text == _localisation.StatusOpenOrBlocked)
            {
                return Filter.TicketStatus.OpenOrBlocked;
            }
            if (text == _localisation.StatusBlocked)
            {
                return Filter.TicketStatus.Blocked;
            }
            if (text == _localisation.StatusClosed)
            {
                return Filter.TicketStatus.Closed;
            }
            return Filter.TicketStatus.All;
        }

        public List<Ticket> Apply()
        {
            return _filter.Apply(_model);
        }
    }
}
