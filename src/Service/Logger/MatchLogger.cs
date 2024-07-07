using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
namespace TennisMatchService.Logger
{
    [ExcludeFromCodeCoverage]
    public class MatchLogger : IMatchLogger
    {
        private readonly ILogger _logger;

        public MatchLogger(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("TennisMatchLogger");
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
        public void LogError(string message)
        {
            _logger.LogError(message);
        }
    }
}
