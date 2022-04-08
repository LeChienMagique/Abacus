namespace Abacus.Tokens.Operators {
	public abstract class Operator: Token {
		protected int arity;

		protected Precedence precedence;

		// protected Operand[]  operands;
		protected Associativity associativity;

		public int           Arity         => arity;
		public Precedence    Precedence    => precedence;
		public Associativity Associativity => associativity;

		public Operator(int arity, Precedence precedence, string humanReadable, Associativity associativity) {
			this.arity         = arity;
			this.precedence    = precedence;
			this.humanReadable = humanReadable;
			this.associativity = associativity;
		}

		public abstract Operand Evaluate(Operand op1, Operand op2);
	}
}