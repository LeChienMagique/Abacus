namespace Abacus.Tokens {
    public abstract class Operand : Token {
        protected int value;

        public int Value => value;

        public Operand(int value) {
            this.value = value;
        }
    }
}