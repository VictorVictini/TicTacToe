namespace TicTacToe {
    class Game {
        private Player[] players;
        private int turn;
        private MoveState[] board;
        private List<int> posLeft;
        public Game(Player first, Player second) {
            // make custom exceptions later
            if (first == second) throw new EqualPlayerObjectException("Player objects passed to Game constructor reference the same memory location. They should be unrelated.");
            players = new Player[]{first, second};
            turn = 0;
            board = new MoveState[9];
            posLeft = new List<int>();
        }
        public void Play() {
            Reset();
            int movesPlayed = 0;
            while (movesPlayed < board.Length) {
                Console.WriteLine("The current state of the game is:");
                IOManager.DisplayBoard(board);
                players[turn].MakeMove(board);
                if (players[turn].HasWon(board)) break;
                turn = (turn + 1) % players.Length;
                movesPlayed++;
            }
            if (movesPlayed == board.Length) {
                Console.WriteLine("Game ended in a draw");
            } else {
                Console.WriteLine("Player {0} has won!", players[turn].GetLetter());
            }
        }
        private void Reset() {
            turn = 0;
            board = new MoveState[9];
            posLeft = Enumerable.Range(0, 9).ToList();
            foreach (Player player in players) {
                player.SetPosLeft(posLeft);
            }
        }
    }
}