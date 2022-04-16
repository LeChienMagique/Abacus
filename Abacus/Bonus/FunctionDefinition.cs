using System.Collections.Generic;
using Abacus.Tokens;
using Abacus.Tokens.Operators;

namespace Abacus.Bonus {
	public class FunctionDefinition: Operator {
		private string       funcName;
		private List<string> funcArgs;
		private List<Token>  funcExpression;

		public FunctionDefinition(string funcName, List<string> funcArgs, List<Token> funcExpression):
			base(0, Precedence.Comma, "FUNC_DEF", Associativity.None) {
			this.funcName = funcName;
			this.funcArgs = funcArgs;
			// this.funcExpression = ShuntingYard.ToRpn(funcExpression);
			this.funcExpression = funcExpression;
		}

		public void EvaluateFunctionDefinition(Dictionary<string, Function> functionContext) {
			UDefinedFunction func = new UDefinedFunction(funcArgs.Count, funcName, funcArgs, funcExpression);
			functionContext[funcName] = func;
		}

		public override void Evaluate(Stack<Token> stack) => throw new System.NotImplementedException();
	}
}