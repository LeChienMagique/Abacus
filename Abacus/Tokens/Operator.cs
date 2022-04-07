namespace Abacus.Tokens {
    public abstract class Operator : Token {
        private int arity;
        private Precedence precedence;
        private Operand[] operands;

        public int Arity => arity;
        public Precedence Precedence => precedence;

        public Operator(int arity, Precedence precedence) {
            this.arity = arity;
            this.precedence = precedence;
        }

        public Operand Evaluate() {
            return null;
        }
    }
}