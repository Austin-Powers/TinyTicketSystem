﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TicketModel
{
	/// <summary>
	/// The model of the tiny ticket system.
	/// </summary>
	public class Model : ITicketObserver
	{
		private static readonly string IndexFileName = "index.md";

		private readonly string _ticketDirectory;

		private uint _nextId = 0U;

		private readonly Dictionary<uint, Ticket> _tickets = new Dictionary<uint, Ticket>();

		public List<uint> TicketIds {  get { return _tickets.Keys.ToList(); } }

		public Ticket GetTicket(uint ticketId)
		{
			if (_tickets.ContainsKey(ticketId))
			{
                return _tickets[ticketId];
            }
			return null;
        }

		private readonly HashSet<string> _tags = new HashSet<string>();

		/// <summary>
		/// A list of all tags known to this model.
		/// </summary>
		public List<string> Tags { get { return _tags.ToList(); } }

		/// <summary>
		/// Initializes a new Model.
		/// </summary>
		/// <param name="TicketDirectory">The directory the tickets are stored in.</param>
		public Model(string TicketDirectory)
		{
			_ticketDirectory = TicketDirectory;
			if (!Directory.Exists(_ticketDirectory))
			{
				throw new Exception("Directory does not exist");
			}

			var indexFilepath = Path.Combine(_ticketDirectory, IndexFileName);
			if(File.Exists(indexFilepath))
			{
				FileStream fs = null;
				StreamReader sr = null;
				try
				{
					fs = new FileStream(indexFilepath, FileMode.Open);
					sr = new StreamReader(fs);

					while (sr.Peek() != -1)
					{
						var line = sr.ReadLine();
						var offset = line.IndexOf('[') + 1;
						var length = line.IndexOf(' ') - offset;
						var idString = line.Substring(offset, length);
						var ticket = AddTicket(uint.Parse(line.Substring(offset, length)));
						RemoveTicketIfEmpty(ticket.ID);
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
		}

        #region Ticket
        /// <summary>
        /// Adds a new empty ticket using the next available id.
        /// </summary>
        /// <returns>The id of the new ticket.</returns>
        /// <remarks>
        /// This method may discover tickets that already exist in the folder,
        /// those tickets will be added to the model.
        /// </remarks>
        public uint AddEmptyTicket()
		{
			var newTicket = AddTicket(NextUnusedID());
			while (!newTicket.Empty())
			{
                newTicket = AddTicket(NextUnusedID());
            }
			return newTicket.ID;
        }

        private Ticket AddTicket(uint id)
        {
            var ticket = new Ticket(_ticketDirectory, id);
			ticket.Observer = this;
            _tickets.Add(id, ticket);
            AddTagsOf(ticket);
            return ticket;
        }

        /// <summary>
        /// Removed the ticket with the given id from the list, if the ticket is empty.
        /// </summary>
        /// <param name="id">The id of the ticket to remove.</param>
        /// <returns>True if the ticket was removed, false otherwise.</returns>
        public bool RemoveTicketIfEmpty(uint id)
		{
			if (_tickets[id].Empty())
			{
				RemoveTicket(id);
				return true;
			}
			return false;
		}

		/// <summary>
		/// Removed the ticket with the given id from the list.
		/// </summary>
		/// <param name="id">The id of the ticket to remove.</param>
		public void RemoveTicket(uint id)
		{
			_tickets[id].RemoveFile();
			_tickets.Remove(id);
			foreach (var key in _tickets.Keys)
			{
				_tickets[key].BlockingTicketsIDs.Remove(id);
			}
		}

		/// <summary>
		/// Checks if the given ticket has any open tickets blocking it.
		/// </summary>
		/// <param name="ticket">The ticket to check.</param>
		/// <returns>True if the ticket is blocked, false otherwise.</returns>
        public bool IsBlocked(Ticket ticket)
        {
			foreach (var blockginId in ticket.BlockingTicketsIDs)
			{
				if (_tickets.ContainsKey(blockginId))
				{
					if (!_tickets[blockginId].Closed)
					{
						return true;
					}
				}
			}
            return false;
        }
        #endregion

        /// <summary>
        /// Adds the tags of the given ticket to the set of known tickets.
        /// </summary>
        /// <param name="ticket">The ticket to extract the tags from.</param>
        public void AddTagsOf(Ticket ticket)
		{
			foreach (var tag in ticket.Tags)
			{
				_tags.Add(tag);
			}
		}

		/// <summary>
		/// Saves the index file.
		/// </summary>
		public void SaveIndex()
		{
			FileStream fs = null;
			StreamWriter sw = null;
			try
			{
				var path = Path.Combine(_ticketDirectory, IndexFileName);
				fs = new FileStream(path, FileMode.Create);
				sw = new StreamWriter(fs);
				foreach (var key in _tickets.Keys)
				{
					var line = "[" + _tickets[key].ID + " " + _tickets[key].Title + "](";
					line += Ticket.CreateFilePath(".", _tickets[key].ID) + ")  ";
					sw.WriteLine(line);
				}
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

        /// <summary>
        /// Method called if a ticket is updated.
        /// </summary>
        /// <param name="ticket">The updated ticket.</param>
        void ITicketObserver.OnTicketUpdated(Ticket ticket)
		{
			if ((ticket != null) && _tickets.ContainsKey(ticket.ID))
			{
				AddTagsOf(ticket);
				if (!ticket.Closed)
				{
                    // reopen tickets this ticket is blocking
                    foreach (var value in _tickets.Values)
					{
						if (value.Closed && value.BlockingTicketsIDs.Contains(ticket.ID))
						{
							value.Closed = false;
							value.Update();
						}
					}
                }
			}
		}

		private uint NextUnusedID()
		{
            if (_tickets.Count == _nextId)
            {
                // average case where the ids are continuous
                ++_nextId;
            }
            else if (_tickets.Count == 0U)
			{
				// edge case if the ticket list is empty
                _nextId = 0U;
			}
            else
			{
				// edge case where deletion caused a hole in the ids
				for (_nextId = 0U; _nextId < _tickets.Count; ++_nextId)
				{
					if (!_tickets.ContainsKey(_nextId))
					{
						break;
					}
				}
            }
            return _nextId;
		}
    }
}
