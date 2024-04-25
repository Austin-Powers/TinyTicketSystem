using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace TinyTicketSystem
{
    /// <summary>
    /// Simplifies acces to the localisation resources.
    /// </summary>
    public class Localisation
    {
        // All users of this class should be in the same assembly
        private readonly ResourceManager _resourceManager = new ResourceManager("TinyTicketSystem.TinyTicketSystem", typeof(Localisation).Assembly);

        #region FilterLocalisation
        public class FilterLocalisation
        {
            public string StatusAll { get; private set; }
            public string StatusBlocked { get; private set; }
            public string StatusClosed { get; private set; }
            public string StatusOpen { get; private set; }
            public string StatusOpenOrBlocked { get; private set; }
            public string TitleEmpty { get; private set; }

            public FilterLocalisation(ResourceManager rm)
            {
                StatusAll = rm.GetString("filter_status_all");
                StatusBlocked = rm.GetString("filter_status_blocked");
                StatusClosed = rm.GetString("filter_status_closed");
                StatusOpen = rm.GetString("filter_status_open");
                StatusOpenOrBlocked = rm.GetString("filter_status_open_or_blocked");
                TitleEmpty = rm.GetString("filter_title_empty");
            }
        }
        public FilterLocalisation Filter { get; private set; }
        #endregion

        public Localisation()
        {
            var rm = new ResourceManager("TinyTicketSystem.TinyTicketSystem", typeof(Localisation).Assembly);
            Filter = new FilterLocalisation(rm);
        }

        /// <summary>
        /// Gets the localised string for the given name.
        /// </summary>
        /// <param name="name">The name of the string.</param>
        /// <returns>The localised string.</returns>
        public string Get(string name)
        {
            return _resourceManager.GetString(name);
        }

        /// <summary>
        /// Gets the localised string from the given name and insert the given argument.
        /// </summary>
        /// <param name="name">The name of the string.</param>
        /// <param name="arg">The argument to put into the placeholder.</param>
        /// <returns>The localised string with the inserted argument.</returns>
        public string Get(string name, object arg)
        {
            return Get(name).Replace("{1}", arg.ToString());
        }

        /// <summary>
        /// Gets the localised string from the given name and insert the given argument.
        /// </summary>
        /// <param name="name">The name of the string.</param>
        /// <param name="arg0">The argument to put into the first placeholder.</param>
        /// <param name="arg1">The argument to put into the second placeholder.</param>
        /// <returns>The localised string with the inserted argument.</returns>
        public string Get(string name, object arg0, object arg1)
        {
            return Get(name).Replace("{1}", arg0.ToString()).Replace("{2}", arg1.ToString());
        }
    }
}
