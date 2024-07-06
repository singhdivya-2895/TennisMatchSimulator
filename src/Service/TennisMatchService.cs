using Domain.Models;
using TennisMatchService.Logger;

namespace TennisMatchService
{
    public class TennisMatchService : ITennisMatchService
    {
        private readonly MatchLogger _logger;
        public TennisMatchService(MatchLogger logger)
        {
            _logger = logger;
        }

        public void PlayMatch(string[] players, int[] points)
        {
            var player1 = new Player(players[0]);
            var player2 = new Player(players[1]);
            var match = new Match(player1, player2);

            _logger.LogNewMatchStart(player1.Name, player2.Name);
            foreach (var point in points)
            {
                match.PointWonBy(point);
                _logger.LogMessage($"Point won by: {(point == 0 ? player1.Name : player2.Name)}");
                _logger.LogMessage($"Current Game Score: {match.GetCurrentGameScore()}");
                _logger.LogMessage($"Current Set Score: {match.GetCurrentSetScore()}");
                _logger.LogMessage($"Current Match Score: {match.GetCurrentMatchScore()}");
                _logger.LogLineBreak();

                if (match.IsMatchOver())
                    break;
            }

            var (winner, isMatchOver) = match.GetMatchWinner();
            if (isMatchOver)
            {
                _logger.LogMessage($"Final Match Winner: {winner.Name}");
            }
            else
            {
                _logger.LogMessage($"No Result");
            }
        }
    }
}
