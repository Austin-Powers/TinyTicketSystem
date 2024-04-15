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
        /// Initializes a new filter.
        /// </summary>
        public Filter() { }

        /// <summary>
        /// Applies the set contraints to filter tickets from the given model.
        /// </summary>
        /// <param name="model">The model to extract the tickets from.</param>
        /// <returns></returns>
        public List<Ticket> Apply(Model model)
        {
            var result = new List<Ticket>();
            return result;
        }
    }
}
