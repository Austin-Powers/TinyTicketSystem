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
            var result = new List<Ticket>();
            foreach (var id in model.TicketIds)
            {
                var ticket = model.GetTicket(id);
                result.Add(ticket);
            }
            switch (State)
            {
                case TicketState.Open:
                    result = FilterOpen(model, result);
                    break;
                case TicketState.Blocked:
                    result = FilterBlocked(model, result);
                    break;
                case TicketState.OpenOrBlocked:
                    result = FilterOpenOrBlocked(result);
                    break;
                case TicketState.Closed:
                    result = FilterClosed(result);
                    break;
            }
            if (Title != null && Title.Length != 0)
            {
                result = FilterTitle(result);
            }
            if (Tag != null && Tag.Length != 0)
            {
                result = FilterTag(result);
            }
            return result;
        }

        private List<Ticket> FilterOpen(Model model, List<Ticket> tickets)
        {
            var result = new List<Ticket>();
            foreach (var ticket in tickets)
            {
                if (!ticket.Closed && !model.IsBlocked(ticket))
                {
                    result.Add(ticket);
                }
            }
            return result;
        }

        private List<Ticket> FilterBlocked(Model model, List<Ticket> tickets)
        {
            var result = new List<Ticket>();
            foreach (var ticket in tickets)
            {
                if (model.IsBlocked(ticket))
                {
                    result.Add(ticket);
                }
            }
            return result;
        }

        private List<Ticket> FilterOpenOrBlocked(List<Ticket> tickets)
        {
            var result = new List<Ticket>();
            foreach (var ticket in tickets)
            {
                if (!ticket.Closed)
                {
                    result.Add(ticket);
                }
            }
            return result;
        }

        private List<Ticket> FilterClosed(List<Ticket> tickets)
        {
            var result = new List<Ticket>();
            foreach (var ticket in tickets)
            {
                if (ticket.Closed)
                {
                    result.Add(ticket);
                }
            }
            return result;
        }

        private List<Ticket> FilterTitle(List<Ticket> tickets)
        {
            var title = Title.ToLower();
            var result = new List<Ticket>();
            foreach (var ticket in tickets)
            {
                if (ticket.Title.ToLower().Contains(title))
                {
                    result.Add(ticket);
                }
            }
            return result;
        }

        private List<Ticket> FilterTag(List<Ticket> tickets)
        {
            var result = new List<Ticket>();
            foreach (var ticket in tickets)
            {
                if (ticket.Tags.Contains(Tag))
                {
                    result.Add(ticket);
                }
            }
            return result;
        }
    }
}
