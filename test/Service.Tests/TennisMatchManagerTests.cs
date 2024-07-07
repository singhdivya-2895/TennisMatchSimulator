using Moq;
using TennisMatchService;
using TennisMatchService.Logger;

namespace Service.Tests
{
    public class TennisMatchManagerTests
    {
        [Fact]
        public void PlayMatch_SimpleMatchFinished_LogsCorrectly()
        {
            // Arrange
            var loggerMock = new Mock<IMatchLogger>();
            var manager = new TennisMatchManager(loggerMock.Object);

            string[] players = new string[] { "Player1", "Player2" };
            int[] points = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0 };

            // Act
            manager.PlayMatch(players, points);

            // Assert
            loggerMock.Verify(x => x.LogNewMatchStart(players[0], players[1]), Times.Once);

            loggerMock.Verify(x => x.LogMessage(It.IsAny<string>()), Times.Exactly((points.Length * 4) + 1));

            loggerMock.Verify(x => x.LogLineBreak(), Times.Exactly(points.Length));

            loggerMock.Verify(x => x.LogMessage("Final Match Winner: Player1"), Times.Once);
        }

        [Fact]
        public void PlayMatch_NoPointsLogged_WhenNoPointsGiven()
        {
            // Arrange
            var loggerMock = new Mock<IMatchLogger>();
            var manager = new TennisMatchManager(loggerMock.Object);

            string[] players = new string[] { "Player1", "Player2" };
            int[] points = Array.Empty<int>();

            // Act
            manager.PlayMatch(players, points);

            // Assert
            loggerMock.Verify(x => x.LogMessage(It.IsAny<string>()), Times.Once);

            loggerMock.Verify(x => x.LogMessage("No Result"), Times.Once);
        }

        [Fact]
        public void PlayMatch_MatchUnfinished_LogsCorrectly()
        {
            // Arrange
            var loggerMock = new Mock<IMatchLogger>();
            var manager = new TennisMatchManager(loggerMock.Object);

            string[] players = new string[] { "Player1", "Player2" };
            int[] points = new int[] { 0, 0, 0, 0, 0, };

            // Act
            manager.PlayMatch(players, points);

            // Assert
            loggerMock.Verify(x => x.LogNewMatchStart(players[0], players[1]), Times.Once);

            loggerMock.Verify(x => x.LogMessage(It.IsAny<string>()), Times.Exactly((points.Length * 4) + 1));

            loggerMock.Verify(x => x.LogLineBreak(), Times.Exactly(points.Length));

            loggerMock.Verify(x => x.LogMessage("Final Match Winner: Player1"), Times.Never);

            loggerMock.Verify(x => x.LogMessage("No Result"), Times.Once);
        }


        [Fact]
        public void PlayMatch_InvalidRequest_ErrorLogged()
        {
            // Arrange
            var loggerMock = new Mock<IMatchLogger>();
            var manager = new TennisMatchManager(loggerMock.Object);

            string[] players = new string[] { "Player1" };
            int[] points = Array.Empty<int>();

            // Act
            manager.PlayMatch(players, points);

            // Assert
            loggerMock.Verify(x => x.LogMessage(It.IsAny<string>()), Times.Never);

            loggerMock.Verify(x => x.LogError(It.IsAny<string>()), Times.Once);
        }
    }
}