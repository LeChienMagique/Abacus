namespace Abacus.Tokens.Operators {
	public class Modulus: Operator {
		public Modulus(): base(2, Precedence.Modulus, "%", Associativity.Left) { }

		public override Operand Evaluate(Operand op1, Operand op2) {
			return new Number(op1.Value % op2.Value);
		}
	}
}