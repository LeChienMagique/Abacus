using System;
using System.Collections.Generic;
using System.Linq;
using Abacus.Exceptions;
using Abacus.Tokens;
using Abacus.Tokens.Operators;
using Abacus.Tokens.UnaryOperators;

namespace Abacus {
	public static class Lexer {
		// TODO : think of a way to specify if bonuses are enabled
		private static Token GetCorrespondingToken(char opChar) {
			return opChar switch {
				'+' => new Plus(),
				'-' => new Minus(),
				'*' => new Multiply(),
				'/' => new Division(),
				'^' => new Exponent(),
				'%' => new Modulus(),
				'(' => new Parenthesis(State.Opening),
				')' => new Parenthesis(State.Closing),
				_   => throw new UnknownOperatorException($"Unknown operator : {opChar}")
			};
		}

		public static List<Token> Lex(string input) {
			/* TODO :
			 *  - detect unary minus/plus
			 *  - detect functions
			 */
			List<Token> tokens      = new List<Token>();
			int         i           = 0;
			string      tokenString = "";
			while (i < input.Length) {
				char currChar = input[i];
				if (currChar == ' ') {
					// add number to list
					if (!string.IsNullOrEmpty(tokenString)) {
						tokens.Add(new Number(int.Parse(tokenString)));
						tokenString = "";
					}
					i++;
					continue;
				}
				// detect number
				if (char.IsDigit(currChar)) {
					tokenString += currChar;
					i++;
					continue;
				}

				// add number to list
				if (!string.IsNullOrEmpty(tokenString)) {
					tokens.Add(new Number(int.Parse(tokenString)));
					tokenString = "";
				}

				Token op = GetCorrespondingToken(currChar);
				tokens.Add(op);
				i++;
			}
			if (!string.IsNullOrEmpty(tokenString))
				tokens.Add(new Number(int.Parse(tokenString)));
			return tokens;
		}

		public static void TransformUnaryOperators(List<Token> tokens) {
			int i = 0;
			while (i < tokens.Count) {
				Token token = tokens[i];
				switch (token) {
					// only Minus and Plus can be unary
					case Minus: {
						// if after an operator or nothing then it is unary
						if (i == 0 || tokens[i - 1] is Operator) {
							tokens[i] = new UnaryMinus();
						}
						break;
					}
					// if unary plus is detected remove it as it has no interaction
					case Plus when i == 0 || tokens[i - 1] is Operator:
						tokens.RemoveAt(i);
						continue;
				}
				i++;
			}
		}
	}
}