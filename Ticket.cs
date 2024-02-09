using System;
using System.Collections.Generic;
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
		/// The identifier to tell tickets apart.
		/// </summary>
		public UInt32 ID { get; }

		private string _title;

		/// <summary>
		/// The title of the ticket.
		/// </summary>
		public string Title { get { return _title ?? ""; } set { _title = value; } }

		private string _details;

		/// <summary>
		/// The details of the ticket.
		/// </summary>
		public string Details { get { return _details ?? ""; } set { _details = value; } }

		private readonly List<UInt32> _idsOfTicketsBlockingThisOne = new List<UInt32>();

		public Ticket(UInt32 id) {  ID = id; }

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
