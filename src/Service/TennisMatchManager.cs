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
            try
            {
                var player1 = new Player(players[0]);
                var player2 = new Player(players[1]);
                var match = new Match(player1, player2);
                bool isMatchOver;
                _logger.LogNewMatchStart(player1.Name, player2.Name);
                int currentPoint = -1;
                for (int index = 0; index < points.Length; index++)
                {
                    currentPoint = points[index];
                    isMatchOver = match.RegisterPointWon(currentPoint);
                    _logger.LogMessage($"Point {index}: {(currentPoint == 0 ? player1.Name : player2.Name)} wins");
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
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
