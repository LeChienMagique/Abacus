namespace Abacus.Tokens.Operators {
	public class LeftParenthesis: Parenthesis {
		public LeftParenthesis(): base(State.Opening, "(") { }
		public override Operand Evaluate(Operand op1, Operand op2) => throw new System.NotImplementedException();
	}
}