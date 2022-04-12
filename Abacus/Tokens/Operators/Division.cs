using System;
using System.Collections.Generic;
using Abacus.Exceptions;
using ArithmeticException = Abacus.Exceptions.ArithmeticException;
using SyntaxErrorException = Abacus.Exceptions.SyntaxErrorException;

namespace Abacus.Tokens.Operators {
	public class Division: Operator {
		public Division(): base(2, Precedence.Division, "/", Associativity.Left) { }

		public override void Evaluate(Stack<Token> stack) {
			CheckOperandsCount(stack);
			Token op2 = stack.Pop();
			Token op1 = stack.Pop();
			CheckInvalidArguments(op1, op2);
			if (((Operand) op2).Value == 0) {
				throw new ArithmeticException();
			}
			stack.Push(new Number(((Operand) op1).Value / ((Operand) op2).Value));
		}
	}
}