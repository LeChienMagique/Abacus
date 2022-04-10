using System.Collections.Generic;

namespace Abacus.Tokens.Operators {
	public class Number: Operand {
		public Number(int value): base(value, value.ToString()) { }

		public override void Evaluate(Stack<Token> stack) => stack.Push(this);
	}
}