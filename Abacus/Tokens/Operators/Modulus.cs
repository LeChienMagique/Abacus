namespace Abacus.Tokens.Operators {
    public class Modulus : Operator {
        public Modulus() : base(2, Precedence.Modulus) {
        }

        public override Operand Evaluate(Operand op1, Operand op2) {
            // TODO
            throw new System.NotImplementedException();
        }
    }
}