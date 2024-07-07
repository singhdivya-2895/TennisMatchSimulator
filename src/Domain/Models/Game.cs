namespace Domain.Models
{
    public class Game
    {
        private readonly Dictionary<int, string> _pointMap = new()
        {
            { 0, "0" }, { 1, "15" }, { 2, "30" }, { 3, "40" }
        };

        public Player Player1 { get; }
        public Player Player2 { get; }
        private bool IsTiebreaker { get; set; }

        public Game(Player player1, Player player2, bool isTiebreaker = false)
        {
            Player1 = player1;
            Player2 = player2;
            IsTiebreaker = isTiebreaker;
        }

        public void PointWonBy(Player player)
        {
            player.RegisterPointWon();

            if (IsGameWon())
            {
                player.RegisterGameWon();
                ResetGame();
            }
        }

        public void ResetGame()
        {
            Player1.ResetPoints();
            Player2.ResetPoints();
            IsTiebreaker = false;
        }

        private bool IsGameWon()
        {
            if (IsTiebreaker)
            {
                return (Player1.Points >= 7 && Player1.Points >= Player2.Points + 2) ||
                       (Player2.Points >= 7 && Player2.Points >= Player1.Points + 2);
            }
            else
            {
                return (Player1.Points >= 4 && Player1.Points >= Player2.Points + 2) ||
                       (Player2.Points >= 4 && Player2.Points >= Player1.Points + 2);
            }
        }

        public string GetScore()
        {
            if (IsTiebreaker)
            {
                return $"(TieBreaker) {Player1.Points}-{Player2.Points}";
            }

            if (Player1.Points >= 3 && Player2.Points >= 3)
            {
                if (Player1.Points == Player2.Points)
                {
                    return "Deuce";
                }
                if (Player1.Points == Player2.Points + 1)
                {
                    return "Advantage " + Player1.Name;
                }
                if (Player2.Points == Player1.Points + 1)
                {
                    return "Advantage " + Player2.Name;
                }
            }

            return $"{_pointMap[Player1.Points]}-{_pointMap[Player2.Points]}";
        }

        public bool IsNewGame => Player1.Points == 0 && Player2.Points == 0;
    }
}
