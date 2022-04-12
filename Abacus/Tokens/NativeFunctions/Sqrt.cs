using System;
using System.Collections.Generic;
using Abacus.Exceptions;
using Abacus.Tokens.Operators;

namespace Abacus.Tokens.NativeFunctions {
	public class Sqrt: Function {
		public Sqrt(): base(1, "sqrt") { }

		public override void Evaluate(Stack<Token> stack) {
			CheckOperandsCount(stack);
			Token op = stack.Pop();
			CheckInvalidArguments(op);
			stack.Push(new Number((int) Math.Sqrt(((Operand) op).Value)));
		}
	}
}