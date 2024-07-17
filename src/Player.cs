namespace TicTacToe {
    abstract class Player {
        // letter to indicate which player's move it is
        private char letter;

        // getter
        public char GetLetter() {
            return letter;
        }

        // setter
        public void SetLetter(char letter) {
            this.letter = letter;
        }

        // player makes a move
        abstract public MoveState[] MakeMove(MoveState[] board);
    }
}