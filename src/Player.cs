namespace TicTacToe {
    abstract class Player {
        // letter to indicate which player's move it is
        private char letter;

        // getter
        public char GetLetter() {
            return letter;
        }

        // player makes a move
        abstract public void MakeMove();
    }
}