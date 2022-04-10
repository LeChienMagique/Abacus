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
				'(' => new LeftParenthesis(),
				')' => new RightParenthesis(),
				'=' => new Assignment(),
				_   => throw new UnknownOperatorException($"Unknown operator : {opChar}")
			};
		}

		private static void AddNumberOrSymbol(List<Token> tokens, string tokenString) {
			int result;
			if (int.TryParse(tokenString, out result)) {
				tokens.Add(new Number(result));
			}
			else {
				tokens.Add(new Symbol(tokenString));
			}
		}

		public static List<Token> Lex(string input) {
			/* TODO :
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
						AddNumberOrSymbol(tokens, tokenString);
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

				// detect symbols
				if (char.IsLetter(currChar)) {
					tokenString += currChar;
					i++;
					continue;
				}

				// add number to list
				if (!string.IsNullOrEmpty(tokenString)) {
					AddNumberOrSymbol(tokens, tokenString);
					tokenString = "";
				}

				Token op = GetCorrespondingToken(currChar);
				tokens.Add(op);
				i++;
			}

			if (!string.IsNullOrEmpty(tokenString)) {
				AddNumberOrSymbol(tokens, tokenString);
			}
			return tokens;
		}

		public static void TransformUnaryOperators(ref List<Token> tokens) {
			int i = 0;
			while (i < tokens.Count) {
				Token token = tokens[i];
				switch (token) {
					// only Minus and Plus can be unary
					case Minus: {
						// if after an operator or nothing then it is unary
						if (i == 0 || tokens[i - 1] is Operator && !(tokens[i - 1] is RightParenthesis)) {
							tokens[i] = new UnaryMinus();
						}
						break;
					}
					// if unary plus is detected remove it as it has no interaction
					case Plus when i == 0 || i == tokens.Count - 1 || tokens[i - 1] is Operator:
						tokens.RemoveAt(i);
						continue;
				}
				i++;
			}
		}
	}
}