using TinyTicketSystem;

namespace Tests
{
    [TestClass]
    public class TicketTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var sut = new Ticket("", 1);
        }
    }
}