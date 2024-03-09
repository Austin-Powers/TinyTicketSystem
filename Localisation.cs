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
        private ResourceManager _resourceManager = new ResourceManager("TinyTicketSystem.TinyTicketSystem", typeof(Localisation).Assembly);

        public Localisation() { }

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
