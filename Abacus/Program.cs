using System;
using System.Collections.Generic;
using System.Linq;
using Abacus.Bonus;
using Abacus.Exceptions;
using Abacus.Tokens;
using Abacus.Tokens.Operators;
using ArithmeticException = Abacus.Exceptions.ArithmeticException;
using SyntaxErrorException = Abacus.Exceptions.SyntaxErrorException;

namespace Abacus {
	public static class Program {
		private static string[] validBonusArgs = {"REPL"};

		private static void _Main(string[] args) {
			bool rpnMode     = false;
			bool replMode    = false;
			bool funcDefMode = false;
			for (int i = 0 ; i < args.Length ; ++i) {
				if (args[i].StartsWith("--additionals=")) {
					string[] bonusArgs = args[i].Remove(0, 14).Split(",");
					foreach (string bonusArg in bonusArgs) {
						switch (bonusArg) {
							case "REPL":
								replMode = true;
								break;
							case "FUNCDEF":
								funcDefMode = true;
								break;
							default:
								throw new UnknownArgumentException();
						}
					}
				}

				else if (args[i] == "--rpn") {
					rpnMode = true;
				}
				else {
					throw new UnknownArgumentException($"Unknown Argument: '{args[i]}'");
				}
			}

			if (replMode) {
				Repl.Run(funcDefMode);
			}
			else {
				EvaluateExpression(rpnMode, funcDefMode);
			}
		}

		private static void EvaluateExpression(bool rpnMode, bool funcDefMode) {
			string      input       = Console.ReadLine();
			Interpreter interpreter = new Interpreter();
			int?        lastResult  = new int();
			foreach (string expr in input.Split(';')) {
				List<Token> tokens = Lexer.Lex(expr, rpnMode, funcDefMode);
				// foreach (Token token in tokens) {
				// Console.Write(token.HumanReadable + " ");
				// }
				// means expression returned nothing (e.g function definition)
				if (tokens.Count == 0)
					return;
				List<Token> rpnNotation;
				if (!rpnMode) {
					rpnNotation = ShuntingYard.ToRpn(tokens);
				}
				else {
					rpnNotation = tokens;
				}

				// foreach (Token token in rpnNotation) {
				// Console.Write(token.HumanReadable + " ");
				// }
				// Console.WriteLine("\n===========");
				lastResult = interpreter.Interpret(rpnNotation);
			}

			if (!lastResult.HasValue) {
				throw new
					Exception("Something really wrong happened : evaluation of the given expression returned `null`.");
			}

			Console.WriteLine(lastResult);
		}

		public static int Main(string[] args) {
			try {
				_Main(args);
			}
			catch (Exception e) {
				switch (e) {
					case ArithmeticException:
						Console.Error.WriteLine("Invalid operation.");
						System.Environment.Exit(3);
						break;
					case MismatchedParenthesesException:
					case SyntaxErrorException:
						Console.Error.WriteLine("Syntax error.");
						System.Environment.Exit(2);
						break;
					case UnknownArgumentException argExc:
						Console.Error.WriteLine("Unknown argument.");
						System.Environment.Exit(1);
						break;
					case UnboundVariableException:
						Console.WriteLine(e.StackTrace);
						Console.Error.WriteLine("Unbound variable.");
						System.Environment.Exit(3);
						break;
					case UnknownTokenException:
						Console.Error.WriteLine("Unexpected token.");
						System.Environment.Exit(2);
						break;
					default:
						Console.Error
						       .WriteLine(
						                  "??? Congrats, you broke the application! Just kidding, this case is just not implemented.");
						System.Environment.Exit(-1);
						break;
				}
			}

			// Returns an error code of 0, everything went fine!
			return 0;
		}
	}
}