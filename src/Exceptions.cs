namespace TicTacToe {
    public class QuitGameException : Exception {
        public QuitGameException(string msg) : base(msg) { /* empty body */ }
    }
    public class EqualPlayerObjectException : Exception {
        public EqualPlayerObjectException(string msg) : base(msg) { /* empty body */ }
    }
    public class EqualPlayerMoveStateException : Exception {
        public EqualPlayerMoveStateException(string msg) : base(msg) { /* empty body */ }
    }
    public class UnexpectedEnumValueException : Exception {
        public UnexpectedEnumValueException(string msg) : base(msg) { /* empty body */ }
    }
}