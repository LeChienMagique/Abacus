using System;
using System.Collections.Generic;
using Abacus.Exceptions;
using Abacus.Tokens;

namespace Abacus.Bonus {
    public class Repl {
        private Interpreter interpreter;

        public Repl() {
            interpreter = new Interpreter();
        }

        public static void Run() {
            Repl repl = new Repl();
            string input;
            int result;

            while (true) {
                Console.Write(">>> ");
                input = Console.ReadLine();
                if (input is null)
                    throw new SyntaxErrorException();
                result = repl.Evaluate(input);
                Console.WriteLine($"> {result}");
            }
        }

        public int Evaluate(string input) {
            int? lastResult = new int();
            foreach (string expr in input.Split(';')) {
                List<Token> tokens = Lexer.Lex(expr, false);
                List<Token> rpnNotation = ShuntingYard.ToRpn(tokens);

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

            return lastResult.Value;
        }
    }
}