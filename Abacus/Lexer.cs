using System;
using System.Collections.Generic;
using Abacus.Tokens;

namespace Abacus {
    public class Lexer {
        private string input;

        // TODO : think of a way to specify if bonuses are enabled
        public Lexer(string input) {
            this.input = input;
        }

        private Operator GetCorrespondingOperator(char opChar) {
            return opChar switch {
                '+' => new Plus(),
                '-' => new Minus(),
                '*' => new Multiply(),
                '/' => new Division(),
                '^' => new Exponent(),
                '%' => new Modulus(),
                _ => throw new Exception("unknown operator")
            };
        }

        public Token[] Lex() {
            /* TODO :
             *  - detect unary minus/plus
             *  -
             */
            List<Token> tokens = new List<Token>();
            int i = 0;
            string tokenString = "";
            while (i < input.Length) {
                char currChar = input[i];

                // detect number
                if (char.IsDigit(currChar)) {
                    tokenString += currChar;
                    continue;
                }

                // add number to list
                if (!string.IsNullOrEmpty(tokenString))
                    tokens.Add(new Number(int.Parse(tokenString)));
            }

            return null;
        }
    }
}