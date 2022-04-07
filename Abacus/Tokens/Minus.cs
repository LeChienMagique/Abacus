namespace Abacus.Tokens {
    public class Minus : Operator {
        public Minus() : base(2, Precedence.Minus) {
        }

        public override Operand Evaluate() {
            // TODO
            throw new System.NotImplementedException();
        }
    }
}