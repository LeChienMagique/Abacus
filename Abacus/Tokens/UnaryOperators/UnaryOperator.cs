using System;
using System.Collections.Generic;
using Abacus.Tokens.Operators;

namespace Abacus.Tokens.UnaryOperators {
	public abstract class UnaryOperator: Operator {
		public UnaryOperator(Precedence precedence, string humanReadable): base(1, precedence, humanReadable, Associativity.Right) { }

		public override void Evaluate(Stack<Token> stack) =>
			throw new MethodAccessException("Unary operator can't evaluate with 2 operand, use ");

		public abstract Operand PerformUnaryOperation(Operand operand);
	}
}