using System;
using System.Collections.Generic;
using Abacus.Tokens.Operators;

namespace Abacus.Tokens {
	public class Symbol: Operand {
		public Symbol(string value): base(0, value) { }

		public void EvaluateSymbol(Stack<Token> stack, Dictionary<string, int> context) {
			if (!context.ContainsKey(HumanReadable)) {
				stack.Push(this);
				return;
			}
			stack.Push(new Number(context[HumanReadable]));
		}

		public override void Evaluate(Stack<Token> stack) => throw new System.NotImplementedException();
	}
}