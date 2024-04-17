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
            var id0 = model.AddEmptyTicket();
            var ticket = model.GetTicket(id0);
            ticket.Title = "Test";
            var id = model.AddEmptyTicket();
            ticket = model.GetTicket(id);
            ticket.Title = "Temp";
            ticket.Tags.Add("Tag");
            ticket.Closed = true;
            id = model.AddEmptyTicket();
            ticket = model.GetTicket(id);
            ticket.Title = "Emp";
            ticket.Tags.Add("Tagging");
            ticket.BlockingTicketsIDs.Add(id0);
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

        [TestMethod]
        public void TestFilteringForOpenTickets()
        {
            // Arrange
            var sut = new Filter();
            sut.State = Filter.TicketState.Open;

            // Act
            var result = sut.Apply(model);

            // Assert
            Assert.IsNotNull(model); // mainly to please linter
            Assert.AreEqual(1, result.Count);
            foreach (var r in result)
            {
                Assert.IsFalse(r.Closed);
                Assert.IsFalse(model.IsBlocked(r));
            }
        }

        [TestMethod]
        public void TestFilteringForBlockedTickets()
        {
            // Arrange
            var sut = new Filter();
            sut.State = Filter.TicketState.Blocked;

            // Act
            var result = sut.Apply(model);

            // Assert
            Assert.IsNotNull(model); // mainly to please linter
            Assert.AreEqual(1, result.Count);
            foreach (var r in result)
            {
                Assert.IsFalse(r.Closed);
                Assert.IsTrue(model.IsBlocked(r));
            }
        }

        [TestMethod]
        public void TestFilteringForOpenOrBlockedTickets()
        {
            // Arrange
            var sut = new Filter();
            sut.State = Filter.TicketState.OpenOrBlocked;

            // Act
            var result = sut.Apply(model);

            // Assert
            Assert.IsNotNull(model); // mainly to please linter
            Assert.AreEqual(2, result.Count);
            foreach (var r in result)
            {
                Assert.IsFalse(r.Closed);
            }
        }

        [TestMethod]
        public void TestFilteringForClosedTickets()
        {
            // Arrange
            var sut = new Filter();
            sut.State = Filter.TicketState.Closed;

            // Act
            var result = sut.Apply(model);

            // Assert
            Assert.IsNotNull(model); // mainly to please linter
            Assert.AreEqual(1, result.Count);
            foreach (var r in result)
            {
                Assert.IsTrue(r.Closed);
            }
        }

        [TestMethod]
        public void TestSettingTitleToNullReturnsAllTickets()
        {
            // Arrange
            var sut = new Filter();
            sut.Title = null;

            // Act
            var result = sut.Apply(model);

            // Assert
            Assert.IsNotNull(model); // mainly to please linter
            Assert.AreEqual(model.TicketIds.Count, result.Count);
        }

        [TestMethod]
        public void TestSettingTitleToEmptyReturnsAllTickets()
        {
            // Arrange
            var sut = new Filter();
            sut.Title = "";

            // Act
            var result = sut.Apply(model);

            // Assert
            Assert.IsNotNull(model); // mainly to please linter
            Assert.AreEqual(model.TicketIds.Count, result.Count);
        }

        [TestMethod]
        public void TestSettingTitleCaseSensitive()
        {
            // Arrange
            var sut = new Filter();
            var testTitle = "Te";
            sut.Title = testTitle;

            // Act
            var result = sut.Apply(model);

            // Assert
            Assert.IsNotNull(model); // mainly to please linter
            Assert.AreEqual(2, result.Count);
            foreach (var r in result)
            {
                Assert.IsTrue(r.Title.Contains(testTitle));
            }
        }

        [TestMethod]
        public void TestSettingTitleCaseInsensitive()
        {
            // Arrange
            var sut = new Filter();
            var testTitle = "Te";
            sut.Title = testTitle;

            // Act
            var result = sut.Apply(model);

            // Assert
            Assert.IsNotNull(model); // mainly to please linter
            Assert.AreEqual(2, result.Count);
            foreach (var r in result)
            {
                Assert.IsTrue(r.Title.Contains(testTitle, StringComparison.InvariantCultureIgnoreCase));
            }
        }

        [TestMethod]
        public void TestSettingTitleCaseInsensitive2()
        {
            // Arrange
            var sut = new Filter();
            var testTitle = "emp";
            sut.Title = testTitle;

            // Act
            var result = sut.Apply(model);

            // Assert
            Assert.IsNotNull(model); // mainly to please linter
            Assert.AreEqual(2, result.Count);
            foreach (var r in result)
            {
                Assert.IsTrue(r.Title.Contains(testTitle, StringComparison.InvariantCultureIgnoreCase));
            }
        }

        [TestMethod]
        public void TestSettingTagToNullReturnsAllTickets()
        {
            // Arrange
            var sut = new Filter();
            sut.Tag = null;

            // Act
            var result = sut.Apply(model);

            // Assert
            Assert.IsNotNull(model); // mainly to please linter
            Assert.AreEqual(model.TicketIds.Count, result.Count);
        }

        [TestMethod]
        public void TestSettingTagToEmptyReturnsAllTickets()
        {
            // Arrange
            var sut = new Filter();
            sut.Tag = "";

            // Act
            var result = sut.Apply(model);

            // Assert
            Assert.IsNotNull(model); // mainly to please linter
            Assert.AreEqual(model.TicketIds.Count, result.Count);
        }

        [TestMethod]
        public void TestSettingTagReturnsTicketWithExactlyThatTag()
        {
            // Arrange
            var searchString = "Tag";
            var sut = new Filter();
            sut.Tag = searchString;

            // Act
            var result = sut.Apply(model);

            // Assert
            Assert.IsNotNull(model); // mainly to please linter
            Assert.AreEqual(1, result.Count);
            foreach (var r in result)
            {
                Assert.IsTrue(r.Tags.Contains(searchString));
            }
        }
    }
}
