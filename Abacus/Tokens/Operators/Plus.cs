namespace Abacus.Tokens.Operators {
	public class Plus: Operator {
		public Plus(): base(2, Precedence.Addition) { }

		public override Operand Evaluate(Operand op1, Operand op2) {
			// TODO
			throw new System.NotImplementedException();
		}
	}
}