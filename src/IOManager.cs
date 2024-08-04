namespace TicTacToe {
    public class IOManager {

        // retrieving input from console
        private static string RetrieveInput(string msg) {
            Console.WriteLine("Enter \"quit\" to end the game. {0}", msg);
            string input = "";
            while (input == "") {
                Console.Write("> ");
                input = Console.ReadLine() ?? throw new IOException("Invalid input provided");
            }
            input = input.ToLower();
            if (input == "quit") throw new QuitGameException("Exiting program...");
            return input.ToLower();
        }

        // parses a number from the input
        public static int RetrieveNum(string msg, int start, int end) {
            // repeat until valid number provided
            int num = Int32.MinValue;
            bool found = false;
            while (!found) {
                // get input from console
                string input = RetrieveInput(msg);

                // parsing the input to ensure it is a valid number in the given range
                if (!int.TryParse(input, out num)) {
                    Console.WriteLine("Invalid input provided. Please enter a number between {0} and {1}", start, end);
                    continue;
                }
                if (num < start || num > end) {
                    Console.WriteLine("Number outwith range ({0}-{1})", start, end);
                    continue;
                }
                found = true;
            }
            return num;
        }

        // restricted input that can be provided
        // choices provided should be lower case
        public static string RestrictedChoice(string msg, HashSet<string> choices) {
            string res = "";
            while (res == "") {
                string input = RetrieveInput(msg + ": " + String.Join(", ", choices));

                // setting the result if valid
                if (choices.Contains(input)) {
                    res = input;

                // else, provide an error
                } else {
                    res = "";
                    Console.WriteLine("Input did not match provided choices: {0}", String.Join(", ", choices));
                }
            }
            return res;
        }

        // goes through the board to display all its details
        public static void DisplayBoard(MoveState[] board) {
            // for all board points
            for (int i = 0; i < board.Length; i++) {
                // output individual board point
                Console.Write(board[i] switch {
                    MoveState.First  => 'X',
                    MoveState.Second => 'O',
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
    }
}