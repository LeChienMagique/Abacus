using System;

namespace Abacus.Exceptions {
	public class SyntaxErrorException : Exception {
		public SyntaxErrorException(): base() { }
		public SyntaxErrorException(string msg): base(msg) { }
	}
}