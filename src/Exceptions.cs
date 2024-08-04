namespace TicTacToe {
    public class QuitGameException : Exception {
        public QuitGameException(string msg) : base(msg) { /* empty body */ }
    }
    public class EqualPlayerObjectException : Exception {
        public EqualPlayerObjectException(string msg) : base(msg) { /* empty body */ }
    }
    public class EqualPlayerMoveState : Exception {
        public EqualPlayerMoveState(string msg) : base(msg) { /* empty body */ }
    }
}