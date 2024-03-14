using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyTicketSystem;

namespace TicketModel
{
    /// <summary>
    /// Interface for classes observing updates on a ticket.
    /// </summary>
    public interface ITicketObserver
    {
        /// <summary>
        /// Method called if a ticket is updated.
        /// </summary>
        /// <param name="ticket">The updated ticket.</param>
        void OnTicketUpdated(Ticket ticket);
    }
}
