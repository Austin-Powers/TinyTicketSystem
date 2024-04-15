using TicketModel;

namespace Tests
{
    [TestClass]
    public class FilterTests
    {
        private readonly string ticketDir = "filterTest";

        private Model? model;

        [ClassInitialize]
        public void Initialize()
        {
            model = new Model(ticketDir);
        }

        [ClassCleanup]
        public void Cleanup()
        {
            if (File.Exists("index.md"))
            {
                File.Delete("index.md");
            }
            if (Directory.Exists("100"))
            {
                Directory.Delete("100", true);
            }
        }

        [TestMethod]
        public void TestNoSetFiltersReturnsAllTicketsOfModel()
        {
            // Arrange


            // Act

            // Assert
        }
    }
}
