using System;
using System.Collections.Generic;
using Abacus.Exceptions;
using Abacus.Tokens.Operators;
using ArithmeticException = Abacus.Exceptions.ArithmeticException;

namespace Abacus.Tokens.NativeFunctions {
	public class Fibonnacci: Function {
		public Fibonnacci(): base(1, "fibo") { }

		private int ComputeFibo(int iters) {
			int u0 = 0;
			int u1 = 1;
			for (; iters > 0 ; iters--) {
				int temp = u0;
				u0 = u1;
				u1 = temp + u1;
			}
			return u0;
		}

		public override void Evaluate(Stack<Token> stack) {
			CheckOperandsCount(stack);
			Token op = stack.Pop();
			CheckInvalidArguments(op);
			if (((Operand) op).Value < 0)
				throw new ArithmeticException();
			int result = ComputeFibo(((Operand) op).Value);
			stack.Push(new Number(result));
		}
	}
}