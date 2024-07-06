using Domain.Models;

namespace Domain.Tests.Models
{
    public class MatchTests
    {
        private readonly Player _winningPlayer1 = new("Player 1");
        [Fact]
        public void Match_Initialization_ShouldSetPlayersAndStartFirstSet()
        {
            var _player1 = new Player("Player 1");
            var _player2 = new Player("Player 2");
            var match = new Match(_player1, _player2);

            match.Player1.Should().Be(_player1);
            match.Player2.Should().Be(_player2);
            match.Sets.Should().HaveCount(1);
        }

        [Fact]
        public void StartNewSet_ShouldAddNewSet()
        {
            var _player1 = new Player("Player 1");
            var _player2 = new Player("Player 2");
            var match = new Match(_player1, _player2);
            var initialSetCount = match.Sets.Count;

            match.StartNewSet();

            match.Sets.Should().HaveCount(initialSetCount + 1);
        }

        [Fact]
        public void StartNewGame_ShouldCreateNewGame()
        {
            var _player1 = new Player("Player 1");
            var _player2 = new Player("Player 2");
            var match = new Match(_player1, _player2);

            match.StartNewGame();

            match.GetCurrentGameScore().Should().NotBeNull();
        }

        [Fact]
        public void PointWonBy_ShouldUpdateCurrentGameScore()
        {
            var _player1 = new Player("Player 1");
            var _player2 = new Player("Player 2");
            var match = new Match(_player1, _player2);

            match.PointWonBy(0);

            match.GetCurrentGameScore().Should().Contain("15");
        }

        [Fact]
        public void GetCurrentScores_ShouldReturnScore()
        {
            var _player1 = new Player("Player 1");
            var _player2 = new Player("Player 2");
            var match = new Match(_player1, _player2);

            match.GetCurrentGameScore().Should().Be("0-0");
            match.GetCurrentSetScore().Should().Be("0-0");
            match.GetCurrentMatchScore().Should().Be("0-0");
        }

        [Fact]
        public void IsMatchOver_ShouldReturnFalseInitially()
        {
            var _player1 = new Player("Player 1");
            var _player2 = new Player("Player 2");
            var match = new Match(_player1, _player2);

            match.IsMatchOver().Should().BeFalse();
        }

        [Fact]
        public void GetMatchWinner_ShouldReturnWinnerAndStatus()
        {
            var _player2 = new Player("Player 2");
            var match = new Match(_winningPlayer1, _player2);


            SimulateSetWin(match, _winningPlayer1);

            match.GetCurrentGameScore().Should().Be("0-0");
            match.GetCurrentMatchScore().Should().Be("3-0");
            var (winner, isMatchOver) = match.GetMatchWinner();

            isMatchOver.Should().BeTrue();
            winner.Should().Be(_winningPlayer1);
        }

        [Fact]
        public void TieBreakerTest_ShouldSimulateTieBreaker()
        {
            var _player1 = new Player("Player 1");
            var _player2 = new Player("Player 2");
            var match = new Match(_player1, _player2);

            // Simulate reaching a tie-breaker (6-6 in games)
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    match.PointWonBy(0);
                }
                for (int j = 0; j < 4; j++)
                {
                    match.PointWonBy(1);
                }

            }

            var setScore = match.GetCurrentSetScore();
            setScore.Should().Be("10-10");

            var matchScore = match.GetCurrentMatchScore();
            matchScore.Should().Be("0-0");
        }


        [Fact]
        public void AdvantageTests_ShouldSimulateAdvantage()
        {
            var _player1 = new Player("Player 1");
            var _player2 = new Player("Player 2");
            var match = new Match(_player1, _player2);

            for (int j = 0; j < 4; j++)
            {
                match.PointWonBy(0);
                match.PointWonBy(1);
            }
            match.GetCurrentGameScore().Should().Be("Deuce");
            match.PointWonBy(0);
            match.GetCurrentGameScore().Should().Be("Advantage Player 1");

            match.PointWonBy(1);
            match.GetCurrentGameScore().Should().Be("Deuce");

            match.PointWonBy(1);
            match.GetCurrentGameScore().Should().Be("Advantage Player 2");

            match.PointWonBy(1);
            match.GetCurrentGameScore().Should().Be("0-0");
            match.GetCurrentSetScore().Should().Be("0-1");

        }

        private void SimulateSetWin(Match match, Player winner)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    match.PointWonBy(winner == _winningPlayer1 ? 0 : 1);
                    match.PointWonBy(winner == _winningPlayer1 ? 0 : 1);
                    match.PointWonBy(winner == _winningPlayer1 ? 0 : 1);
                    match.PointWonBy(winner == _winningPlayer1 ? 0 : 1);
                }
            }
        }
    }
}
