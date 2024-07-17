namespace TicTacToe {
    class Bot : Player {
        private static AIChance difficulty;

        // for caching
        private Random rnd;
        private List<int> posLeft;

        // constructor
        public Bot(char letter) {
            this.SetLetter(letter);
            this.rnd = new Random();
            this.posLeft = new List<int>();

            // creating UI for making the chances
            HashSet<string> choices = new HashSet<string>{"easy", "medium", "hard", "impossible"};
            string choice = IOManager.RestrictedChoice("Enter \"quit\" to end the game. Enter the difficulty: \"Easy\", \"Medium\", \"Hard\", or \"Impossible\"", choices);

            // getting the relevant difficulty set up
            switch (choice) {
                case "easy":
                    difficulty = AIChance.Easy;
                    break;
                case "medium":
                    difficulty = AIChance.Medium;
                    break;
                case "hard":
                    difficulty = AIChance.Hard;
                    break;
                case "impossible":
                    difficulty = AIChance.Impossible;
                    break;
                default:
                    // replace with exceptions later
                    Console.WriteLine("Something went wrong");
                    break;
            }
        }

        // making the move
        public override MoveState[] MakeMove(MoveState[] board) {
            // random number from 1 to 100 (inclusively)
            int chance = rnd.Next(1, 101);

            // if it's less than or equal to the associated constant, use AI
            int index = -1;
            if (chance <= (int)difficulty) {
                (index, int _) = MiniMax(board, 0, Int32.MinValue, Int32.MaxValue, player, player);

            // otherwise, guess the move randomly
            } else {
                index = posLeft[rnd.Next(posLeft.Count)];
            }

            // applying the move
            board[index] = MoveState.Bot;
            posLeft.Remove(index);

            return board;
        }

        // minimax algorithm to choose (and return) the best move for a given board position
        // with alpha-beta pruning applied
        private static (int, int) MiniMax(MoveState[] board, int depth, int alpha, int beta, MoveState player, MoveState initPlayer) {
            // if last move won, evaluate current position
            if (HasWon(board)) {
                // evaluate inversely proportional to depth
                // i.e. higher depth -> lower eval, lower depth -> higher eval
                int eval = posLeft.Count + 1 - depth;

                // if the current player is the initial player,
                // it means the last move was played by the opponent so we must evaluate it negatively
                if (player == initPlayer) eval *= -1;
                return (-1, eval);
            }

            // draw
            if (depth == posLeft.Count) return (-1, 0);

            // determine who is the next player
            MoveState nextPlayer = MoveState.Player;
            if (player == MoveState.Player) nextPlayer = MoveState.Bot;

            // calculate max eval i.e best move for the player we want to win
            if (player == initPlayer) {
                int move = -1;

                // applying alpha-beta pruning such that it stops when beta <= alpha
                for (int i = 0; i < posLeft.Count && beta > alpha; i++) {
                    // skip if visited

                    if (board[posLeft[i]] != MoveState.Unused) continue;

                    // set as visited
                    board[posLeft[i]] = player;

                    // find max
                    (int _, int currEval) = MiniMax(board, depth + 1, alpha, beta, nextPlayer, initPlayer);
                    if (currEval > alpha) {
                        alpha = currEval;
                        move = posLeft[i];
                    }

                    // set as unvisited
                    board[posLeft[i]] = MoveState.Unused;
                }
                return (move, alpha);

            // calculate min eval i.e. best move for the player we want to lose
            } else {
                int move = -1;

                // applying alpha-beta pruning such that it stops when beta <= alpha
                for (int i = 0; i < posLeft.Count && beta > alpha; i++) {
                    // skip if visited
                    if (board[posLeft[i]] != MoveState.Unused) continue;

                    // set as visited
                    board[posLeft[i]] = player;

                    // find min
                    (int _, int currEval) = MiniMax(board, depth + 1, alpha, beta, nextPlayer, initPlayer);
                    if (currEval < beta) {
                        beta = currEval;
                        move = posLeft[i];
                    }

                    // set as unvisited
                    board[posLeft[i]] = MoveState.Unused;
                }
                return (move, beta);
            }
        }
    }
}