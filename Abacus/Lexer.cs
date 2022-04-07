using System.Collections.Generic;
using Abacus.Tokens;

namespace Abacus {
    public class Lexer {
        private string input;

        // TODO : think of a way to specify if bonuses are enabled
        public Lexer(string input) {
            this.input = input;
        }

        public Token[] Lex(char seprator = ' ') {
            string[] splitted = input.Split(seprator);
            Token[] tokens = new Token[splitted.Length];
            return tokens;
        }
    }
}