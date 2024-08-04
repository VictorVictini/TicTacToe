namespace TicTacToe {
    public class Program {
        // program runs from here
        private static void Main() {
            // how many players -- 2 or 1
            int num = 0;
            try {
                num = IOManager.RetrieveNum("Enter how many players there will be (2 or 1)", 1, 2);

            // quit the game
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

                // quit the game
                } catch (QuitGameException exception) {
                    Console.WriteLine(exception.Message);
                    return;
                }
            
            // otherwise, it's a human
            } else {
                secondPlayer = new User(MoveState.Second, 'O');
            }

            // create the game with both players
            Game game;
            try {
                game = new Game(firstPlayer, secondPlayer);
            } catch (EqualPlayerObjectException exception) {
                Console.WriteLine(exception.Message);
                return;
            } catch (EqualPlayerMoveStateException exception) {
                Console.WriteLine(exception.Message);
                return;
            }

            // play games until provided the Quit exception
            while (true) {
                Console.WriteLine("Starting new game...");
                try {
                    game.Play();

                // quit the game
                } catch (QuitGameException exception) {
                    Console.WriteLine(exception.Message);
                    return;
                }
            }
        }
    }
}