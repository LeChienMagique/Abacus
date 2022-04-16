using System;
using System.Collections.Generic;
using Abacus.Exceptions;
using Abacus.Tokens;
using Abacus.Tokens.Operators;

namespace Abacus.Bonus {
	public class UDefinedFunction: Function {
		private List<string> args;
		private List<Token>  expression;

		public UDefinedFunction(int arity, string humanReadable, List<string> args, List<Token> expression):
			base(arity, humanReadable) {
			this.args       = args;
			this.expression = ShuntingYard.ToRpn(expression);
		}

		public override void Evaluate(Stack<Token> stack) {
			Interpreter funcInterpreter = new Interpreter();
			CheckOperandsCount(stack);
			for (int i = 0 ; i < args.Count ; i++) {
				Token tok = stack.Pop();
				CheckInvalidArguments(tok);
				funcInterpreter.Context[args[args.Count - i - 1]] = ((Operand) tok).Value;
			}
			int result = funcInterpreter.Interpret(expression);
			stack.Push(new Number(result));
		}
	}
}