namespace TicTacToe {
    public class Program {
        // caching the data structure to reuse
        static Random rnd = new Random();
        static List<int> posLeft = new List<int>();

        // program runs from here
        private static void Main() {
            // input is what we get from the user
            string input = "";

            // set up a new board and display it
            Console.WriteLine("Starting game...");
            MoveState[] board = new MoveState[9]; // 3x3 board = 9 length
            ResetBoard(board);
            DisplayBoard(board);

            // actual game
            // runs until "quit" is provided
            while (true) {
                // retrieving the input
                try {
                    input = RetrieveInput();
                } catch (IOException e) {
                    Console.WriteLine(e.Message);
                    continue;
                }

                // ends the game if "quit" is provided
                if (input.ToLower() == "quit") break;

                // ignore 'empty' input
                if (input == "") continue;

                // parsing the input to ensure it is a valid number in the given range
                int num = 0;
                if (!int.TryParse(input, out num)) {
                    Console.WriteLine("Invalid input provided. Not a number or \"quit\"");
                    continue;
                }
                if (num < 1 || num > 9) {
                    Console.WriteLine("Number outwith range (1-9)");
                    continue;
                }

                // gets the relevant grid position and checks it's currently unvisited
                if (board[num - 1] != MoveState.Unused) {
                    Console.WriteLine("Invalid position chosen");
                    continue;
                }

                // visiting the grid position
                board[num - 1] = MoveState.Player;
                posLeft.Remove(num - 1);

                // displays game state after your move
                Console.WriteLine("After your move, the game state is:");
                DisplayBoard(board);

                // check if player won
                if (HasWon(board)) {
                    Console.WriteLine("Congrats! You won!");

                    // reset the game state
                    Console.WriteLine("Starting new game...");
                    ResetBoard(board);
                    DisplayBoard(board);
                    continue;
                }

                // if there are no unvisited positions left, it is a draw
                if (posLeft.Count == 0) {
                    Console.WriteLine("Game ended in a draw");

                    // reset the game state
                    Console.WriteLine("Starting new game...");
                    ResetBoard(board);
                    DisplayBoard(board);
                    continue;
                }

                // set a random unvisited position as visited by the bot player
                /*int index = posLeft[rnd.Next(posLeft.Count)];*/
                int index = CalculateBestMove(board, MoveState.Bot);
                board[index] = MoveState.Bot;
                posLeft.Remove(index);

                // display game state after bot's turn
                Console.WriteLine("After the bot's turn, the game is:");
                DisplayBoard(board);

                // if the bot won
                if (HasWon(board)) {
                    Console.WriteLine("Bzz! You lost!");

                    // reset the game state
                    Console.WriteLine("Starting new game...");
                    ResetBoard(board);
                    DisplayBoard(board);
                    continue;
                }
            }
        }

        // goes through the board to display all its details
        private static void DisplayBoard(MoveState[] board) {
            // for all board points
            for (int i = 0; i < board.Length; i++) {
                // output individual board point
                Console.Write(board[i] switch {
                    MoveState.Player => 'X',
                    MoveState.Bot    => 'O',
                    _                => (char)(i + '1'),
                });

                // every third number, we output a new line instead of pipelines in-between
                if ((i + 1) % 3 == 0) {
                    Console.WriteLine();
                } else {
                    Console.Write(" | ");
                }
            }
        }

        // retrieving input from console
        private static string RetrieveInput() {
            Console.WriteLine("Enter \"quit\" to end the game. Enter the relevant number for the box, to enter the position you would like to play. You are playing as X, the first player.");
            Console.Write("> ");
            string? input = Console.ReadLine();
            return input ?? throw new IOException("Invalid input provided");
        }

        // resets game state
        private static void ResetBoard(MoveState[] board) {
            posLeft.Clear();
            for (int i = 0; i < board.Length; i++) {
                posLeft.Add(i);
                board[i] = MoveState.Unused;
            }
        }
        // determines if there is a winning position
        // context can determine who won, so we do not need to return that
        private static bool HasWon(MoveState[] board) {
            return  // horizontal win
                    (board[0] == board[1] && board[1] == board[2] && board[0] != MoveState.Unused) ||
                    (board[3] == board[4] && board[4] == board[5] && board[3] != MoveState.Unused) ||
                    (board[6] == board[7] && board[7] == board[8] && board[6] != MoveState.Unused) ||

                    // diagonal wins
                    (board[0] == board[4] && board[4] == board[8] && board[0] != MoveState.Unused) ||
                    (board[2] == board[4] && board[4] == board[6] && board[2] != MoveState.Unused) ||

                    // vertical wins
                    (board[0] == board[3] && board[3] == board[6] && board[0] != MoveState.Unused) ||
                    (board[1] == board[4] && board[4] == board[7] && board[1] != MoveState.Unused) ||
                    (board[2] == board[5] && board[5] == board[8] && board[2] != MoveState.Unused);
        }
        // calculates the best move for the given player
        private static int CalculateBestMove(MoveState[] board, MoveState player) {
            (int index, int _) = MiniMax(board, 0, player, player);
            return index;
        }

        // minimax algorithm to choose (and return) the best move for a given board position
        private static (int, int) MiniMax(MoveState[] board, int depth, MoveState player, MoveState initPlayer) {
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
                int maxEval = Int32.MinValue;
                int move = -1;
                for (int i = 0; i < posLeft.Count; i++) {
                    // skip if visited
                    if (board[posLeft[i]] != MoveState.Unused) continue;

                    // set as visited
                    board[posLeft[i]] = player;

                    // find max
                    (int _, int currEval) = MiniMax(board, depth + 1, nextPlayer, initPlayer);
                    if (currEval > maxEval) {
                        maxEval = currEval;
                        move = posLeft[i];
                    }

                    // set as unvisited
                    board[posLeft[i]] = MoveState.Unused;
                }
                return (move, maxEval);

            // calculate min eval i.e. best move for the player we want to lose
            } else {
                int minEval = Int32.MaxValue;
                int move = -1;
                for (int i = 0; i < posLeft.Count; i++) {
                    // skip if visited
                    if (board[posLeft[i]] != MoveState.Unused) continue;

                    // set as visited
                    board[posLeft[i]] = player;

                    // find min
                    (int _, int currEval) = MiniMax(board, depth + 1, nextPlayer, initPlayer);
                    if (currEval < minEval) {
                        minEval = currEval;
                        move = posLeft[i];
                    }

                    // set as unvisited
                    board[posLeft[i]] = MoveState.Unused;
                }
                return (move, minEval);
            }
        }
    }
}