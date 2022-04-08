using System;
using System.Collections.Generic;
using Abacus.Tokens;
using Abacus.Tokens.Operators;
using Abacus.Tokens.UnaryOperators;

namespace Abacus {
	public class Interpreter {
		public static int Interpret(List<Token> rpnInput) {
			Stack<Token> outputStack = new Stack<Token>();
			foreach (Token token in rpnInput) {
				switch (token) {
					case Operand:
						outputStack.Push(token);
						break;
					case Operator op:
						if (op.Arity == 1) {
							if (outputStack.Count < 1)
								throw new ArgumentException($"Not enough operands for operator: `{op.HumanReadable}`");
							Operand ope = (Operand) outputStack.Pop();
							outputStack.Push(((UnaryOperator) op).PerformUnaryOperation(ope));
						}
						else if (op.Arity == 2) {
							if (outputStack.Count < 2)
								throw new ArgumentException($"Not enough operands for operator: `{op.HumanReadable}`");
							Operand ope2 = (Operand) outputStack.Pop();
							Operand ope1 = (Operand) outputStack.Pop();
							outputStack.Push(op.Evaluate(ope1, ope2));
						}
						else {
							throw new
								NotImplementedException("Unreachable, operators with arity != 2 are not currently implemented.");
						}
						break;
				}
			}
			if (outputStack.Count == 0)
				throw new Exception("Too much tokens were consumed, sus.");
			if (outputStack.Count > 1) {
				while (outputStack.Count > 0)
					Console.Write(outputStack.Pop().HumanReadable + " | ");
				throw new Exception("Some tokens were not consumed while interpreting, sus.");
			}
			Token result = outputStack.Pop();
			if (!(result is Operand))
				throw new Exception($"Expected an operand to be left on stack but got: {result.GetType()}");
			return ((Operand) result).Value;
		}
	}
}