using System;
using System.Collections.Generic;
using Abacus.Exceptions;
using Abacus.Tokens.Operators;
using ArithmeticException = Abacus.Exceptions.ArithmeticException;

namespace Abacus.Tokens.NativeFunctions {
	public class Sqrt: Function {
		public Sqrt(): base(1, "sqrt") { }

		public override void Evaluate(Stack<Token> stack) {
			CheckOperandsCount(stack);
			Token op = stack.Pop();
			CheckInvalidArguments(op);
			if (((Operand) op).Value < 0)
				throw new ArithmeticException();
			stack.Push(new Number((int) Math.Sqrt(((Operand) op).Value)));
		}
	}
}