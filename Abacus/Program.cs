using System;
using System.Collections.Generic;
using Abacus.Tokens;
using Abacus.Tokens.Operators;

namespace Abacus {
	public static class Program {
		public static int Main(string[] args) {
			for (int i = 0 ; i < args.Length ; ++i) {
				Console.WriteLine("argument {0}: {1}", i, args[i]);
			}

			List<Token> tokens = Lexer.Lex("3 4 1 2 + * +");
			foreach (Token token in tokens) {
				if (token is Operand)
					Console.WriteLine(((Operand) token).Value);
				if (token is Operator)
					Console.WriteLine(((Operator) token));
			}

			// Console.WriteLine(Console.ReadLine());
			// Returns an error code of 0, everything went fine!
			return 0;
		}
	}
}