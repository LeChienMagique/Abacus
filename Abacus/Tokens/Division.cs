namespace Abacus.Tokens {
    public class Division : Operator {
        public Division() : base(2, Precedence.Division) {
        }

        public override Operand Evaluate() {
            // TODO
            throw new System.NotImplementedException();
        }
    }
}