using System;

namespace Abacus.Exceptions {
	public class UnknownArgumentException: Exception {
		public UnknownArgumentException(string msg): base(msg) { }
	}
}