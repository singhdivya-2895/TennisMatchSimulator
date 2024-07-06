namespace Domain.Models
{
    public class Game
    {
        private readonly Dictionary<int, string> _pointMap = new ()
        {
            { 0, "0" }, { 1, "15" }, { 2, "30" }, { 3, "40" }
        };

        public Player Player1 { get; }
        public Player Player2 { get; }

        public Game(Player player1, Player player2)
        {
            Player1 = player1;
            Player2 = player2;
        }

        public void PointWonBy(Player player)
        {
            player.RegisterPointWon();

            if (IsGameWon())
            {
                player.RegisterGameWon();
                ResetPlayerPoints();
            }
        }

        public void ResetPlayerPoints()
        {
            Player1.ResetPoints();
            Player2.ResetPoints();
        }

        private bool IsGameWon()
        {
            if (Player1.Points >= 4 && Player1.Points >= Player2.Points + 2)
                return true;
            if (Player2.Points >= 4 && Player2.Points >= Player1.Points + 2)
                return true;
            return false;
        }

        public string GetScore()
        {
            if (Player1.Points >= 3 && Player2.Points >= 3)
            {
                if (Player1.Points == Player2.Points)
                    return "Deuce";
                if (Player1.Points == Player2.Points + 1)
                    return "Advantage " + Player1.Name;
                if (Player2.Points == Player1.Points + 1)
                    return "Advantage " + Player2.Name;
            }

            return $"{_pointMap[Player1.Points]}-{_pointMap[Player2.Points]}";
        }

        public bool IsNewGame => Player1.Points == 0 && Player2.Points == 0;
    }
}
