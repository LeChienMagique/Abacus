namespace Abacus.Tokens {
    public class UnaryPlus : Operator {
        public UnaryPlus() : base(1, Precedence.UnaryPlus) {
        }

        public override Operand Evaluate() {
            // TODO
            throw new System.NotImplementedException();
        }
    }
}