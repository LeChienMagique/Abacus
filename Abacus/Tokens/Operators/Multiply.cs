using System;
using System.Collections.Generic;
using Abacus.Exceptions;

namespace Abacus.Tokens.Operators {
	public class Multiply: Operator {
		public Multiply(): base(2, Precedence.Multiply, "*", Associativity.Left) { }

		public override void Evaluate(Stack<Token> stack) {
			CheckOperandsCount(stack);
			Token op2 = stack.Pop();
			Token op1 = stack.Pop();
			CheckInvalidArguments(op1, op2);
			stack.Push(new Number(((Operand) op1).Value * ((Operand) op2).Value));
		}
	}
}