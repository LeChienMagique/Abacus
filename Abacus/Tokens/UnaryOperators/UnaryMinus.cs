using System;
using System.Collections.Generic;
using Abacus.Tokens.Operators;

namespace Abacus.Tokens.UnaryOperators {
	public class UnaryMinus: UnaryOperator {
		public UnaryMinus(): base(Precedence.UnaryMinus, "-") { }

		public override void PerformUnaryOperation(Stack<Token> stack) {
			Token op = stack.Pop();
			if (!(op is Operand))
				throw new Exception("Syntax Error unary before something other than an Operand");
			((Operand) op).NegateValue();
			stack.Push(op);
		}
	}
}