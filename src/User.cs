namespace TicTacToe {
    class User : Player {
        public User(MoveState player) {
            this.SetPlayer(player);
        }
        public override void ResetState() {
            // nothing to reset here, included for completeness
            return;
        }

        public override MoveState[] MakeMove(MoveState[] board) {
            int move = -1;
            bool found = false;
            while (!found) {
                move = IOManager.RetrieveNum("Enter \"quit\" to end the game. Enter the relevant number for the box, to enter the position you would like to play. You are playing as X, the first player.", 1, board.Length);
                if (board[move - 1] != MoveState.Unused) {
                    Console.WriteLine("Board position selected is currently occupied.");
                } else {
                    found = true;
                }
            }

            board[move - 1] = this.GetPlayer();
            return board;
        }
    }
}