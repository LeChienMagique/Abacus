using System;
using System.Collections.Generic;
using Abacus.Exceptions;
using Abacus.Tokens.Operators;

namespace Abacus.Tokens.UnaryOperators {
	public class UnaryPlus: UnaryOperator {
		public UnaryPlus(): base(Precedence.UnaryPlus, "u+") { }

		public override void PerformUnaryOperation(Stack<Token> stack) {
			CheckOperandsCount(stack);
			Token op = stack.Pop();
			CheckInvalidArguments(op);
			stack.Push(op);
		}
	}
}