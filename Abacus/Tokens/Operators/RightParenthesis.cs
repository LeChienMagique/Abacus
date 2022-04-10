using System.Collections.Generic;

namespace Abacus.Tokens.Operators {
	public class RightParenthesis: Parenthesis {
		public RightParenthesis(): base(State.Closing, ")") { }
		public override void Evaluate(Stack<Token> stack) => throw new System.NotImplementedException();
	}
}