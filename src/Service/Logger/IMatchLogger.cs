namespace TennisMatchService.Logger
{
    public interface IMatchLogger
    {
        void LogNewMatchStart(string player1, string player2);
        void LogMessage(string message);
        void LogLineBreak();
        void LogError(string message);
    }
}