namespace TicTacToe {
    abstract class Player {
        // letter to indicate which player's move it is
        private MoveState player;

        // getter
        public MoveState GetPlayer() {
            return player;
        }

        // determines if there is a winning position
        // context can determine who won, so we do not need to return that
        public static bool HasWon(MoveState[] board) {
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
        public void SetPlayer(MoveState player) {
            this.player = player;
        }

        // player makes a move
        abstract public MoveState[] MakeMove(MoveState[] board);
    }
}