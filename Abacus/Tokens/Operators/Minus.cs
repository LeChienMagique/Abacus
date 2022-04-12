using System;
using System.Collections.Generic;
using Abacus.Exceptions;

namespace Abacus.Tokens.Operators {
	public class Minus: Operator {
		public Minus(): base(2, Precedence.Minus, "-", Associativity.Left) { }

		public override void Evaluate(Stack<Token> stack) {
			CheckOperandsCount(stack);
			Token op2 = stack.Pop();
			Token op1 = stack.Pop();
			CheckInvalidArguments(op1, op2);
			stack.Push(new Number(((Operand) op1).Value - ((Operand) op2).Value));
		}
	}
}