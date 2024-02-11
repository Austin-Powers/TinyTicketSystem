using System;
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
		private readonly string _ticketDirectory;

		private uint _nextId = 0U;

		private readonly List<Ticket> _ticketList = new List<Ticket>();

		public List<Ticket> TicketList {  get { return _ticketList; } }

		public Model(string TicketDirectory)
		{
			_ticketDirectory = TicketDirectory;
			if (!Directory.Exists(_ticketDirectory))
			{
				throw new Exception("Directory does not exist");
			}

			var indexFilepath = Path.Combine(_ticketDirectory, "index.md");
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
						var length = line.IndexOf(']') - offset;
						var idString = line.Substring(offset, length);
						AddTicket(uint.Parse(line.Substring(offset, length)));
					}
				}
				catch (Exception e)
				{
					throw e;
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
		/// <remarks>
		/// This method may discover tickets that already exist in the folder,
		/// those tickets will be added to the model.
		/// </remarks>
		public void AddEmptyTicket()
		{
			var newTicket = AddTicket(NextUnusedID());
			while (!newTicket.Empty())
			{
                newTicket = AddTicket(NextUnusedID());
            }
		}

		private uint NextUnusedID()
		{
            if (_ticketList.Count == (_nextId + 1U))
            {
                // average case where the ids are continuous
                ++_nextId;
            }
            else if (_ticketList.Count == 0U)
			{
				// edge case if the ticket list is empty
                _nextId = 0U;
			}
            else
			{
				// edge case where deletion caused a hole in the ids
				_ticketList.Sort();
				_nextId = 0U;
				foreach (var ticket in _ticketList)
				{
					if (ticket.ID == _nextId)
					{
						++_nextId;
						continue;
					}
					break;
				}
            }
            return _nextId;
		}

		private Ticket AddTicket(uint id)
		{
			var ticket = new Ticket(_ticketDirectory, id);
            _ticketList.Add(ticket);
			return ticket;
        }
    }
}
