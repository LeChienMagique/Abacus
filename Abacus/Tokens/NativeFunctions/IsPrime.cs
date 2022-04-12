using System;
using System.Collections.Generic;
using Abacus.Exceptions;
using Abacus.Tokens.Operators;

namespace Abacus.Tokens.NativeFunctions {
	public class IsPrime: Function {
		public IsPrime(): base(1, "isprime") { }

		private int TestIsPrime(int a) {
			if (a <= 1) return 0;
			if (a == 2) return 1;
			if (a % 2 == 0) return 0;

			int boundary = (int) Math.Floor(Math.Sqrt(a));

			for (int i = 3 ; i <= boundary ; i += 2)
				if (a % i == 0)
					return 0;

			return 1;
		}

		public override void Evaluate(Stack<Token> stack) {
			CheckOperandsCount(stack);
			Token op = stack.Pop();
			CheckInvalidArguments(op);
			stack.Push(new Number(TestIsPrime(((Operand) op).Value)));
		}
	}
}