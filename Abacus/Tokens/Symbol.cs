using System;
using System.Collections.Generic;
using Abacus.Tokens.Operators;

namespace Abacus.Tokens {
	public class Symbol: Token {
		private string name;
		public  string Name => name;

		public Symbol(string value) {
			humanReadable = value;
			name          = value;
		}

		public void EvaluateSymbol(Stack<Token> stack, Dictionary<string, int> context) {
			if (!context.ContainsKey(Name)) {
				stack.Push(this);
				return;
			}
			stack.Push(new Variable(context[Name], Name));
		}
	}
}