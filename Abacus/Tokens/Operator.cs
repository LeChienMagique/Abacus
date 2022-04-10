using System;
using System.Collections.Generic;
using Abacus.Tokens.Operators;

namespace Abacus.Tokens {
	public abstract class Operator: Token {
		protected int arity;

		protected Precedence precedence;

		// protected Operand[]  operands;
		protected Associativity associativity;

		public int           Arity         => arity;
		public Precedence    Precedence    => precedence;
		public Associativity Associativity => associativity;

		public Operator(int arity, Precedence precedence, string humanReadable, Associativity associativity) {
			this.arity         = arity;
			this.precedence    = precedence;
			this.humanReadable = humanReadable;
			this.associativity = associativity;
		}

		protected void CheckOperandsCount(Stack<Token> tokens) {
			if (tokens.Count < arity)
				throw new Exception("Not enough arguments");
		}

		public abstract void Evaluate(Stack<Token> stack);
	}
}