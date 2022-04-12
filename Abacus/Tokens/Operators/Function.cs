using System.Collections.Generic;

namespace Abacus.Tokens.Operators {
	public abstract class Function: Operator {
		public Function(int arity, string humanReadable):
			base(arity, Precedence.FuncCall, humanReadable, Associativity.Left) { }

		public abstract override void Evaluate(Stack<Token> stack);
	}
}