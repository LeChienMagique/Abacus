using System;
using System.Collections.Generic;
using Abacus.Exceptions;
using Abacus.Tokens.Operators;

namespace Abacus.Tokens.NativeFunctions {
	public class Min: Function {
		public Min(): base(2, "min") { }

		public override void Evaluate(Stack<Token> stack) {
			CheckOperandsCount(stack);
			Token op2 = stack.Pop();
			Token op1 = stack.Pop();
			CheckInvalidArguments(op1, op2);
			int result = Math.Min(((Operand) op1).Value, ((Operand) op2).Value);
			stack.Push(new Number(result));
		}
	}
}