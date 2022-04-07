namespace Abacus.Tokens {
    public class Plus : Operator {
        public Plus() : base(2, Precedence.Addition) {
        }
    }
}