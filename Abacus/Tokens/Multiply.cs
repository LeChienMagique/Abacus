namespace Abacus.Tokens {
    public class Multiply : Operator {
        public Multiply() : base(2, Precedence.Multiply) {
        }

        public override Operand Evaluate() {
            // TODO
            throw new System.NotImplementedException();
        }
    }
}