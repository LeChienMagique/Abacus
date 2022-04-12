using System;
using System.Collections.Generic;
using Abacus.Exceptions;

namespace Abacus.Tokens.Operators {
	public class Exponent: Operator {
		public Exponent(): base(2, Precedence.Exponent, "^", Associativity.Right) { }

		public override void Evaluate(Stack<Token> stack) {
			CheckOperandsCount(stack);
			Token op2 = stack.Pop();
			Token op1 = stack.Pop();
			CheckInvalidArguments(op1, op2);
			stack.Push(new Number((int) Math.Pow((double) ((Operand) op1).Value, (double) ((Operand) op2).Value)));
		}
	}
}