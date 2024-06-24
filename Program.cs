public class Program {
    // caching the data structure to reuse
    static Random rnd = new Random();

    // program runs from here
    private static void Main() {
        // input is what we get from the user
        string input = "";

        // game state is the tictactoe game's state
        // -1 indicates unused space
        // 1 indicates a player placed their move there
        // 0 indicates the bot placed its move there
        int[,] gameState = {
            {-1, -1, -1},
            {-1, -1, -1},
            {-1, -1, -1}
        };

        // actual game
        // runs until "quit" is provided
        while (true) {
            // displays initial state
            Console.WriteLine("The current game state is:");
            DisplayGameState(gameState);

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
            int left = (int)Math.Floor((double)(num - 1) / 3);
            int right = (num - 1) % 3;
            if (gameState[left, right] != -1) {
                Console.WriteLine("Invalid position chosen");
                continue;
            }

            // visiting the grid position
            gameState[left, right] = 1;

            // displays game state after your move
            Console.WriteLine("After your move, the game state is: ");
            DisplayGameState(gameState);

            // check if player won
            if (
                (gameState[0, 0] == 1 && gameState[0, 1] == 1 && gameState[0, 2] == 1) ||
                (gameState[1, 0] == 1 && gameState[1, 1] == 1 && gameState[1, 2] == 1) ||
                (gameState[2, 0] == 1 && gameState[2, 1] == 1 && gameState[2, 2] == 1) ||
                (gameState[0, 0] == 1 && gameState[1, 1] == 1 && gameState[2, 2] == 1) ||
                (gameState[2, 0] == 1 && gameState[1, 1] == 1 && gameState[0, 2] == 1) ||
                (gameState[0, 0] == 1 && gameState[1, 0] == 1 && gameState[2, 0] == 1) ||
                (gameState[0, 1] == 1 && gameState[1, 1] == 1 && gameState[2, 1] == 1) ||
                (gameState[0, 2] == 1 && gameState[1, 2] == 1 && gameState[2, 2] == 1)
            ) {
                Console.WriteLine("Congrats! You won!");

                // reset the game state
                for (int i = 0; i < gameState.GetLength(0); i++) {
                    for (int j = 0; j < gameState.GetLength(1); j++) {
                        gameState[i, j] = -1;
                    }
                }
                continue;
            }

            // create a list of all unvisited grid positions
            // used to randomly select the position for the bot player
            List<(int, int)> list = new List<(int, int)>();
            for (int i = 0; i < gameState.GetLength(0); i++) {
                for (int j = 0; j < gameState.GetLength(1); j++) {
                    if (gameState[i, j] == -1) list.Add((i, j));
                }
            }

            // if there are no unvisited positions left, it is a draw
            if (list.Count == 0) {
                Console.WriteLine("Game ended in a draw");

                // reset the game state
                for (int i = 0; i < gameState.GetLength(0); i++) {
                    for (int j = 0; j < gameState.GetLength(1); j++) {
                        gameState[i, j] = -1;
                    }
                }
                continue;
            }

            // set a random unvisited position as visited by the bot player
            (int first, int second) = list[rnd.Next(list.Count)];
            gameState[first, second] = 0;

            // if the bot won
            if (
                (gameState[0, 0] == 0 && gameState[0, 1] == 0 && gameState[0, 2] == 0) ||
                (gameState[1, 0] == 0 && gameState[1, 1] == 0 && gameState[1, 2] == 0) ||
                (gameState[2, 0] == 0 && gameState[2, 1] == 0 && gameState[2, 2] == 0) ||
                (gameState[0, 0] == 0 && gameState[1, 1] == 0 && gameState[2, 2] == 0) ||
                (gameState[2, 0] == 0 && gameState[1, 1] == 0 && gameState[0, 2] == 0) ||
                (gameState[0, 0] == 0 && gameState[1, 0] == 0 && gameState[2, 0] == 0) ||
                (gameState[0, 1] == 0 && gameState[1, 1] == 0 && gameState[2, 1] == 0) ||
                (gameState[0, 2] == 0 && gameState[1, 2] == 0 && gameState[2, 2] == 0)
            ) {
                Console.WriteLine("Bzz! You lost!");

                // reset the game state
                for (int i = 0; i < gameState.GetLength(0); i++) {
                    for (int j = 0; j < gameState.GetLength(1); j++) {
                        gameState[i, j] = -1;
                    }
                }
                continue;
            }
        }
    }

    // goes through the gameState to display all its details
    private static void DisplayGameState(int[,] gameState) {
        if (gameState.GetLength(0) != 3 || gameState.GetLength(1) != 3) throw new IndexOutOfRangeException("Expected gameState of length 3x3");
        for (int i = 0; i < gameState.GetLength(0); i++) {
            Console.Write(DisplayPoint(gameState[i, 0], i, 0));
            for (int j = 1; j < gameState.GetLength(1); j++) {
                Console.Write(" | " + DisplayPoint(gameState[i, j], i, j));
            }
            Console.WriteLine();
        }
    }

    // used to display the corresponding player's character
    // for the spot in the table displayed from DisplayGameState
    private static char DisplayPoint(int state, int i, int j) {
        if (state == 1) return 'X';
        if (state == 0) return 'O';
        return (char)(i * 3 + j + '1');
    }

    // retrieving input from console
    private static string RetrieveInput() {
        Console.WriteLine("Enter \"quit\" to end the game. Enter the relevant number for the box, to enter the position you would like to play. You are playing as X, the first player.");
        Console.Write("> ");
        string? input = Console.ReadLine();
        return input ?? throw new IOException("Invalid input provided");
    }
}