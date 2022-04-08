using System;
using System.Collections.Generic;
using Abacus.Tokens;
using Abacus.Tokens.Operators;

namespace Abacus {
	public static class Program {
		public static int Main(string[] args) {
			bool rpnMode = false;
			for (int i = 0 ; i < args.Length ; ++i) {
				// Console.WriteLine("argument {0}: {1}", i, args[i]);
				if (args[i] == "--rpn") {
					rpnMode = true;
				}
			}

			List<Token> tokens = Lexer.Lex(Console.ReadLine());
			List<Token> rpnNotation;
			if (!rpnMode) {
				Lexer.TransformUnaryOperators(ref tokens);
				rpnNotation = ShuntingYard.ToRpn(tokens);
			}
			else {
				rpnNotation = tokens;
			}

			// foreach (Token token in rpnNotation) {
			// 	if (token is Operand)
			// 		Console.Write(((Operand) token).Value + " ");
			// 	if (token is Operator)
			// 		Console.Write(((Operator) token).HumanReadable + " ");
			// }
			// Console.WriteLine($"Result : {Interpreter.Interpret(rpnNotation)}");
			Console.WriteLine(Interpreter.Interpret(rpnNotation));

			// Console.WriteLine(Console.ReadLine());
			// Returns an error code of 0, everything went fine!
			return 0;
		}
	}
}