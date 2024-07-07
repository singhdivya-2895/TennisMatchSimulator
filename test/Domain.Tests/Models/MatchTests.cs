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

            match.GetCurrentGameScore.Should().NotBeNull();
        }

        [Fact]
        public void PointWonBy_ShouldUpdateCurrentGameScore()
        {
            var _player1 = new Player("Player 1");
            var _player2 = new Player("Player 2");
            var match = new Match(_player1, _player2);

            match.RegisterPointWon(0);

            match.GetCurrentGameScore.Should().Be("15-0");
        }

        [Fact]
        public void GetCurrentScores_ShouldReturnScore()
        {
            var _player1 = new Player("Player 1");
            var _player2 = new Player("Player 2");
            var match = new Match(_player1, _player2);
            match.RegisterPointWon(0);
            match.RegisterPointWon(1);

            match.GetCurrentGameScore.Should().Be("15-15");
            match.GetCurrentSetScore.Should().Be("0-0");
            match.GetCurrentMatchScore.Should().Be("0-0");
        }

        [Fact]
        public void IsMatchOver_ShouldReturnFalseInitially()
        {
            var _player1 = new Player("Player 1");
            var _player2 = new Player("Player 2");
            var match = new Match(_player1, _player2);

            match.IsMatchOver.Should().BeFalse();
        }

        [Fact]
        public void GetMatchWinner_ShouldReturnWinnerAndStatus()
        {
            var _player2 = new Player("Player 2");
            var match = new Match(_winningPlayer1, _player2);


            SimulateSetWin(match, _winningPlayer1);

            match.GetCurrentGameScore.Should().Be("0-0");
            match.GetCurrentMatchScore.Should().Be("3-0");

            match.IsMatchOver.Should().BeTrue();
            match.GetMatchWinner.Should().Be(_winningPlayer1);
        }

        [Fact]
        public void GetMatchWinner_ShouldReturnPlayer2WinnerAndStatus()
        {
            var _player2 = new Player("Player 2");
            var match = new Match(_winningPlayer1, _player2);


            SimulateSetWin(match, _player2);

            match.GetCurrentGameScore.Should().Be("0-0");
            match.GetCurrentMatchScore.Should().Be("0-3");

            match.IsMatchOver.Should().BeTrue();
            match.GetMatchWinner.Should().Be(_player2);
        }

        [Fact]
        public void GetMatchWinner_ShouldNotRegisterPointAfterMatchOver()
        {
            var _player2 = new Player("Player 2");
            var match = new Match(_winningPlayer1, _player2);


            SimulateSetWin(match, _winningPlayer1);

            match.GetCurrentGameScore.Should().Be("0-0");
            match.GetCurrentMatchScore.Should().Be("3-0");

            match.IsMatchOver.Should().BeTrue();
            match.GetMatchWinner.Should().Be(_winningPlayer1);

            var isMatchOver = match.RegisterPointWon(0);
            match.GetCurrentGameScore.Should().Be("0-0");
            isMatchOver.Should().BeTrue();
        }

        [Fact]
        public void Match_SimulateUnfinishedMatch()
        {
            var _player1 = new Player("Player 1");
            var _player2 = new Player("Player 2");
            var match = new Match(_player1, _player2);

            // Simulate reaching a tie-breaker (6-6 in games)
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    match.RegisterPointWon(0);
                }
                for (int j = 0; j < 4; j++)
                {
                    match.RegisterPointWon(1);
                }

            }
            match.IsMatchOver.Should().BeFalse();
        }

        [Fact]
        public void TieBreakerTest_ShouldSimulateTieBreaker()
        {
            var _player1 = new Player("Player 1");
            var _player2 = new Player("Player 2");
            var match = new Match(_player1, _player2);

            // Simulate reaching a tie-breaker (6-6 in games)
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    match.RegisterPointWon(0);
                }
                for (int j = 0; j < 4; j++)
                {
                    match.RegisterPointWon(1);
                }

            }

            match.RegisterPointWon(1);

            match.GetCurrentGameScore.Should().Be("(TieBreaker) 0-1");
            match.GetCurrentSetScore.Should().Be("6-6");
            match.GetCurrentMatchScore.Should().Be("0-0");

            for (int j = 0; j < 6; j++)
            {
                match.RegisterPointWon(0);
                match.RegisterPointWon(1);
            }

            match.GetCurrentGameScore.Should().Be("(TieBreaker) 6-7");
            match.GetCurrentSetScore.Should().Be("6-6");
            match.GetCurrentMatchScore.Should().Be("0-0");

            // Break TieBreaker
            match.RegisterPointWon(1);

            match.GetCurrentGameScore.Should().Be("0-0");
            match.GetCurrentSetScore.Should().Be("0-0");
            match.GetCurrentMatchScore.Should().Be("0-1");
        }

        [Fact]
        public void AdvantageTests_ShouldSimulateAdvantage()
        {
            var _player1 = new Player("Player 1");
            var _player2 = new Player("Player 2");
            var match = new Match(_player1, _player2);

            for (int j = 0; j < 4; j++)
            {
                match.RegisterPointWon(0);
                match.RegisterPointWon(1);
            }
            match.GetCurrentGameScore.Should().Be("Deuce");
            match.RegisterPointWon(0);
            match.GetCurrentGameScore.Should().Be("Advantage Player 1");

            match.RegisterPointWon(1);
            match.GetCurrentGameScore.Should().Be("Deuce");

            match.RegisterPointWon(1);
            match.GetCurrentGameScore.Should().Be("Advantage Player 2");

            match.RegisterPointWon(1);
            match.GetCurrentGameScore.Should().Be("0-0");
            match.GetCurrentSetScore.Should().Be("0-1");

        }

        private void SimulateSetWin(Match match, Player winner)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    match.RegisterPointWon(winner == _winningPlayer1 ? 0 : 1);
                    match.RegisterPointWon(winner == _winningPlayer1 ? 0 : 1);
                    match.RegisterPointWon(winner == _winningPlayer1 ? 0 : 1);
                    match.RegisterPointWon(winner == _winningPlayer1 ? 0 : 1);
                }
            }
        }
    }
}
