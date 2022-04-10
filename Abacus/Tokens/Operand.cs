using System.Collections.Generic;

namespace Abacus.Tokens {
	public abstract class Operand: Token {
		protected int value;

		public int Value => value;

		public Operand(int value, string humanReadable) {
			this.value         = value;
			this.humanReadable = humanReadable;
		}

		public abstract void Evaluate(Stack<Token> stack);
	}
}