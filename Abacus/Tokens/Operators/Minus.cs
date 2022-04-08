namespace Abacus.Tokens.Operators {
    public class Minus : Operator {
        public Minus() : base(2, Precedence.Minus, "-", Associativity.Left) {
        }

        public override Operand Evaluate(Operand op1, Operand op2) {
            return new Number(op1.Value - op2.Value);
        }
    }
}