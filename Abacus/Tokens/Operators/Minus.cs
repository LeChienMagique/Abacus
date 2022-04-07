namespace Abacus.Tokens.Operators {
    public class Minus : Operator {
        public Minus() : base(2, Precedence.Minus) {
        }

        public override Operand Evaluate(Operand op1, Operand op2) {
            // TODO
            throw new System.NotImplementedException();
        }
    }
}