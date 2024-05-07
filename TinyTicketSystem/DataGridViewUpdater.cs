using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicketModel;

namespace TinyTicketSystem
{
    /// <summary>
    /// Simplifies updating a DataGridView.
    /// </summary>
    public class DataGridViewUpdater
    {
        private readonly DataGridView _dataGridView;
        private readonly Localisation _localisation;
        private readonly Dictionary<uint, DataGridViewRow> _rows = new Dictionary<uint, DataGridViewRow>();

        public DataGridViewUpdater(DataGridView dataGridView, Localisation localisation)
        {
            _dataGridView = dataGridView;
            _localisation = localisation;
        }

        public void Update(Model model, List<Ticket> ticketList)
        {
            RemoveNoLongerIncludedTickets(ticketList);
            AddOrUpdateTickets(model, ticketList);
        }

        private void RemoveNoLongerIncludedTickets(List<Ticket> ticketList)
        {
            var ticketsToRemove = new HashSet<uint>();
            foreach (var key in _rows.Keys)
            {
                ticketsToRemove.Add(key);
            }
            foreach (var ticket in ticketList)
            {
                ticketsToRemove.Remove(ticket.ID);
            }
            foreach (var id in ticketsToRemove)
            {
                _dataGridView.Rows.Remove(_rows[id]);
                _rows.Remove(id);
            }
        }
        private void AddOrUpdateTickets(Model model, List<Ticket> ticketList)
        {
            foreach (var ticket in ticketList)
            {
                DataGridViewRow row;
                if (_rows.ContainsKey(ticket.ID))
                {
                    row = _rows[ticket.ID];
                }
                else
                {
                    row = CreateNewRow();
                    _rows.Add(ticket.ID, row);
                    _dataGridView.Rows.Add(row);
                    row.Cells[0].Value = ticket.ID;
                }
                row.Cells[1].Value = ticket.Title;
                row.Cells[2].Value = ticket.LastChanged.ToString();
                if (model.IsBlocked(ticket))
                {
                    row.Cells[3].Value = _localisation.Main.TableBlocked;
                    row.Cells[3].Style.ForeColor = Color.Red;
                }
                else if (ticket.Closed)
                {
                    row.Cells[3].Value = _localisation.Main.TableClosed;
                    row.Cells[3].Style.ForeColor = Color.Green;
                }
                else
                {
                    row.Cells[3].Value = _localisation.Main.TableOpen;
                    row.Cells[3].Style.ForeColor = Color.Blue;
                }
                var tagsString = "";
                foreach (var tag in ticket.Tags)
                {
                    tagsString += (tag + " ");
                }
                row.Cells[4].Value = tagsString;
            }
        }

        private DataGridViewRow CreateNewRow()
        {
            var row = new DataGridViewRow();
            for (var i = 0; i < 5; ++i)
            {
                row.Cells.Add(new DataGridViewTextBoxCell());
            }
            return row;
        }
    }
}
