namespace Abacus.Tokens {
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