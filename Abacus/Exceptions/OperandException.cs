using System;

namespace Abacus.Exceptions {
	public class OperandException: Exception {
		public OperandException() { }

		public OperandException(int pos, object errorObject)
			: base($"Token at position {pos} should be an Operand but instead got: {(errorObject == null ? "EOF" : errorObject.GetType())}") { }
	}
}