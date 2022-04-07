namespace Abacus.Tokens.Operators {
    public class Exponent : Operator {
        public Exponent() : base(2, Precedence.Exponent) {
        }

        public override Operand Evaluate(Operand op1, Operand op2) {
            // TODO
            throw new System.NotImplementedException();
        }
    }
}