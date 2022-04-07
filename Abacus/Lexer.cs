using Abacus.Tokens;

namespace Abacus {
    public class Lexer {
        private string input;

        // TODO : think of a way to specify if bonuses are enabled
        public Lexer(string input) {
            this.input = input;
        }

        public Token[] Lex() {
            int i = 0;
            string token_string = "";
            while (i < input.Length) {
            }

            return null;
        }
    }
}