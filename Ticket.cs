using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyTicketSystem
{
	/// <summary>
	/// Contains all data typically associated with a ticket.
	/// </summary>
	public class Ticket : IComparable<Ticket>
	{
		/// <summary>
		/// Creates the filepath for the given parameters.
		/// </summary>
		/// <param name="ticketDirectory">The ticket directory all tickets are stored in.</param>
		/// <param name="id">The id of the ticket.</param>
		/// <returns>The path of the ticket.</returns>
		public static string CreateFilePath(string ticketDirectory, uint id)
		{
            var subDirNumber = 100;
            while (subDirNumber < id)
            {
                subDirNumber += 100;
            }
            return Path.Combine(ticketDirectory, subDirNumber.ToString(), id.ToString() + ".md");
        }

		/// <summary>
		/// The identifier to tell tickets apart.
		/// </summary>
		public uint ID { get; }

		private readonly string _path;

		private string _title;

		/// <summary>
		/// The title of the ticket.
		/// </summary>
		public string Title { get { return _title ?? ""; } set { _title = value; } }

		private DateTime _lastChanged;

		/// <summary>
		/// The timestamp of the last change to the ticket.
		/// </summary>
		public DateTime LastChanged { get { return _lastChanged; } }

		private bool _status;

		/// <summary>
		/// The status of the ticket, Open or Closed.
		/// </summary>
		public string Status { get { return _status ? "open" : "closed"; } set { _status = value.ToLower().Equals("open"); } }

		private readonly List<string> _tags = new List<string>();

		/// <summary>
		/// Returns the list of all tags of this ticket, tags can be used to group tickets.
		/// </summary>
		public List<string> Tags { get { return _tags; } }

		private readonly List<uint> _idsOfTicketsBlockingThisTicket = new List<uint>();

		/// <summary>
		/// Returns the list of identifiers of all tickets blocking this ticket.
		/// </summary>
		public List<uint> IDsOfTicketsBlockingThisTicket { get { return _idsOfTicketsBlockingThisTicket; } }

        private string _details;

        /// <summary>
        /// The details of the ticket.
        /// </summary>
        public string Details { get { return _details ?? ""; } set { _details = value; } }

        /// <summary>
        /// Initializes a new ticket object, with the given ID loading it from the ticket directory if the corresponding file exists.
        /// </summary>
        /// <param name="ticketDirectory">The ticket directory currently used by the model.</param>
        /// <param name="id">The id of this ticket.</param>
        public Ticket(string ticketDirectory, uint id)
		{
			ID = id;
			_path = CreateFilePath(ticketDirectory, id);
			if (File.Exists(_path))
			{
				_lastChanged = File.GetLastWriteTime(_path);
				throw new NotImplementedException();
            }
            else
			{
                _lastChanged = DateTime.Now;
            }
        }

		/// <summary>
		/// Save changes to this ticket to the file, if there are any.
		/// </summary>
		public void SaveChanges()
		{
            throw new NotImplementedException();
        }

        public int CompareTo(Ticket other)
		{
			if (other == null)
			{
				return 1;
			}
			return ID.CompareTo(other.ID);
		}
	}
}
