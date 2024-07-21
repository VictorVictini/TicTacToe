namespace TicTacToe {
    public class Program {
        // program runs from here
        private static void Main() {
            // how many players -- 2 or 1
            int num = IOManager.RetrieveNum("Enter \"quit\" to end the game. Enter how many players there will be (2 or 1)", 1, 2);

            // first player is human
            Player firstPlayer = new User(MoveState.First, 'X');

            // if 1 player, the second is a bot
            Player secondPlayer;
            if (num == 1) {
                secondPlayer = new Bot(MoveState.Second, 'O');
            } else {
                secondPlayer = new User(MoveState.Second, 'O');
            }

            Game game = new Game(firstPlayer, secondPlayer);
            while (true) {
                Console.WriteLine("Starting new game...");
                game.Play();
            }
        }
    }
}