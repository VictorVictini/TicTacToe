namespace TicTacToe {
    abstract class Player {
        // letter to indicate which player's move it is
        private char letter;

        // what the player's designated enum on the board is
        private MoveState player;

        // how many positions are unplayed
        private List<int> posLeft;

        // basic constructor to remove warnings
        public Player() {
            posLeft = new List<int>();
        }

        // getters
        public char GetLetter() {
            return letter;
        }
        public MoveState GetPlayer() {
            return player;
        }

        // determines if there is a winning position
        // context can determine who won, so we do not need to return that
        public bool HasWon(MoveState[] board) {
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

        // setters
        public void SetLetter(char letter) {
            this.letter = letter;
        }
        public void SetPlayer(MoveState player) {
            this.player = player;
        }
        public void SetPosLeft(List<int> posLeft) {
            this.posLeft = posLeft;
        }

        // removing from posLeft
        public void PosLeftRemove(int move) {
            this.posLeft.Remove(move);
        }

        // get a random available position
        public int GetRandomPosLeft(Random rnd) {
            return posLeft[rnd.Next(posLeft.Count)];
        }
        // get count of available positions
        public int CountAvailablePos() {
            return posLeft.Count;
        }
        // getting a position at a given index
        public int GetPosition(int index) {
            return posLeft[index];
        }

        // player makes a move
        abstract public MoveState[] MakeMove(MoveState[] board);
    }
}