namespace TicTacToe {
    public static class Constants {
        // self-explanatory, board length and height
        public static readonly int BoardLength = 3;
        public static readonly int BoardHeight = 3;

        // associated character with the MoveState
        public static readonly Dictionary<MoveState, char> moveChar = new Dictionary<MoveState, char>{
            { MoveState.First,  'X'},
            { MoveState.Second, 'O'}
        };
    }
}