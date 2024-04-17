using System;
using System.Collections.Generic;

namespace TicketModel
{
    /// <summary>
    /// Stores a set of constraints which are used to create a list
    /// of all tickets from a given model fullfilling these constraints.
    /// </summary>
    public class Filter
    {
        /// <summary>
        /// Enumerates all possible states of a ticket.
        /// </summary>
        public enum TicketState
        {
            All,
            Open,
            Blocked,
            OpenOrBlocked,
            Closed
        }

        /// <summary>
        /// Initializes a new filter.
        /// </summary>
        public Filter() { }

        /// <summary>
        /// The ticket state to filter for.
        /// </summary>
        public TicketState State { get; set; }

        /// <summary>
        /// The title string to filter for.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The tag to filter for.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Applies the set contraints to filter tickets from the given model.
        /// </summary>
        /// <param name="model">The model to extract the tickets from.</param>
        /// <returns></returns>
        public List<Ticket> Apply(Model model)
        {
            var title = Title?.ToLower();
            var result = new List<Ticket>();
            foreach (var id in model.TicketIds)
            {
                var ticket = model.GetTicket(id);
                switch (State)
                {
                    case TicketState.Open:
                        if (ticket.Closed || model.IsBlocked(ticket))
                        {
                            continue;
                        }
                        break;
                    case TicketState.Blocked:
                        if (!model.IsBlocked(ticket))
                        {
                            continue;
                        }
                        break;
                    case TicketState.OpenOrBlocked:
                        if (ticket.Closed)
                        {
                            continue;
                        }
                        break;
                    case TicketState.Closed:
                        if (!ticket.Closed)
                        {
                            continue;
                        }
                        break;
                }
                if (!ticket.Title.ToLower().Contains(title ?? ""))
                {
                    continue;
                }
                if (Tag != null && Tag.Length != 0)
                {
                    if (!ticket.Tags.Contains(Tag))
                    {
                        continue;
                    }
                }
                result.Add(ticket);
            }
            return result;
        }
    }
}
