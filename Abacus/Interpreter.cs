using System;
using System.Collections.Generic;
using System.Data;
using Abacus.Bonus;
using Abacus.Exceptions;
using Abacus.Tokens;
using Abacus.Tokens.Operators;
using Abacus.Tokens.UnaryOperators;
using SyntaxErrorException = Abacus.Exceptions.SyntaxErrorException;

namespace Abacus {
	public class Interpreter {
		public  Dictionary<string, int>      Context          = new Dictionary<string, int>();
		private Dictionary<string, Function> definedFunctions = new Dictionary<string, Function>();

		public int Interpret(List<Token> rpnInput) {
			Stack<Token> outputStack = new Stack<Token>();
			foreach (Token token in rpnInput) {
				switch (token) {
					case FunctionDefinition funcDef:
						funcDef.EvaluateFunctionDefinition(definedFunctions);
						outputStack.Push(new FuncDefEnd());
						break;
					case Symbol sym:
						sym.EvaluateSymbol(outputStack, Context, definedFunctions);
						break;
					case Assignment assign:
						assign.EvaluateAssignment(outputStack, Context);
						break;
					case Operand op:
						op.Evaluate(outputStack);
						break;
					case UnaryOperator unOp:
						unOp.PerformUnaryOperation(outputStack);
						break;
					case Operator op:
						op.Evaluate(outputStack);
						break;
					default:
						throw new Exception("unreachable");
				}
			}
			if (outputStack.Count == 0)
				throw new SyntaxErrorException("Too much tokens were consumed, sus.");
			if (outputStack.Count > 1) {
				// foreach (Token token in outputStack) {
				// 	Console.Write(token.HumanReadable + " | ");
				// }
				throw new SyntaxErrorException("Some tokens were not consumed while interpreting, sus.");
			}
			Token result = outputStack.Pop();
			if (result is Symbol)
				throw new UnboundVariableException();
			if (result is FuncDefEnd) {
				return 0;
			}
			if (!(result is Operand operand))
				throw new SyntaxErrorException($"Expected an operand to be left on stack but got: {result.GetType()}");
			return operand.Value;
		}
	}
}