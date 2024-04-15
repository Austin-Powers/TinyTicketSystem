using TicketModel;
using TinyTicketSystem;

namespace Tests
{
    [TestClass]
    public class ModelTests
    {
        public readonly string ticketDir = ".";

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
        public void TestConstruction()
        {
            // Arrange
            Cleanup();
            var sut = new Model(ticketDir);

            // Act

            // Assert
            Assert.AreEqual(0, sut.Tags.Count());
            Assert.AreEqual(0, sut.TicketIds.Count());
        }

        [TestMethod]
        public void TestGetTicketForUnknownIDReturnsNull()
        {
            // Arrange
            Cleanup();
            var sut = new Model(ticketDir);

            // Act
            var ticket = sut.GetTicket(12U);

            // Assert
            Assert.AreEqual(null, ticket);
        }

        [TestMethod]
        public void TestAddingEmptyTicket()
        {
            // Arrange
            Cleanup();
            var sut = new Model(ticketDir);

            // Act
            var id0 = sut.AddEmptyTicket();
            var id1 = sut.AddEmptyTicket();

            // Assert
            Assert.AreEqual(1U, id0);
            Assert.AreEqual(2U, id1);
            Assert.IsNotNull(sut.GetTicket(id0));
            Assert.IsNotNull(sut.GetTicket(id1));
            Assert.IsTrue(sut.GetTicket(id0).Empty());
            Assert.IsTrue(sut.GetTicket(id1).Empty());
        }

        [TestMethod]
        public void TestRemoveTicketIfEmptyOnEmptyTicket()
        {
            // Arrange
            Cleanup();
            var sut = new Model(ticketDir);
            var ticketId = sut.AddEmptyTicket();

            // Act
            var result = sut.RemoveTicketIfEmpty(ticketId);

            // Assert
            Assert.IsTrue(result);
            Assert.IsNull(sut.GetTicket(ticketId));
        }

        [TestMethod]
        public void TestRemoveTicketIfEmptyOnNonEmptyTicket()
        {
            // Arrange
            Cleanup();
            var sut = new Model(ticketDir);
            var ticketId = sut.AddEmptyTicket();
            sut.GetTicket(ticketId).Title = "Test";

            // Act
            var result = sut.RemoveTicketIfEmpty(ticketId);

            // Assert
            Assert.IsFalse(result);
            Assert.IsNotNull(sut.GetTicket(ticketId));
        }

        [TestMethod]
        public void TestRemoveTicket()
        {
            // Arrange
            Cleanup();
            var sut = new Model(ticketDir);
            var ticketId = sut.AddEmptyTicket();
            sut.GetTicket(ticketId).Title = "Test";

            // Act
            sut.RemoveTicket(ticketId);

            // Assert
            Assert.IsNull(sut.GetTicket(ticketId));
        }

