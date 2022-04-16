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

		public void EvaluateSymbol(Stack<Token> stack,
		                           Dictionary<string, int> context,
		                           Dictionary<string, Function> funcContext) {
			if (context.ContainsKey(Name)) {
				stack.Push(new Variable(context[Name], Name));
			}
			else if (funcContext.ContainsKey(Name)) {
				stack.Push(funcContext[Name]);
			}
			else {
				stack.Push(this);
			}
		}
	}
}