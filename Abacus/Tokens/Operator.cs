using System;
using System.Collections.Generic;
using Abacus.Exceptions;
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
			if (tokens.Count < arity) {
				foreach (Token token in tokens) {
					Console.WriteLine(token.HumanReadable);
				}

				throw new SyntaxErrorException($"Not enough arguments for operator : {humanReadable}");
			}
		}

		private void CheckArg(Token tok) {
			if (tok is Symbol)
				throw new UnboundVariableException();
			if (!(tok is Operand))
				throw new SyntaxErrorException();
		}

		protected void CheckInvalidArguments(Token tok) => CheckArg(tok);

		protected void CheckInvalidArguments(Token tok1, Token tok2) {
			CheckArg(tok1);
			CheckArg(tok2);
		}

		protected void CheckInvalidArguments(List<Token> args) {
			foreach (Token tok in args) {
				CheckArg(tok);
			}
		}

		public abstract void Evaluate(Stack<Token> stack);
	}
}