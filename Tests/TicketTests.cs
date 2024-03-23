using TicketModel;
using TinyTicketSystem;

namespace Tests
{
    public class TicketObserver : ITicketObserver
    {
        public TicketObserver() { }

        void ITicketObserver.OnTicketUpdated(Ticket ticket)
        {

        }
    }

    [TestClass]
    public class TicketTests
    {
        public readonly string ticketDir = ".";

        public void CleanupTestFile(uint id)
        {
            var path = Ticket.CreateFilePath(ticketDir, id);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public bool CheckFile(uint id)
        {
            return File.Exists(Ticket.CreateFilePath(ticketDir, id));
        }

        [TestMethod]
        public void TestCreateFilePath()
        {
            // Arrange
            var path = "\\test\\tickets";
            var id = 12U;

            // Act
            var result = Ticket.CreateFilePath(path, id);

            // Assert
            Assert.AreEqual(result, "\\test\\tickets\\100\\12.md", "Filepath not created correctly");
        }

        [TestMethod]
        public void TestTicketEmptyAfterConstruction()
        {
            // Arrange
            var sut = new Ticket(ticketDir, 0U);

            // Act

            // Assert
            Assert.IsTrue(sut.Empty(), "Ticket not empty after construction");
        }

        [TestMethod]
        public void TestTicketEmptyNotInfluencedByClosedFlag()
        {
            // Arrange
            var sut = new Ticket(ticketDir, 0U);

            // Act
            sut.Closed = true;

            // Assert
            Assert.IsTrue(sut.Empty(), "Ticket not empty after construction");
        }

        [TestMethod]
        public void TestToString()
        {
            // Arrange
            var sut = new Ticket(ticketDir, 17U);
            sut.Title = "Test";

            // Act
            var result = sut.ToString();

            // Assert
            Assert.AreEqual("17 - Test", result, "ToString return value incorrect");
        }

        [TestMethod]
        public void TestCompareTo()
        {
            // Arrange
            var sut0 = new Ticket(ticketDir, 0U);
            var sut1 = new Ticket(ticketDir, 1U);
            var sut2 = new Ticket(ticketDir, 2U);

            // Act

            // Assert
            Assert.AreEqual(sut0.CompareTo(null), 1, "CompareTo null did not return 1");
            Assert.AreEqual(sut1.CompareTo(sut1), 0, "CompareTo self did not return 0");
            Assert.AreEqual(sut1.CompareTo(sut0), 1, "CompareTo lower did not return 1");
            Assert.AreEqual(sut1.CompareTo(sut2), -1, "CompareTo higher did not return -1");
        }

        [TestMethod]
        public void TestClosedField()
        {
            var sut = new Ticket(ticketDir, 0U);
            Assert.IsFalse(sut.Closed);

            sut.Closed = true;
            Assert.IsTrue(sut.Closed);

            sut.Closed = false;
            Assert.IsFalse(sut.Closed);
        }

        [TestMethod]
        public void TestTitleField()
        {
            var sut = new Ticket(ticketDir, 0U);
            Assert.AreEqual(sut.Title, "");

            sut.Title = null;
            Assert.AreEqual(sut.Title, "");

            sut.Title = "Test";
            Assert.AreEqual(sut.Title, "Test");

            sut.Title = null;
            Assert.AreEqual(sut.Title, "");
        }

        [TestMethod]
        public void TestDetailsField()
        {
            var sut = new Ticket(ticketDir, 0U);
            Assert.AreEqual(sut.Details, "");

            sut.Details = null;
            Assert.AreEqual(sut.Details, "");

            sut.Details = "Test";
            Assert.AreEqual(sut.Details, "Test");

            sut.Details = null;
            Assert.AreEqual(sut.Details, "");
        }

        [TestMethod]
        public void TestTagsField()
        {
            var sut = new Ticket(ticketDir, 0U);
            Assert.AreEqual(sut.Tags.Count, 0);

            sut.Tags.Add("test");
            Assert.AreEqual(sut.Tags.Count, 1);
            Assert.IsTrue(sut.Tags.Contains("test"));

            sut.Tags.Add("test");
            Assert.AreEqual(sut.Tags.Count, 1);
            Assert.IsTrue(sut.Tags.Contains("test"));

            sut.Tags.Add("foo");
            Assert.AreEqual(sut.Tags.Count, 2);
            Assert.IsTrue(sut.Tags.Contains("test"));
            Assert.IsTrue(sut.Tags.Contains("foo"));

            sut.Tags.Remove("test");
            Assert.AreEqual(sut.Tags.Count, 1);
            Assert.IsTrue(sut.Tags.Contains("foo"));

            sut.Tags = null;
            Assert.AreEqual(sut.Tags.Count, 0);

            var set = new HashSet<string>();
            set.Add("bar");
            set.Add("man");
            sut.Tags = set;
            Assert.AreEqual(sut.Tags.Count, 2);
            Assert.IsTrue(sut.Tags.Contains("man"));
            Assert.IsTrue(sut.Tags.Contains("bar"));
        }

        [TestMethod]
        public void TestBlockingIds()
        {
            var sut = new Ticket(ticketDir, 0U);
            Assert.AreEqual(sut.BlockingTicketsIDs.Count, 0);

            sut.BlockingTicketsIDs.Add(1U);
            Assert.AreEqual(sut.BlockingTicketsIDs.Count, 1);
            Assert.IsTrue(sut.BlockingTicketsIDs.Contains(1U));

            sut.BlockingTicketsIDs.Add(1U);
            Assert.AreEqual(sut.BlockingTicketsIDs.Count, 1);
            Assert.IsTrue(sut.BlockingTicketsIDs.Contains(1U));

            sut.BlockingTicketsIDs.Add(2U);
            Assert.AreEqual(sut.BlockingTicketsIDs.Count, 2);
            Assert.IsTrue(sut.BlockingTicketsIDs.Contains(1U));
            Assert.IsTrue(sut.BlockingTicketsIDs.Contains(2U));

            sut.BlockingTicketsIDs.Remove(1U);
            Assert.AreEqual(sut.BlockingTicketsIDs.Count, 1);
            Assert.IsTrue(sut.BlockingTicketsIDs.Contains(2U));

            sut.BlockingTicketsIDs = null;
            Assert.AreEqual(sut.BlockingTicketsIDs.Count, 0);

            var set = new HashSet<uint>();
            set.Add(3U);
            set.Add(4U);
            sut.BlockingTicketsIDs = set;
            Assert.AreEqual(sut.BlockingTicketsIDs.Count, 2);
            Assert.IsTrue(sut.BlockingTicketsIDs.Contains(3U));
            Assert.IsTrue(sut.BlockingTicketsIDs.Contains(4U));
        }

        [TestMethod]
        public void TestCommitingEmptyTicketDoesNotWriteAFile()
        {
            // Arrange
            var id = 0U;
            CleanupTestFile(id);
            var sut = new Ticket(ticketDir, id);

            // Assert
            Assert.IsFalse(sut.CommitChanges());
            Assert.IsFalse(CheckFile(id));
        }

        [TestMethod]
        public void TestCommitingClosedEmptyTicketDoesNotWriteAFile()
        {
            // Arrange
            var id = 1U;
            CleanupTestFile(id);
            var sut = new Ticket(ticketDir, id);

            // Act
            sut.Closed = true;

            // Assert
            Assert.IsTrue(sut.CommitChanges());
            Assert.IsFalse(CheckFile(id));
        }

        [TestMethod]
        public void TestCommitingTicketWithNewTitle()
        {
            // Arrange
            var id = 2U;
            CleanupTestFile(id);
            var sut = new Ticket(ticketDir, id);

            // Act
            sut.Title = "Test";

            // Assert
            Assert.IsTrue(sut.CommitChanges());
            Assert.IsTrue(CheckFile(id));
        }

        [TestMethod]
        public void TestCommitingTicketWithNewDetails()
        {
            // Arrange
            var id = 3U;
            CleanupTestFile(id);
            var sut = new Ticket(ticketDir, id);

            // Act
            sut.Details = "Test";

            // Assert
            Assert.IsTrue(sut.CommitChanges());
            Assert.IsTrue(CheckFile(id));
        }

        [TestMethod]
        public void TestCommitingTicketWithNewTags()
        {
            // Arrange
            var id = 4U;
            CleanupTestFile(id);
            var sut = new Ticket(ticketDir, id);

            // Act
            sut.Tags.Add("Test");

            // Assert
            Assert.IsTrue(sut.CommitChanges());
            Assert.IsTrue(CheckFile(id));
        }

        public void TestCommitingTicketAfterRemovingTags()
        {
            // Arrange
            var tag = "Foo";
            var id = 5U;
            CleanupTestFile(id);
            var sut = new Ticket(ticketDir, id);
            sut.Tags.Add(tag);
            sut.Tags.Add("Bar");
            sut.CommitChanges();
            CleanupTestFile(id);

            // Act
            sut.Tags.Remove(tag);

            // Assert
            Assert.IsTrue(sut.CommitChanges());
            Assert.IsTrue(CheckFile(id));
        }

        [TestMethod]
        public void TestCommitingTicketWithNewBlockingIDs()
        {
            // Arrange
            var id = 6U;
            CleanupTestFile(id);
            var sut = new Ticket(ticketDir, id);

            // Act
            sut.BlockingTicketsIDs.Add(1U);

            // Assert
            Assert.IsTrue(sut.CommitChanges());
            Assert.IsTrue(CheckFile(id));
        }

        public void TestCommitingTicketAfterRemovingBlockingIDs()
        {
            // Arrange
            var blockingId = 4U;
            var id = 7U;
            CleanupTestFile(id);
            var sut = new Ticket(ticketDir, id);
            sut.BlockingTicketsIDs.Add(blockingId);
            sut.BlockingTicketsIDs.Add(5U);
            sut.CommitChanges();
            CleanupTestFile(id);

            // Act
            sut.BlockingTicketsIDs.Remove(blockingId);

            // Assert
            Assert.IsTrue(sut.CommitChanges());
            Assert.IsTrue(CheckFile(id));
        }

        [TestMethod]
        public void TestSavingAndLoadingKeepsDataCorrect()
        {
            // Arrange
            var expectedTitle = "TitleText";
            var expectedDetails = "DetailText";
            var tag0 = "Tag0";
            var tag1 = "Tag1";
            var blockingId0 = 2U;
            var blockingId1 = 3U;
            var blockingId2 = 4U;

            var id = 8U;
            CleanupTestFile(id);
            var sut = new Ticket(ticketDir, id);

            // Act
            sut.Closed = true;
            sut.Title = expectedTitle;
            sut.Details = expectedDetails;
            sut.Tags.Add(tag0);
            sut.Tags.Add(tag1);
            sut.BlockingTicketsIDs.Add(blockingId0);
            sut.BlockingTicketsIDs.Add(blockingId1);
            sut.BlockingTicketsIDs.Add(blockingId2);
            sut.CommitChanges();

            // Assert
            sut = new Ticket(ticketDir, id);
            Assert.IsTrue(sut.Closed);
            Assert.AreEqual(expectedTitle, sut.Title);
            Assert.AreEqual(expectedDetails, sut.Details);
            Assert.AreEqual(2, sut.Tags.Count);
            Assert.IsTrue(sut.Tags.Contains(tag0));
            Assert.IsTrue(sut.Tags.Contains(tag1));
            Assert.AreEqual(3, sut.BlockingTicketsIDs.Count);
            Assert.IsTrue(sut.BlockingTicketsIDs.Contains(blockingId0));
            Assert.IsTrue(sut.BlockingTicketsIDs.Contains(blockingId1));
            Assert.IsTrue(sut.BlockingTicketsIDs.Contains(blockingId2));
        }
    }
}