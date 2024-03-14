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
    }
}