        [TestMethod]
        public void TestIsBlockedReturnsFalseOnTicketWithoutBlockingIDs()
        {
            // Arrange
            Cleanup();
            var sut = new Model(ticketDir);
            var ticketId = sut.AddEmptyTicket();

            // Act
            var result = sut.IsBlocked(sut.GetTicket(ticketId));

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestIsBlockedReturnsTrueIfTicketHasOpenTicketBlockingIt()
        {
            // Arrange
            Cleanup();
            var sut = new Model(ticketDir);
            var ticketId0 = sut.AddEmptyTicket();
            var ticketId1 = sut.AddEmptyTicket();
            var ticket0 = sut.GetTicket(ticketId0);
            ticket0.BlockingTicketsIDs.Add(ticketId1);

            // Act
            var result = sut.IsBlocked(ticket0);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestIsBlockedReturnsFalseIfTicketHasClosedTicketBlockingIt()
        {
            // Arrange
            Cleanup();
            var sut = new Model(ticketDir);
            var ticketId0 = sut.AddEmptyTicket();
            var ticketId1 = sut.AddEmptyTicket();
            var ticket0 = sut.GetTicket(ticketId0);
            ticket0.BlockingTicketsIDs.Add(ticketId1);
            sut.GetTicket(ticketId1).Closed = true;

            // Act
            var result = sut.IsBlocked(ticket0);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestSaveAndLoadIndex()
        {
            // Arrange
            Cleanup();
            var sut = new Model(ticketDir);
            var ticketId0 = sut.AddEmptyTicket();
            var ticket = sut.GetTicket(ticketId0);
            ticket.Title = "Test";
            ticket.Update();

            // Act
            sut.SaveIndex();
            sut = new Model(ticketDir);

            // Assert
            Assert.IsNotNull(sut.GetTicket(ticketId0));
        }

        [TestMethod]
        public void TestAddTagsOf()
        {
            // Arrange
            Cleanup();
            var sut = new Model(ticketDir);
            var ticketId = sut.AddEmptyTicket();
            var ticket = sut.GetTicket(ticketId);

            var tag0 = "Test";
            var tag1 = "Tag";
            ticket.Tags.Add(tag0);
            ticket.Tags.Add(tag1);

            // Act
            sut.AddTagsOf(ticket);

            // Assert
            Assert.AreEqual(2, sut.Tags.Count());
            Assert.IsTrue(sut.Tags.Contains(tag0));
            Assert.IsTrue(sut.Tags.Contains(tag1));
        }

        [TestMethod]
        public void TestOnTicketUpdatedIsNullSafe()
        {
            // Arrange
            var sut = new Model(ticketDir);
            ITicketObserver observer = sut;

            // Act
            observer.OnTicketUpdated(null);

            // Assert
            // Checked automatically
        }

        [TestMethod]
        public void TestOnTicketUpdatedOnlyUsesTicketsFromModel()
        {
            // Arrange
            Cleanup();
            var sut = new Model(ticketDir);
            ITicketObserver observer = sut;
            var ticket = new Ticket(ticketDir, 0U);
            ticket.Tags.Add("Test");

            // Act
            observer.OnTicketUpdated(ticket);

            // Assert
            Assert.AreEqual(0, sut.Tags.Count);
        }

        [TestMethod]
        public void TestTicketUpdate()
        {
            // Arrange
            var tag = "Test";
            Cleanup();
            var sut = new Model(ticketDir);
            var ticketId = sut.AddEmptyTicket();
            var ticket = sut.GetTicket(ticketId);

            // Act
            ticket.Title = tag; // ticket needs a title to be not counted as empty
            ticket.Tags.Add(tag);
            ticket.Update();

            // Assert
            Assert.AreEqual(1, sut.Tags.Count);
            Assert.IsTrue(sut.Tags.Contains(tag));
        }

        [TestMethod]
        public void TestRemoveTicketRemovesBlockingIdFromOtherTickets()
        {
            // Arrange
            Cleanup();
            var sut = new Model(ticketDir);
            var ticketId0 = sut.AddEmptyTicket();
            var ticketId1 = sut.AddEmptyTicket();
            var ticket0 = sut.GetTicket(ticketId0);
            ticket0.BlockingTicketsIDs.Add(ticketId1);

            // Act
            sut.RemoveTicket(ticketId1);

            // Assert
            Assert.AreEqual(0, ticket0.BlockingTicketsIDs.Count);
        }

        [TestMethod]
        public void TestReopenTicketAlsoReopensTicketsBlockedByIt()
        {
            // Arrange
            Cleanup();
            var sut = new Model(ticketDir);
            var ticketId0 = sut.AddEmptyTicket();
            var ticketId1 = sut.AddEmptyTicket();
            var ticketId2 = sut.AddEmptyTicket();

            var ticket0 = sut.GetTicket(ticketId0);
            var ticket1 = sut.GetTicket(ticketId1);
            var ticket2 = sut.GetTicket(ticketId2);

            ticket0.Title = "0";
            ticket1.Title = "1";
            ticket2.Title = "2";
            ticket0.Closed = true;
            ticket1.Closed = true;
            ticket2.Closed = true;
            ticket0.BlockingTicketsIDs.Add(ticketId1);
            ticket1.BlockingTicketsIDs.Add(ticketId2);
            ticket0.Update();
            ticket1.Update();
            ticket2.Update();

            // Act
            ticket2.Closed = false;
            ticket2.CommitChanges();

            // Assert
            Assert.IsFalse(ticket0.Closed);
            Assert.IsFalse(ticket1.Closed);
            Assert.IsFalse(ticket2.Closed);
        }
    }
}
