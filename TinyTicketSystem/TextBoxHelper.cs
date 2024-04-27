using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TinyTicketSystem
{
    /// <summary>
    /// Wraps a TextBox or ToolStripTextBox so it displays a gray default text when empty and unselected.
    /// </summary>
    public class TextBoxHelper
    {
        /// <summary>
        /// As TextBox and ToolStripTextBox have no public common interface we need an adapter.
        /// </summary>
        private class TextBoxAdapter
        {
            private TextBox _textBox;
            private ToolStripTextBox _toolStripTextBox;

            public TextBoxAdapter(TextBox textBox)
            {
                _textBox = textBox;
            }
            public TextBoxAdapter(ToolStripTextBox toolStripTextBox)
            {
                _toolStripTextBox = toolStripTextBox;
            }

            public Color ForeColor
            {
                get
                {
                    if (_textBox == null)
                    {
                        return _toolStripTextBox.ForeColor;
                    }
                    else
                    {
                        return _textBox.ForeColor;
                    }
                }
                set
                {
                    if (_textBox == null)
                    {
                        _toolStripTextBox.ForeColor = value;
                    }
                    else
                    {
                        _textBox.ForeColor = value;
                    }
                }
            }
            public string Text
            {
                get
                {
                    if (_textBox == null)
                    {
                        return _toolStripTextBox.Text;
                    }
                    else
                    {
                        return _textBox.Text;
                    }
                }
                set
                {
                    if (_textBox == null)
                    {
                        _toolStripTextBox.Text = value;
                    }
                    else
                    {
                        _textBox.Text = value;
                    }
                }
            }
        }

        public event EventHandler TextChanged;

        private readonly string _defaultText;
        private readonly TextBoxAdapter _textBox;

        public TextBoxHelper(TextBox textBox, string defaultText)
        {
            textBox.Enter += new EventHandler(EnterTextBox);
            textBox.Leave += new EventHandler(LeaveTextBox);
            textBox.TextChanged += new EventHandler(TextBoxContentChanged);
            _textBox = new TextBoxAdapter(textBox);
            _defaultText = defaultText;
        }

        public TextBoxHelper(ToolStripTextBox textBox, string defaultText)
        {
            textBox.Enter += new EventHandler(EnterTextBox);
            textBox.Leave += new EventHandler(LeaveTextBox);
            textBox.TextChanged += new EventHandler(TextBoxContentChanged);
            _textBox = new TextBoxAdapter(textBox);
            _defaultText = defaultText;
        }

        private void EnterTextBox(object sender, EventArgs e)
        {
            if (_textBox.ForeColor == SystemColors.InactiveCaption)
            {
                _textBox.ForeColor = SystemColors.ControlText;
                _textBox.Text = "";
            }
        }
        private void LeaveTextBox(object sender, EventArgs e)
        {
            if (_textBox.Text.Length == 0)
            {
                _textBox.ForeColor = SystemColors.InactiveCaption;
                _textBox.Text = _defaultText;
            }
        }
        private void TextBoxContentChanged(object sender, EventArgs e)
        {
            TextChanged?.Invoke(sender, e);
        }

        public string Text
        {
            get
            {
                return _textBox.ForeColor == SystemColors.InactiveCaption ? "" : _textBox.Text;
            }

            set
            {
                if ((value ?? "") == "")
                {
                    _textBox.ForeColor = SystemColors.InactiveCaption;
                    _textBox.Text = _defaultText;
                }
                else
                {
                    _textBox.ForeColor = SystemColors.ControlText;
                    _textBox.Text = value;
                }
            }
        }
    }
}
