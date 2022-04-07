namespace Abacus.Tokens.Operators {
	public class Division: Operator {
		public Division(): base(2, Precedence.Division) { }

		public override Operand Evaluate(Operand op1, Operand op2) {
			// TODO
			throw new System.NotImplementedException();
		}
	}
}