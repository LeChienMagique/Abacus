using System.Collections.Generic;

namespace Abacus.Tokens.Operators {
	public class LeftParenthesis: Parenthesis {
		public LeftParenthesis(): base(State.Opening, "(") { }
		public override void Evaluate(Stack<Token> stack) => throw new System.NotImplementedException();
	}
}