using System;
using System.Collections.Generic;
using System.Linq;
using Abacus.Exceptions;
using Abacus.Tokens;
using Abacus.Tokens.NativeFunctions;
using Abacus.Tokens.Operators;
using Abacus.Tokens.UnaryOperators;
using ArithmeticException = Abacus.Exceptions.ArithmeticException;

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
				',' => new Comma(),
				_   => throw new UnknownOperatorException($"Unknown operator : {opChar}")
			};
		}

		private static List<string> nativeFunctions = new List<string>()
			{"facto", "sqrt", "max", "min", "isprime", "fibo", "gcd"};

		private static bool IsFunction(string tokenString) {
			return nativeFunctions.Contains(tokenString);
		}

		private static Function GetCorrespondingFunction(string funcName) {
			return funcName switch {
				"facto"   => new Facto(),
				"sqrt"    => new Sqrt(),
				"max"     => new Max(),
				"min"     => new Min(),
				"isprime" => new IsPrime(),
				"fibo"    => new Fibonnacci(),
				"gcd"     => new Gcd(),
				_         => throw new NotImplementedException()
			};
		}

		private static void AddTokenFromString(List<Token> tokens, string tokenString) {
			int result;
			if (int.TryParse(tokenString, out result)) {
				tokens.Add(new Number(result));
			}
			else if (IsFunction(tokenString)) {
				tokens.Add(GetCorrespondingFunction(tokenString));
			}
			else {
				tokens.Add(new Symbol(tokenString));
			}
		}

		public static List<Token> Lex(string input) {
			List<Token> tokens       = new List<Token>();
			int         i            = 0;
			string      numberString = "";
			string      symbolString = "";
			while (i < input.Length) {
				char currChar = input[i];
				if (currChar == ' ') {
					// add number to list
					if (!string.IsNullOrEmpty(numberString)) {
						AddTokenFromString(tokens, numberString);
						numberString = "";
					}
					else if (!string.IsNullOrEmpty(symbolString)) {
						AddTokenFromString(tokens, symbolString);
						symbolString = "";
					}
					i++;
					continue;
				}

				// detect symbols
				if (symbolString.Length == 0) {
					if (char.IsLetter(currChar) || currChar == '_') {
						symbolString += currChar;
						i++;
						continue;
					}
				}
				else {
					if (char.IsLetterOrDigit(currChar) || currChar == '_') {
						symbolString += currChar;
						i++;
						continue;
					}
				}

				// detect number
				if (char.IsDigit(currChar)) {
					numberString += currChar;
					i++;
					continue;
				}

				if (!string.IsNullOrEmpty(numberString)) {
					AddTokenFromString(tokens, numberString);
					numberString = "";
				}
				if (!string.IsNullOrEmpty(symbolString)) {
					AddTokenFromString(tokens, symbolString);
					symbolString = "";
				}

				Token op = GetCorrespondingToken(currChar);
				tokens.Add(op);
				i++;
			}

			if (!string.IsNullOrEmpty(numberString)) {
				AddTokenFromString(tokens, numberString);
			}
			if (!string.IsNullOrEmpty(symbolString)) {
				AddTokenFromString(tokens, symbolString);
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
					case Plus when i == 0 || i == tokens.Count - 1 ||
					               tokens[i - 1] is Operator && !(tokens[i - 1] is RightParenthesis):
						tokens.RemoveAt(i);
						continue;
				}
				i++;
			}
		}

		public static void TransformImplicitMult(ref List<Token> tokens) {
			List<Token> transformed = new List<Token>();
			int         i           = 0;
			while (i < tokens.Count) {
				Token token = tokens[i];
				if (token is Number) {
					if (i < tokens.Count - 1) {
						if (tokens[i + 1] is LeftParenthesis || tokens[i + 1] is Symbol) {
							transformed.Add(new Multiply());
						}
					}
				}
				transformed.Add(token);
				i++;
			}
			tokens.Clear();
			tokens.AddRange(transformed);
		}
	}
}