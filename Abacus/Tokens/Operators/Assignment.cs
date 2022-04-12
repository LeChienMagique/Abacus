using System;
using System.Collections.Generic;
using Abacus.Exceptions;

namespace Abacus.Tokens.Operators {
	public class Assignment: Operator {
		public Assignment():
			base(2, Precedence.Assignment, "=", Associativity.Right) { }

		public void EvaluateAssignment(Stack<Token> stack, Dictionary<string, int> context) {
			CheckOperandsCount(stack);
			Token op2 = stack.Pop();
			Token op1 = stack.Pop();
			if (!(op1 is Symbol && op2 is Operand))
				throw new SyntaxErrorException("Syntax Error");
			stack.Push(op2);
			context[((Symbol) op1).Name] = ((Operand) op2).Value;
		}

		public override void Evaluate(Stack<Token> stack) => throw new NotImplementedException();
	}
}