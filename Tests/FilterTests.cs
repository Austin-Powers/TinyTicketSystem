using TicketModel;

namespace Tests
{
    [TestClass()]
    public class FilterTests
    {
        private readonly string ticketDir = "filterTest";
        
        private Model? model;
        
        [TestInitialize]
        public void Initialize()
        {
            Directory.CreateDirectory(ticketDir);
            model = new Model(ticketDir);
            var id = model.AddEmptyTicket();
        }

        [TestCleanup]
        public void Cleanup()
        {
            var indexPath = Path.Combine(ticketDir, "index.md");
            if (File.Exists(indexPath))
            {
                File.Delete(indexPath);
            }
            var ticketPath = Path.Combine(ticketDir, "100");
            if (Directory.Exists(ticketPath))
            {
                Directory.Delete(ticketPath, true);
            }
        }

        [TestMethod]
        public void TestNoSetFiltersReturnsAllTicketsOfModel()
        {
            // Arrange
            var sut = new Filter();

            // Act
            var result = sut.Apply(model);

            // Assert
            Assert.IsNotNull(model); // mainly to please linter
            Assert.AreEqual(model.TicketIds.Count, result.Count);
        }
    }
}
