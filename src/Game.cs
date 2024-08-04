namespace TicTacToe {
    class Game {
        // both players in the game
        private Player[] players;

        // the current board state
        private MoveState[] board;

        // the unfilled positions left
        private List<int> posLeft;
        public Game(Player first, Player second) {
            // exceptions in case invalid data is provided
            if (first == second) throw new EqualPlayerObjectException("Player objects passed to Game constructor reference the same memory location. They should be unrelated.");
            if (first.GetPlayer() == second.GetPlayer()) throw new EqualPlayerMoveStateException("Players cannot have the same move states.");

            // initialising data and providing both players
            players = new Player[]{first, second};
            board = new MoveState[Constants.BoardHeight * Constants.BoardLength];
            posLeft = new List<int>();
        }
        public void Play() {
            // reset everything
            Reset();

            // play the game until it ends
            int movesPlayed = 0;
            while (movesPlayed < board.Length) {
                // output current board state
                Console.WriteLine("The current state of the game is:");
                IOManager.DisplayBoard(board);

                // have the current player make a move
                int currPlayer = movesPlayed % players.Length;
                players[currPlayer].MakeMove(board);

                // breaks when movesPlayed <= 8 if there is a winner
                if (players[currPlayer].HasWon(board)) break;

                // output which player made a move
                Console.WriteLine("Player {0} has made a move.", players[currPlayer].GetLetter());

                // move on to the next move
                movesPlayed++;
            }

            // output current board state at the end of the game
            Console.WriteLine("The current state of the game is:");
            IOManager.DisplayBoard(board);

            // if all moves were played without a winner, it's a draw
            if (movesPlayed == board.Length) {
                Console.WriteLine("Game ended in a draw");

            // otherwise, it's a win
            } else {
                Console.WriteLine("Player {0} has won!", players[movesPlayed % players.Length].GetLetter());
            }
        }

        // resets all properties of the game
        // and links them (as needed) to the players
        private void Reset() {
            board = new MoveState[Constants.BoardLength * Constants.BoardHeight];
            posLeft = Enumerable.Range(0, board.Length).ToList();
            foreach (Player player in players) {
                player.SetPosLeft(posLeft);
            }
        }
    }
}