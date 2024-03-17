﻿using System;
using System.Collections.Generic;
using System.IO;
using TicketModel;

namespace TinyTicketSystem
{
	/// <summary>
	/// Contains all data typically associated with a ticket.
	/// </summary>
	public class Ticket : IComparable<Ticket>
	{
		#region static members
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

        private static readonly string TitleString = "# ";

		private static readonly string OpenString = " - `open`";

		private static readonly string ClosedString = " - `closed`";

		private static readonly string BlockByString = "__Blocked by__ ";

		private static readonly string DetailsString = "## Details";
        #endregion

        #region attributes
        /// <summary>
        /// The identifier to tell tickets apart.
        /// </summary>
        public uint ID { get; }

		private readonly string _path;

        private DateTime _lastChanged;

        /// <summary>
        /// The timestamp of the last change to the ticket.
        /// </summary>
        public DateTime LastChanged { get { return _lastChanged; } }

		private bool _closed = false;

		private bool _savedClosed = false;

        /// <summary>
        /// Flag signalling if the ticket is closed.
        /// </summary>
        public bool Closed
		{
			get
			{
				return _closed;
			}
			set
			{
				_closed = value;
			}
		}

        private string _title = "";

		private string _savedTitle = null;

		/// <summary>
		/// The title of the ticket.
		/// </summary>
		public string Title
		{ 
			get
			{
				return _title;
			}
			set
			{
				var newTitle = value ?? "";
				if (_savedTitle == null)
				{
                    _savedTitle = _title;
                }
                _title = newTitle;
			}
        }

        private string _details = "";

        private string _savedDetails = null;

        /// <summary>
        /// The details of the ticket.
        /// </summary>
        public string Details
		{
			get
			{
				return _details;
			}
			set
			{
				var newDetails = value ?? "";
				if (_savedDetails == null)
				{
					_savedDetails = _details;
				}
				_details = newDetails;
			}
		}

        private readonly List<string> _tags = new List<string>();

		/// <summary>
		/// Returns the list of all tags of this ticket, tags can be used to group tickets.
		/// </summary>
		public List<string> Tags
		{
			get
			{
				return _tags;
			}
			set
			{
				_tags.Clear();
				_tags.AddRange(value);
			}
		}

		private readonly List<uint> _idsOfTicketsBlockingThisTicket = new List<uint>();

		/// <summary>
		/// Returns the list of identifiers of all tickets blocking this ticket.
		/// </summary>
		public List<uint> IDsOfTicketsBlockingThisTicket
		{
			get
			{
				return _idsOfTicketsBlockingThisTicket;
			}
			set
			{
				_idsOfTicketsBlockingThisTicket.Clear();
				_idsOfTicketsBlockingThisTicket.AddRange(value);
			}
		}
		#endregion

		ITicketObserver _observer = null;

        /// <summary>
        /// Initializes a new ticket object, with the given ID loading it from the ticket directory if the corresponding file exists.
        /// </summary>
        /// <param name="ticketDirectory">The ticket directory currently used by the model.</param>
        /// <param name="id">The id of this ticket.</param>
        /// <param name="observer">Optional: An observer getting notified about updates on this ticket.</param>
        public Ticket(string ticketDirectory, uint id, ITicketObserver observer = null)
		{
			ID = id;
			_observer = observer;
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
					ProcessLineEmpty(sr.ReadLine(), 3U);
					ProcessBlockedByLine(sr.ReadLine());
					ProcessLineEmpty(sr.ReadLine(), 5U);
                    ProcessDetailsHeaderLine(sr.ReadLine());
					_details = "";
					while (sr.Peek() != -1)
					{
						ProcessDetailsLine(sr.ReadLine());
					}
					_details = _details.Trim();
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
				Closed = false;
            }
        }

