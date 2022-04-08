namespace Abacus.Tokens.Operators {
	public enum State {
		Opening,
		Closing
	}

	public abstract class Parenthesis: Operator {
		public State State;

		public Parenthesis(State state, string humanReadable): base(0, Precedence.Grouping, humanReadable, Associativity.None) {
			this.State = state;
		}
	}
}