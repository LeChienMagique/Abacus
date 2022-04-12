using System;
using System.Collections.Generic;
using Abacus.Tokens.Operators;

namespace Abacus.Tokens.NativeFunctions {
	public class Sqrt: Function {
		public Sqrt(): base(1, "sqrt") { }

		public override void Evaluate(Stack<Token> stack) {
			CheckOperandsCount(stack);
			Token op = stack.Pop();
			if (!(op is Operand)) {
				throw new Exception("Syntax Error");
			}
			stack.Push(new Number((int) Math.Sqrt(((Operand) op).Value)));
		}
	}
}