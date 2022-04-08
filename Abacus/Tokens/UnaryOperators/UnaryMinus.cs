using System;
using Abacus.Tokens.Operators;

namespace Abacus.Tokens.UnaryOperators {
	public class UnaryMinus: UnaryOperator {
		public UnaryMinus(): base(Precedence.UnaryMinus, "-") { }

		public override Operand PerformUnaryOperation(Operand operand) {
			// TODO : when variables will be here treat them aswell
			if (operand is Number)
				return new Number(-operand.Value);
			throw new NotImplementedException("Not implemented for Operand other than Number");
		}
	}
}