namespace Abacus.Tokens.Operators {
	public class Plus: Operator {
		public Plus(): base(2, Precedence.Addition, "+", Associativity.Left) { }

		public override Operand Evaluate(Operand op1, Operand op2) {
			return new Number(op1.Value + op2.Value);
		}
	}
}