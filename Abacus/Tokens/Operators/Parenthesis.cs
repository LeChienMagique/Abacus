namespace Abacus.Tokens.Operators {
    public enum State {
        Opening,
        Closing
    }

    public class Parenthesis : Token {
        public State State;

        public Parenthesis(State state) {
            this.State = state;
        }
    }
}