namespace TennisMatchService.Logger
{
    using Microsoft.Extensions.Logging;

    public class MatchLogger
    {
        private readonly ILogger<MatchLogger> _logger;

        public MatchLogger(ILogger<MatchLogger> logger)
        {
            _logger = logger;
        }
        public void LogNewMatchStart(string player1, string player2)
        {
            LogMessage($"\n========== New Match started: {player1} vs {player2} ==========");
        }

        public void LogMessage(string message)
        {
            _logger.LogInformation(message);
        }

        public void LogLineBreak()
        {
            LogMessage("----\n");
        }
    }
}
