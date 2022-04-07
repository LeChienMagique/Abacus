using System;
using Abacus.Tokens.Operators;

namespace Abacus.Tokens.UnaryOperators {
	public abstract class UnaryOperator: Operator {
		public UnaryOperator(Precedence precedence): base(1, precedence) { }

		public override Operand Evaluate(Operand op1, Operand op2) =>
			throw new MethodAccessException("Unary operator can't evaluate with 2 operand, use ");

		public abstract Operand PerformUnaryOperation(Operand operand);
	}
}