using System.Collections.Generic;
using Abacus.Tokens.Operators;

namespace Abacus.Tokens {
	public class Variable: Operand {
		public Variable(int value, string humanReadable): base(value, humanReadable) { }
		public override void Evaluate(Stack<Token> stack) { }
	}
}