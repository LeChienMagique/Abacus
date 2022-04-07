namespace Abacus.Tokens.Operators {
	public abstract class Operator: Token {
		protected int arity;

		protected Precedence precedence;
		// protected Operand[]  operands;

		public int        Arity      => arity;
		public Precedence Precedence => precedence;

		public Operator(int arity, Precedence precedence) {
			this.arity      = arity;
			this.precedence = precedence;
		}

		public abstract Operand Evaluate(Operand op1, Operand op2);
	}
}