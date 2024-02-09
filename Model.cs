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
		private string _workingDirectory;

		private readonly List<Ticket> _ticketList = new List<Ticket>();

		public List<Ticket> TicketList {  get { return _ticketList; } }

		public Model(string workingDirectory)
		{
			_workingDirectory = workingDirectory;
			if (!Directory.Exists(_workingDirectory))
			{
				throw new Exception("Directory does not exist");
			}
		}

		public void AddEmptyTicket()
		{
			_ticketList.Sort();
			UInt32 newID = 0;
			foreach (var ticket in _ticketList)
			{
				if (newID == ticket.ID)
				{
					++newID;
					continue;
				}
				break;
			}
			_ticketList.Add(new Ticket(newID));
		}
	}
}
