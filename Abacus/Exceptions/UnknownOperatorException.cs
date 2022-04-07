using System;

namespace Abacus.Exceptions {
	public class UnknownOperatorException: Exception {
		public UnknownOperatorException() { }
		public UnknownOperatorException(string message): base(message) { }
	}
}