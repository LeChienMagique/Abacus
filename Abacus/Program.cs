﻿using System;
using System.Collections.Generic;
using System.Linq;
using Abacus.Exceptions;
using Abacus.Tokens;
using Abacus.Tokens.Operators;
using ArithmeticException = Abacus.Exceptions.ArithmeticException;
using SyntaxErrorException = Abacus.Exceptions.SyntaxErrorException;

namespace Abacus {
	public static class Program {
		private static string[] validArgs = new[] {"--rpn"};

		public static void _Main(string[] args) {
			bool rpnMode = false;
			for (int i = 0 ; i < args.Length ; ++i) {
				if (validArgs.Contains(args[i])) {
					// TODO : works for now
					rpnMode = true;
				}
				else {
					throw new UnknownArgumentException($"Unknown Argument: '{args[i]}'");
				}
			}
			string      input       = Console.ReadLine();
			Interpreter interpreter = new Interpreter();
			int?        lastResult  = new int();
			foreach (string expr in input.Split(';')) {
				List<Token> tokens = Lexer.Lex(expr);
				List<Token> rpnNotation;
				if (!rpnMode) {
					Lexer.TransformUnaryOperators(ref tokens);
					Lexer.TransformImplicitMult(ref tokens);
					rpnNotation = ShuntingYard.ToRpn(tokens);
				}
				else {
					rpnNotation = tokens;
				}

				// foreach (Token token in rpnNotation) {
				// 	Console.Write(token.HumanReadable + " ");
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
						// Console.WriteLine(e.Message);
						// Console.WriteLine(e.StackTrace);
						Console.Error.WriteLine("Syntax error.");
						System.Environment.Exit(2);
						break;
					case UnknownArgumentException argExc:
						Console.Error.WriteLine(argExc.Message);
						System.Environment.Exit(1);
						break;
					case UnboundVariableException:
						Console.Error.WriteLine("Unbound variable.");
						System.Environment.Exit(3);
						break;
					default:
						Console.Error
						       .WriteLine("??? Congrats, you broke the application! Just kidding, this case is just not implemented.");
						System.Environment.Exit(-1);
						break;
				}
			}
			// Returns an error code of 0, everything went fine!
			return 0;
		}
	}
}