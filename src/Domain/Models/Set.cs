namespace Domain.Models
{
    public class Set
    {
        private readonly Player _player1;
        private readonly Player _player2;
        public List<Game> Games { get; private set; }

        public Set(Player player1, Player player2)
        {
            _player1 = player1;
            _player2 = player2;
            Games = new List<Game>();
        }

        public void AddGame(Game game)
        {
            Games.Add(game);
            if (IsSetWon())
                AwardSet();
        }

        private bool IsSetWon()
        {
            return (_player1.GamesWon >= 6 && _player1.GamesWon >= _player2.GamesWon + 2) ||
                   (_player2.GamesWon >= 6 && _player2.GamesWon >= _player1.GamesWon + 2);
        }

        private void AwardSet()
        {
            if (_player1.GamesWon > _player2.GamesWon)
                _player1.RegisterSetWon();
            else
                _player2.RegisterSetWon();
            ResetPlayerGames();
        }

        private void ResetPlayerGames()
        {
            _player1.ResetGames();
            _player2.ResetGames();
        }

        public string GetScore() => $"{_player1.GamesWon}-{_player2.GamesWon}";
    }
}
