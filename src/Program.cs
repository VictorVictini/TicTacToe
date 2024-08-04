namespace TicTacToe {
    public class Program {
        // program runs from here
        private static void Main() {
            // how many players -- 2 or 1
            int num = 0;
            try {
                num = IOManager.RetrieveNum("Enter how many players there will be (2 or 1)", 1, 2);
            } catch (QuitGameException exception) {
                Console.WriteLine(exception.Message);
                return;
            }

            // first player is human
            Player firstPlayer = new User(MoveState.First, 'X');

            // if 1 player, the second is a bot
            Player secondPlayer;
            if (num == 1) {
                try {
                    secondPlayer = new Bot(MoveState.Second, 'O');
                } catch (QuitGameException exception) {
                    Console.WriteLine(exception.Message);
                    return;
                }
            } else {
                secondPlayer = new User(MoveState.Second, 'O');
            }

            Game game;
            try {
                game = new Game(firstPlayer, secondPlayer);
            } catch (EqualPlayerObjectException exception) {
                Console.WriteLine(exception.Message);
                return;
            }
            while (true) {
                Console.WriteLine("Starting new game...");
                try {
                    game.Play();
                } catch (QuitGameException exception) {
                    Console.WriteLine(exception.Message);
                    return;
                }
            }
        }
    }
}