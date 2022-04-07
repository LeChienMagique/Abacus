namespace Abacus.Tokens.Operators {
    public class Multiply : Operator {
        public Multiply() : base(2, Precedence.Multiply) {
        }

        public override Operand Evaluate(Operand op1, Operand op2) {
            // TODO
            throw new System.NotImplementedException();
        }
    }
}