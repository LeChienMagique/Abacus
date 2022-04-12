using System;
using System.Collections.Generic;
using Abacus.Exceptions;
using Abacus.Tokens.Operators;

namespace Abacus.Tokens.NativeFunctions {
	public class Gcd: Function {
		public Gcd(): base(2, "gcd") { }

		private uint ComputeGCD(uint a, uint b) {
			while (a != 0 && b != 0) {
				if (a > b)
					a %= b;
				else
					b %= a;
			}
			return a | b;
		}

		public override void Evaluate(Stack<Token> stack) {
			CheckOperandsCount(stack);
			Token op2 = stack.Pop();
			Token op1 = stack.Pop();
			CheckInvalidArguments(op1, op2);
			int op1Value = ((Operand) op1).Value;
			int op2Value = ((Operand) op2).Value;
			int result;
			if (op1Value < 0 && op2Value < 0)
				result = -(int) ComputeGCD((uint) -op1Value, (uint) -op2Value);
			else
				result = (int) ComputeGCD((uint) op1Value, (uint) op2Value);
			stack.Push(new Number(result));
		}
	}
}