namespace Domain.Models
{
    public class Match
    {
        public Player Player1 { get; }
        public Player Player2 { get; }
        public List<Set> Sets { get; private set; }
        private Set _currentSet;
        private Game _currentGame;

        public Match(Player player1, Player player2)
        {
            Player1 = player1;
            Player2 = player2;
            Sets = new List<Set>();
            StartNewSet();
        }

        public void StartNewSet()
        {
            _currentSet = new Set(Player1, Player2);
            Sets.Add(_currentSet);
            StartNewGame();
        }

        public void StartNewGame()
        {
            _currentGame = new Game(Player1, Player2, _currentSet.IsTiebreakerActive);
        }

        public bool RegisterPointWon(int playerIndex)
        {
            bool _isMatchOver = IsMatchOver;
            if (!_isMatchOver)
            {
                var player = playerIndex == 0 ? Player1 : Player2;
                _currentGame.PointWonBy(player);

                if (_currentGame.IsNewGame)
                {
                    _currentSet.AddGame(_currentGame);
                    StartNewGame();
                    _isMatchOver = IsMatchOver;
                }
            }
            return _isMatchOver;
        }

        public string GetCurrentGameScore => _currentGame.GetScore();

        public string GetCurrentSetScore => _currentSet.GetScore();

        public string GetCurrentMatchScore => $"{Player1.SetsWon}-{Player2.SetsWon}";

        public bool IsMatchOver => Player1.SetsWon == 3 || Player2.SetsWon == 3;

        public Player GetMatchWinner => Player1.SetsWon > Player2.SetsWon ? Player1 : Player2;
    }
}