        #region process lines
        private void ProcessTitleLine(string line)
		{
			// # Title - `status`
			if (line == null)
			{
				throw new FormatException("Line 1 missing: " + _path);
			}
			if (!line.StartsWith(TitleString))
			{
				throw new FormatException("First line does not start with \"" + TitleString + "\": " + _path);
			}
			if (line.EndsWith(OpenString))
			{
				Closed = false;
				_title = line.Substring(2, line.Length - TitleString.Length - OpenString.Length);
			}
			else if (line.EndsWith(ClosedString))
			{
                Closed = true;
				_title = line.Substring(2, line.Length - TitleString.Length - ClosedString.Length);
            }
            else
			{
				throw new FormatException("First line does end on a known status: " + _path);
            }

        }

		private void ProcessTagLine(string line)
		{
			// `tag0` `tag1` `tag2` ...
			if (line == null)
			{
				throw new FormatException("Line 2 missing: " + _path);
			}
			foreach (var part in line.Split('`'))
			{
				var trimed = part.Trim();
				if (trimed.Length != 0)
				{
                    _tags.Add(part.Trim());
                }
            }
        }
        private void ProcessLineEmpty(string line, uint number)
        {
			// 
			if (line == null)
			{
				throw new FormatException("Line " + number + " missing: " + _path);
			}
			if (line.Trim().Length != 0)
			{
                throw new FormatException("Expected line " + number + " to be empty: " + _path);
            }
        }

        private void ProcessBlockedByLine(string line)
        {
			// __Blocked by__ [0](../100/0.md) ...
			if (line == null)
			{
				throw new FormatException("Line 4 missing: " + _path);
			}
			if (!line.StartsWith(BlockByString))
			{
                throw new FormatException("Fourth line does not start with \"" + BlockByString + "\" : " + _path);
            }
			var number = false;
			foreach (var part in line.Split('[', ']'))
			{
				if (number)
				{
					_idsOfTicketsBlockingThisTicket.Add(uint.Parse(part));
				}
				number = !number;
			}
        }

        private void ProcessDetailsHeaderLine(string line)
        {
			// ## Details
			if (line == null)
			{
				throw new FormatException("Line 6 missing: " + _path);
			}
			if (!line.Equals(DetailsString))
            {
                throw new FormatException("Expected line six to be \"" + DetailsString + "\": " + _path);
            }
        }

        private void ProcessDetailsLine(string line)
        {
			// ...
			if (line != null)
			{
				_details += line + "\r\n";
			}
		}
        #endregion

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
        /// Updates the file behind the ticket and notifies any observer about the changes.
        /// </summary>
        public void Update()
		{
			if (!Empty())
			{
				_observer?.OnTicketUpdated(this);
				_lastChanged = DateTime.Now;
				FileStream fs = null;
				StreamWriter sw = null;
				try
				{
					Directory.CreateDirectory(Path.GetDirectoryName(_path));
					fs = new FileStream(_path, FileMode.Create);
					sw = new StreamWriter(fs);
					sw.WriteLine(TitleString + _title + (Closed ? ClosedString : OpenString));
					sw.WriteLine(CreateTagsString());
					sw.WriteLine();
					sw.WriteLine(CreateBlockedByString());
					sw.WriteLine();
					sw.WriteLine(DetailsString);
					sw.WriteLine(Details.Trim());
				}
				catch (Exception ex)
				{
					throw ex;
				}
				finally
				{
					sw?.Close();
					fs?.Close();
				}
			}
        }

        private string CreateTagsString()
        {
            string result = "";
            foreach (var tag in _tags)
            {
                result += "`" + tag + "` ";
            }
            return result.Trim();
        }

        private string CreateBlockedByString()
        {
            string result = BlockByString;
            foreach (var id in _idsOfTicketsBlockingThisTicket)
            {
                result += "[" + id + "](" + CreateFilePath("..", id) + ") ";
            }
            return result;
        }

        /// <summary>
        /// Removes the file connected to this ticket.
        /// </summary>
        public void RemoveFile()
		{
			File.Delete(_path);
		}

        public int CompareTo(Ticket other)
		{
			if (other == null)
			{
				return 1;
			}
			return ID.CompareTo(other.ID);
		}

		public override string ToString()
		{
			return ID.ToString() + " - " + Title;
		}
	}
}
