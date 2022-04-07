namespace Abacus.Tokens {
    public class Exponent : Operator {
        public Exponent() : base(2, Precedence.Exponent) {
        }

        public override Operand Evaluate() {
            // TODO
            throw new System.NotImplementedException();
        }
    }
}