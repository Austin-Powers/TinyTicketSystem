using TinyTicketSystem;

namespace Tests
{
    [TestClass]
    public class TicketTests
    {
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
            var sut = new Ticket(".", 0U);

            // Act

            // Assert
            Assert.IsTrue(sut.Empty(), "Ticket not empty after construction");
        }

        [TestMethod]
        public void TestToString()
        {
            // Arrange
            var sut = new Ticket(".", 17U);
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
            var sut0 = new Ticket(".", 0U);
            var sut1 = new Ticket(".", 1U);
            var sut2 = new Ticket(".", 2U);

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
            var sut = new Ticket(".", 0U);
            Assert.IsFalse(sut.Closed);

            sut.Closed = true;
            Assert.IsTrue(sut.Closed);

            sut.Closed = false;
            Assert.IsFalse(sut.Closed);
        }

        [TestMethod]
        public void TestTitleField()
        {
            var sut = new Ticket(".", 0U);
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
            var sut = new Ticket(".", 0U);
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
            var sut = new Ticket(".", 0U);
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
            var sut = new Ticket(".", 0U);
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
    }
}