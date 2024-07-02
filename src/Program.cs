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
                if (
                    // horizontal win
                    (board[0] == MoveState.Player && board[1] == MoveState.Player && board[2] == MoveState.Player) ||
                    (board[3] == MoveState.Player && board[4] == MoveState.Player && board[5] == MoveState.Player) ||
                    (board[6] == MoveState.Player && board[7] == MoveState.Player && board[8] == MoveState.Player) ||

                    // diagonal wins
                    (board[0] == MoveState.Player && board[4] == MoveState.Player && board[8] == MoveState.Player) ||
                    (board[2] == MoveState.Player && board[4] == MoveState.Player && board[6] == MoveState.Player) ||

                    // vertical wins
                    (board[0] == MoveState.Player && board[3] == MoveState.Player && board[6] == MoveState.Player) ||
                    (board[1] == MoveState.Player && board[4] == MoveState.Player && board[7] == MoveState.Player) ||
                    (board[2] == MoveState.Player && board[5] == MoveState.Player && board[8] == MoveState.Player)
                ) {
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
                int index = posLeft[rnd.Next(posLeft.Count)];
                board[index] = MoveState.Bot;
                posLeft.Remove(index);

                // display game state after bot's turn
                Console.WriteLine("After the bot's turn, the game is:");
                DisplayBoard(board);

                // if the bot won
                if (
                    // horizontal win
                    (board[0] == MoveState.Bot && board[1] == MoveState.Bot && board[2] == MoveState.Bot) ||
                    (board[3] == MoveState.Bot && board[4] == MoveState.Bot && board[5] == MoveState.Bot) ||
                    (board[6] == MoveState.Bot && board[7] == MoveState.Bot && board[8] == MoveState.Bot) ||

                    // diagonal wins
                    (board[0] == MoveState.Bot && board[4] == MoveState.Bot && board[8] == MoveState.Bot) ||
                    (board[2] == MoveState.Bot && board[4] == MoveState.Bot && board[6] == MoveState.Bot) ||

                    // vertical wins
                    (board[0] == MoveState.Bot && board[3] == MoveState.Bot && board[6] == MoveState.Bot) ||
                    (board[1] == MoveState.Bot && board[4] == MoveState.Bot && board[7] == MoveState.Bot) ||
                    (board[2] == MoveState.Bot && board[5] == MoveState.Bot && board[8] == MoveState.Bot)
                ) {
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
    }
}