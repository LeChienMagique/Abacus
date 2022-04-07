namespace Abacus.Tokens {
    public class UnaryMinus : Operator {
        public UnaryMinus() : base(1, Precedence.UnaryMinus) {
        }

        public override Operand Evaluate() {
            // TODO
            throw new System.NotImplementedException();
        }
    }
}