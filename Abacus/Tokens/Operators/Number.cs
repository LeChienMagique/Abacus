namespace Abacus.Tokens.Operators {
    public class Number : Operand {
        public Number(int value) : base(value, value.ToString()) {
        }
    }
}