using System.Collections.Generic;

namespace Abacus.Tokens.Operators {
	public class Comma: Operator {
		public Comma(): base(0, Precedence.Comma, ",", Associativity.Left) { }
		public override void Evaluate(Stack<Token> stack) { }
	}
}