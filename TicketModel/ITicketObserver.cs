using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyTicketSystem;

namespace TicketModel
{
    /// <summary>
    /// Interface for classes observing changes on a ticket.
    /// </summary>
    internal interface ITicketObserver
    {
        /// <summary>
        /// Method called if a ticket is updated.
        /// </summary>
        /// <param name="ticket">The ticket that has been updated.</param>
        void OnTicketChanged(Ticket ticket);
    }
}
