namespace TicTacToe {
    class Bot : Player {
        // what difficulty the bot is set to
        private static AIChance difficulty;

        // for caching
        // i.e. to avoid creating the same object several times unnecessarily
        private static Random rnd = new Random();

        // constructor
        public Bot(MoveState player) {
            this.SetPlayer(player);

            // creating UI for making the chances
            HashSet<string> choices = new HashSet<string>{"easy", "medium", "hard", "impossible"};
            string choice = IOManager.RestrictedChoice("Enter the difficulty", choices);

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
                    throw new UnexpectedEnumValueException("Was not provided a valid AIChance enum value.");
            }
        }

        // making the move
        public override MoveState[] MakeMove(MoveState[] board) {
            // random number from 1 to 100 (inclusively)
            int chance = rnd.Next(1, 101);

            // if it's less than or equal to the associated constant, use AI i.e. minimax algo
            int index = -1;
            if (chance <= (int)difficulty) {
                (index, int _) = MiniMax(board, 0, Int32.MinValue, Int32.MaxValue, this.GetPlayer(), this.GetPlayer());

            // otherwise, guess the move randomly
            } else {
                index = GetRandomPosLeft(rnd);
            }

            // applying the move
            board[index] = this.GetPlayer();
            PosLeftRemove(index);

            return board;
        }

        // minimax algorithm to choose (and return) the best move for a given board position
        // with alpha-beta pruning applied
        private (int, int) MiniMax(MoveState[] board, int depth, int alpha, int beta, MoveState player, MoveState initPlayer) {
            // if last move won, evaluate current position
            if (HasWon(board)) {
                // evaluate inversely proportional to depth
                // i.e. higher depth -> lower eval, lower depth -> higher eval
                int eval = CountAvailablePos() + 1 - depth;

                // if the current player is the initial player,
                // it means the last move was played by the opponent so we must evaluate it negatively
                if (player == initPlayer) eval *= -1;
                return (-1, eval);
            }

            // draw
            if (depth == CountAvailablePos()) return (-1, 0);

            // determine who is the next player
            MoveState nextPlayer = MoveState.First;
            if (player == MoveState.First) nextPlayer = MoveState.Second;

            // calculate max eval i.e best move for the player we want to win
            if (player == initPlayer) {
                int move = -1;

                // applying alpha-beta pruning such that it stops when beta <= alpha
                for (int i = 0; i < CountAvailablePos() && beta > alpha; i++) {
                    // skip if visited

                    if (board[GetPosition(i)] != MoveState.Unused) continue;

                    // set as visited
                    board[GetPosition(i)] = player;

                    // find max
                    (int _, int currEval) = MiniMax(board, depth + 1, alpha, beta, nextPlayer, initPlayer);
                    if (currEval > alpha) {
                        alpha = currEval;
                        move = GetPosition(i);
                    }

                    // set as unvisited
                    board[GetPosition(i)] = MoveState.Unused;
                }
                return (move, alpha);

            // calculate min eval i.e. best move for the player we want to lose
            } else {
                int move = -1;

                // applying alpha-beta pruning such that it stops when beta <= alpha
                for (int i = 0; i < CountAvailablePos() && beta > alpha; i++) {
                    // skip if visited
                    if (board[GetPosition(i)] != MoveState.Unused) continue;

                    // set as visited
                    board[GetPosition(i)] = player;

                    // find min
                    (int _, int currEval) = MiniMax(board, depth + 1, alpha, beta, nextPlayer, initPlayer);
                    if (currEval < beta) {
                        beta = currEval;
                        move = GetPosition(i);
                    }

                    // set as unvisited
                    board[GetPosition(i)] = MoveState.Unused;
                }
                return (move, beta);
            }
        }
    }
}