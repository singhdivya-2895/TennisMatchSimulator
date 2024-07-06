namespace Domain.Models
{
    public class Player
    {
        public string Name { get; private set; }
        public int Points { get; private set; }
        public int GamesWon { get; private set; }
        public int SetsWon { get; private set; }

        public Player(string name)
        {
            Name = name;
            Points = 0;
            GamesWon = 0;
            SetsWon = 0;
        }

        public void RegisterPointWon()
        {
            Points++;
        }

        public void RegisterGameWon()
        {
            GamesWon++;
        }

        public void RegisterSetWon()
        {
            SetsWon++;
        }

        public void ResetGames()
        {
            GamesWon = 0;
            ResetPoints();
        }

        public void ResetPoints()
        {
            Points = 0;
        }
    }
}
