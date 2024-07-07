using Domain.Models;
using TennisMatchService.Logger;

namespace TennisMatchService
{
    public class TennisMatchManager : ITennisMatchService
    {
        private readonly IMatchLogger _logger;
        public TennisMatchManager(IMatchLogger logger)
        {
            _logger = logger;
        }

        public void PlayMatch(string[] players, int[] points)
        {
            var player1 = new Player(players[0]);
            var player2 = new Player(players[1]);
            var match = new Match(player1, player2);
            bool isMatchOver;
            _logger.LogNewMatchStart(player1.Name, player2.Name);

            foreach (var point in points)
            {
                isMatchOver = match.RegisterPointWon(point);
                _logger.LogMessage($"Point won by: {(point == 0 ? player1.Name : player2.Name)}");
                _logger.LogMessage($"Current Game Score: {match.GetCurrentGameScore}");
                _logger.LogMessage($"Current Set Score: {match.GetCurrentSetScore}");
                _logger.LogMessage($"Current Match Score: {match.GetCurrentMatchScore}");
                _logger.LogLineBreak();

                if (isMatchOver)
                {
                    _logger.LogMessage($"Final Match Winner: {match.GetMatchWinner.Name}");
                    return;
                }
            }
            _logger.LogMessage($"No Result");
        }
    }
}
