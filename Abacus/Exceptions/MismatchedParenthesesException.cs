using System;

namespace Abacus.Exceptions {
	public class MismatchedParenthesesException : Exception {
		public MismatchedParenthesesException() { }
		public MismatchedParenthesesException(string message): base(message) { }
	}
}