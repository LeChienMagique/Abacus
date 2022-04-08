using System;

namespace Abacus.Tokens.Operators {
	public class Exponent: Operator {
		public Exponent(): base(2, Precedence.Exponent, "^", Associativity.Right) { }

		public override Operand Evaluate(Operand op1, Operand op2) {
			return new Number((int) Math.Pow((double) op1.Value, (double) op2.Value));
		}
	}
}