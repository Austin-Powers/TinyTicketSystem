﻿using TinyTicketSystem;

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
            Assert.IsTrue(0U == sut.Tags.Count);
            Assert.IsTrue(0U == sut.TicketIds.Count);
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
            Assert.IsTrue(1U == id0);
            Assert.IsTrue(2U == id1);
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
        public void TestSaveIndex()
        {
            // Arrange
            Cleanup();
            var sut = new Model(ticketDir);
            var ticketId0 = sut.AddEmptyTicket();

            // Act
            sut.SaveIndex();
            sut = new Model(ticketDir);

            // Assert
            Assert.IsNotNull(sut.GetTicket(ticketId0));
        }
    }
}
