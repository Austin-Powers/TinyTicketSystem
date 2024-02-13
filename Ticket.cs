﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

		private static string OpenString = " - `open`";

		private static string ClosedString = " - `closed`";

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
				FileStream fs = null;
				StreamReader sr = null;
				try
				{
					fs = new FileStream(_path, FileMode.Open);
					sr = new StreamReader(fs);
                    ProcessTitleLine(sr.ReadLine());
                    ProcessTagLine(sr.ReadLine());
					ProcessLineEmpty(sr.ReadLine());
					ProcessBlockedByLine(sr.ReadLine());
					ProcessLineEmpty(sr.ReadLine());
                    ProcessDetailsHeaderLine(sr.ReadLine());
					while (sr.Peek() != -1)
					{
						ProcessDetailsLine(sr.ReadLine());
					}
                }
                catch (Exception ex)
				{
					throw ex;
				}
				finally
				{
					sr?.Close();
					fs?.Close();
				}
            }
            else
			{
                _lastChanged = DateTime.Now;
				_status = true;
            }
        }

        private void ProcessTitleLine(string line)
		{
			// # Title - `status`
			if (!line.StartsWith("# "))
			{
				throw new FormatException("First line does not start with \"# \"");
			}
			if (line.EndsWith(OpenString))
			{
				_status = true;
				_title = line.Substring(2, line.Length - 2 - OpenString.Length);
			}
			else if (line.EndsWith(ClosedString))
			{
                _status = false;
				_title = line.Substring(2, line.Length - 2 - ClosedString.Length);
            }
            else
			{
				throw new FormatException("First line does end on a known status");
            }

        }

		private void ProcessTagLine(string line)
		{
            // `tag0` `tag1` `tag2` ...
			var parts = line.Split('`');
			foreach (var part in parts)
			{
				var trimed = part.Trim();
				if (trimed.Length != 0)
				{
                    _tags.Add(part.Trim());
                }
            }
        }
        private void ProcessLineEmpty(string line)
        {
            // 
        }

        private void ProcessBlockedByLine(string line)
        {
            // __Blocked by__ [0](../100/0.md) ...
        }

        private void ProcessDetailsHeaderLine(string line)
        {
            // ## Details
        }

        private void ProcessDetailsLine(string line)
        {
			// ...
        }

        /// <summary>
        /// Checks if the ticket does not contain any information.
        /// </summary>
        /// <returns>True if the ticket is empty, false otherwise.</returns>
        public bool Empty()
		{
			if (Title.Length != 0)
			{
				return false;
			}
			if (Details.Length != 0)
			{
				return false;
			}
			if (IDsOfTicketsBlockingThisTicket.Count != 0)
			{
				return false;
			}
			if (Tags.Count != 0)
			{
				return false;
			}
			return true;
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
