using System;
using System.Collections.Generic;
using Abacus.Exceptions;
using Abacus.Tokens.Operators;

namespace Abacus.Tokens.NativeFunctions {
	public class Facto: Function {
		public Facto(): base(1, "facto") { }

		public override void Evaluate(Stack<Token> stack) {
			CheckOperandsCount(stack);
			Token op = stack.Pop();
			CheckInvalidArguments(op);
			int value  = ((Operand) op).Value;
			int result = 1;
			for (; value > 1 ; value--)
				result *= value;
			stack.Push(new Number(result));
		}
	}
}