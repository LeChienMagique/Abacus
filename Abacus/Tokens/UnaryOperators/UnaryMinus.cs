using System;
using System.Collections.Generic;
using Abacus.Exceptions;
using Abacus.Tokens.Operators;

namespace Abacus.Tokens.UnaryOperators {
	public class UnaryMinus: UnaryOperator {
		public UnaryMinus(): base(Precedence.UnaryMinus, "-") { }

		public override void PerformUnaryOperation(Stack<Token> stack) {
			CheckOperandsCount(stack);
			Token op = stack.Pop();
			CheckInvalidArguments(op);
			((Operand) op).NegateValue();
			stack.Push(op);
		}
	}
}