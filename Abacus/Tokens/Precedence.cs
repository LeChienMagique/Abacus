namespace Abacus.Tokens {
	public enum Precedence {
		Comma      = 0,
		Assignment = 1,
		Addition   = 2,
		Minus      = 2,
		Division   = 3,
		Multiply   = 3,
		Modulus    = 3,
		Exponent   = 4,
		UnaryMinus = 5,
		UnaryPlus  = 5,
		FuncCall   = 6,
		// Grouping = 7
	}
}