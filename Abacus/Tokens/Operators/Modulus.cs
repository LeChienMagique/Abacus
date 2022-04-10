using System;
using System.Collections.Generic;

namespace Abacus.Tokens.Operators {
	public class Modulus: Operator {
		public Modulus(): base(2, Precedence.Modulus, "%", Associativity.Left) { }

		public override void Evaluate(Stack<Token> stack) {
			CheckOperandsCount(stack);
			Token op2 = stack.Pop();
			Token op1 = stack.Pop();
			if (!(op1 is Operand && op2 is Operand))
				throw new Exception("Syntax Error");
			stack.Push(new Number(((Operand)op1).Value % ((Operand)op2).Value));
		}
	}
}