namespace Abacus.Tokens.Operators {
    public class Multiply : Operator {
        public Multiply() : base(2, Precedence.Multiply, "*", Associativity.Left) {
        }

        public override Operand Evaluate(Operand op1, Operand op2) {
            return new Number(op1.Value * op2.Value);
        }
    }
}