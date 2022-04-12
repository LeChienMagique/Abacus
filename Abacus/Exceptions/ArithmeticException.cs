using System;

namespace Abacus.Exceptions {
	public class ArithmeticException: Exception {
		public ArithmeticException(): base() { }
		public ArithmeticException(string msg): base(msg) { }
	}
}