namespace Abacus.Tokens.Operators {
	public class Division: Operator {
		public Division(): base(2, Precedence.Division, "/", Associativity.Left) { }

		public override Operand Evaluate(Operand op1, Operand op2) {
			return new Number(op1.Value / op2.Value);
		}
	}
}