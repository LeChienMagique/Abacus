namespace Abacus.Tokens.Operators {
	public enum Precedence {
		Comma      = 0,
		Assignment = 1,
		Addition   = 2,
		Minus      = 2,
		Division   = 3,
		Multiply   = 3,
		Modulus    = 3,
		Exponent   = 4,
		FuncCall   = 5,
		Grouping   = 6,
		UnaryMinus = 7,
		UnaryPlus  = 7
	}
}