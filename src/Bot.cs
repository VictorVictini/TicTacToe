namespace TicTacToe {
    class Bot : Player {
        private static AIChance difficulty;

        // for caching
        private static Random rnd = new Random();

        // constructor
        public Bot(MoveState player, char letter) {
            this.SetPlayer(player);
            this.SetLetter(letter);

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
                (index, int _) = MiniMax(board, 0, Int32.MinValue, Int32.MaxValue, this.GetPlayer(), this.GetPlayer());

            // otherwise, guess the move randomly
            } else {
                index = GetPosLeft()[rnd.Next(GetPosLeft().Count)];
            }

            // applying the move
            board[index] = this.GetPlayer();
            GetPosLeft().Remove(index);

            return board;
        }

        // minimax algorithm to choose (and return) the best move for a given board position
        // with alpha-beta pruning applied
        private (int, int) MiniMax(MoveState[] board, int depth, int alpha, int beta, MoveState player, MoveState initPlayer) {
            // if last move won, evaluate current position
            if (HasWon(board)) {
                // evaluate inversely proportional to depth
                // i.e. higher depth -> lower eval, lower depth -> higher eval
                int eval = GetPosLeft().Count + 1 - depth;

                // if the current player is the initial player,
                // it means the last move was played by the opponent so we must evaluate it negatively
                if (player == initPlayer) eval *= -1;
                return (-1, eval);
            }

            // draw
            if (depth == GetPosLeft().Count) return (-1, 0);

            // determine who is the next player
            MoveState nextPlayer = MoveState.First;
            if (player == MoveState.First) nextPlayer = MoveState.Second;

            // calculate max eval i.e best move for the player we want to win
            if (player == initPlayer) {
                int move = -1;

                // applying alpha-beta pruning such that it stops when beta <= alpha
                for (int i = 0; i < GetPosLeft().Count && beta > alpha; i++) {
                    // skip if visited

                    if (board[GetPosLeft()[i]] != MoveState.Unused) continue;

                    // set as visited
                    board[GetPosLeft()[i]] = player;

                    // find max
                    (int _, int currEval) = MiniMax(board, depth + 1, alpha, beta, nextPlayer, initPlayer);
                    if (currEval > alpha) {
                        alpha = currEval;
                        move = GetPosLeft()[i];
                    }

                    // set as unvisited
                    board[GetPosLeft()[i]] = MoveState.Unused;
                }
                return (move, alpha);

            // calculate min eval i.e. best move for the player we want to lose
            } else {
                int move = -1;

                // applying alpha-beta pruning such that it stops when beta <= alpha
                for (int i = 0; i < GetPosLeft().Count && beta > alpha; i++) {
                    // skip if visited
                    if (board[GetPosLeft()[i]] != MoveState.Unused) continue;

                    // set as visited
                    board[GetPosLeft()[i]] = player;

                    // find min
                    (int _, int currEval) = MiniMax(board, depth + 1, alpha, beta, nextPlayer, initPlayer);
                    if (currEval < beta) {
                        beta = currEval;
                        move = GetPosLeft()[i];
                    }

                    // set as unvisited
                    board[GetPosLeft()[i]] = MoveState.Unused;
                }
                return (move, beta);
            }
        }
    }
}