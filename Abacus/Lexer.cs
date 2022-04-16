using System;
using System.Collections.Generic;
using System.Linq;
using Abacus.Bonus;
using Abacus.Exceptions;
using Abacus.Tokens;
using Abacus.Tokens.NativeFunctions;
using Abacus.Tokens.Operators;
using Abacus.Tokens.UnaryOperators;
using ArithmeticException = Abacus.Exceptions.ArithmeticException;

namespace Abacus {
	public static class Lexer {
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
				_   => throw new UnknownTokenException()
			};
		}

		private static List<string> nativeFunctions = new List<string>()
			{"facto", "sqrt", "max", "min", "isprime", "fibo", "gcd"};

		private static Dictionary<string, Function> definedFunctions = new Dictionary<string, Function>();

		private static bool IsNativeFunction(string tokenString) {
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
				_         => throw new UnknownTokenException()
			};
		}

		private static void AddTokenFromString(List<Token> tokens, string tokenString, bool funcDefEnabled = false) {
			int result;
			if (int.TryParse(tokenString, out result)) {
				tokens.Add(new Number(result));
			}
			else if (IsNativeFunction(tokenString)) {
				tokens.Add(GetCorrespondingFunction(tokenString));
			}
			else if ((funcDefEnabled && definedFunctions.ContainsKey(tokenString))) {
				tokens.Add(definedFunctions[tokenString]);
			}
			else if (char.IsDigit(tokenString[0])) {
				throw new ArithmeticException();
			}
			else {
				tokens.Add(new Symbol(tokenString));
			}
		}

		private static bool IsValidSymbolFirstChar(char c) {
			return char.IsLetter(c) || c == '_';
		}

		private static bool IsValidSymbolChar(char c) {
			return char.IsLetterOrDigit(c) || c == '_';
		}

		private static List<Token> ReLexFunctionDefinition(List<Token> lexed) {
			List<string> args       = new List<string>();
			List<Token>  tokens     = new List<Token>();
			List<Token>  expression = new List<Token>();
			int          i          = 0;
			Symbol       funcName;
			if (!(i < lexed.Count && lexed[i] is Symbol))
				throw new SyntaxErrorException();
			funcName = (Symbol) lexed[i];
			// we are on funcName
			i++;
			if (!(i < lexed.Count && lexed[i] is LeftParenthesis))
				throw new SyntaxErrorException();
			// we are on lPar
			i++;
			while (i < lexed.Count && !(lexed[i] is RightParenthesis)) {
				if (lexed[i] is Symbol) {
					args.Add(((Symbol) lexed[i]).Name);
				}
				else if (!(lexed[i] is Comma)) {
					throw new SyntaxErrorException();
				}
				i++;
			}
			// we are on a rPar
			i++;
			if (!(i < lexed.Count && lexed[i] is Assignment))
				throw new SyntaxErrorException();
			i++;
			while (i < lexed.Count) {
				// if (lexed[i] is Symbol && !(args.Contains(((Symbol) lexed[i]).Name))) {
				// throw new UnboundVariableException();
				// }
				expression.Add(lexed[i]);
				i++;
			}
			expression = TransformImplicitMult(TransformUnaryOperators(expression));
			tokens.Add(new FunctionDefinition(funcName.Name, args, expression));
			definedFunctions.Add(funcName.Name, new UDefinedFunction(args.Count, funcName.Name, args, expression));
			return tokens;
		}

		private static List<Token> _Lex(string input, bool funcDefEnabled = false) {
			List<Token> tokens               = new List<Token>();
			int         i                    = 0;
			string      numberString         = "";
			string      symbolString         = "";
			bool        isFunctionDefinition = false;
			while (i < input.Length && input[i] == ' ')
				i++;
			if (funcDefEnabled && i < input.Length && input[i] == '#') {
				isFunctionDefinition = true;
				// Console.WriteLine("here");
				i++;
				// LexFunctionDefinition(input.Remove(0, i + 1));
			}
			while (i < input.Length) {
				char currChar = input[i];
				if (currChar == ' ') {
					// add number to list
					if (!string.IsNullOrEmpty(numberString)) {
						AddTokenFromString(tokens, numberString);
						numberString = "";
					}
					else if (!string.IsNullOrEmpty(symbolString)) {
						AddTokenFromString(tokens, symbolString, funcDefEnabled);
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
					AddTokenFromString(tokens, symbolString, funcDefEnabled);
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
				AddTokenFromString(tokens, symbolString, funcDefEnabled);
			}
			if (isFunctionDefinition)
				return ReLexFunctionDefinition(tokens);
			return tokens;
		}

		private static List<Token> TransformUnaryOperators(List<Token> tokens) {
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
					case Plus when i == 0 ||
					               tokens[i - 1] is Operator && !(tokens[i - 1] is RightParenthesis):
						tokens.RemoveAt(i);
						continue;
					case Plus when i == tokens.Count - 1:
						throw new SyntaxErrorException();
				}

				i++;
			}

			return tokens;
		}

		private static List<Token> TransformImplicitMult(List<Token> tokens) {
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

			return transformed;
		}

		public static List<Token> Lex(string input, bool rpnMode, bool funcDefMode) {
			if (!rpnMode) {
				return TransformImplicitMult(TransformUnaryOperators(_Lex(input, funcDefMode)));
			}

			return _Lex(input);
		}
	}
}