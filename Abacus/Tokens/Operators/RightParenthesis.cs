namespace Abacus.Tokens.Operators {
	public class RightParenthesis: Parenthesis {
		public RightParenthesis(): base(State.Closing, ")") { }
		public override Operand Evaluate(Operand op1, Operand op2) => throw new System.NotImplementedException();
	}
}