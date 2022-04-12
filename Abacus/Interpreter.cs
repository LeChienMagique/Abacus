using System;
using System.Collections.Generic;
using Abacus.Tokens;
using Abacus.Tokens.Operators;
using Abacus.Tokens.UnaryOperators;

namespace Abacus {
	public class Interpreter {
		private Dictionary<string, int> context = new Dictionary<string, int>();

		public int Interpret(List<Token> rpnInput) {
			Stack<Token> outputStack = new Stack<Token>();
			foreach (Token token in rpnInput) {
				switch (token) {
					case Symbol sym:
						sym.EvaluateSymbol(outputStack, context);
						break;
					case Assignment assign:
						assign.EvaluateAssignment(outputStack, context);
						break;
					case Operand op:
						op.Evaluate(outputStack);
						break;
					case Operator op:
						op.Evaluate(outputStack);
						break;
					default:
						throw new Exception("unreachable");
				}
			}
			if (outputStack.Count == 0)
				throw new Exception("Too much tokens were consumed, sus.");
			if (outputStack.Count > 1) {
				throw new Exception("Some tokens were not consumed while interpreting, sus.");
			}
			Token result = outputStack.Pop();
			if (!(result is Operand))
				throw new Exception($"Expected an operand to be left on stack but got: {result.GetType()}");
			return ((Operand) result).Value;
		}
	}
}