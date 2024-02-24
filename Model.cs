﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyTicketSystem
{
	/// <summary>
	/// The model of the tiny ticket system.
	/// </summary>
	public class Model
	{
		private static readonly string IndexFileName = "index.md";

		private readonly string _ticketDirectory;

		private uint _nextId = 0U;

		private readonly Dictionary<uint, Ticket> _tickets = new Dictionary<uint, Ticket>();

		public List<uint> TicketIds {  get { return _tickets.Keys.ToList(); } }

		public Ticket GetTicket(uint ticketId)
		{
			return _tickets[ticketId];
		}

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
						AddTicket(uint.Parse(line.Substring(offset, length)));
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

		/// <summary>
		/// Removed the ticket with the given id from the list.
		/// </summary>
		/// <param name="id">The id of the ticket to remove.</param>
		public void RemoveTicket(uint id)
		{
			_tickets[id].RemoveFile();
			_tickets.Remove(id);
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
					line += Ticket.CreateFilePath(".", _tickets[key].ID) + ")";
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

		private uint NextUnusedID()
		{
            if (_tickets.Count == (_nextId + 1U))
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

		private Ticket AddTicket(uint id)
		{
			var ticket = new Ticket(_ticketDirectory, id);
            _tickets.Add(id, ticket);
			return ticket;
        }
    }
}
