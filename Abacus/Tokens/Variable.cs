using System.Collections.Generic;

namespace Abacus.Tokens {
    public class Variable : Operand {
        private string name;
        public string Name => name;

        public Variable(int value, string humanReadable) : base(value, humanReadable) {
            name = humanReadable;
        }

        public override void Evaluate(Stack<Token> stack) {
        }
    }
}