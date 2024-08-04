namespace TicTacToe {
    class User : Player {
        // basic constructor
        public User(MoveState player) {
            this.SetPlayer(player);
        }

        // user makes a move
        public override MoveState[] MakeMove(MoveState[] board) {
            // get an available move
            int move = -1;
            bool found = false;
            while (!found) {
                move = IOManager.RetrieveNum("Enter the relevant number for the box, to enter the position you would like to play. You are playing as " + this.GetLetter() + ".", 1, board.Length);
                if (board[move - 1] != MoveState.Unused) {
                    Console.WriteLine("Board position selected is currently occupied.");
                } else {
                    found = true;
                }
            }

            // play the move and remove it from the 
            board[move - 1] = this.GetPlayer();
            PosLeftRemove(move - 1);

            return board;
        }
    }
}