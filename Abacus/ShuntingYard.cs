using System;
using System.Collections.Generic;
using Abacus.Exceptions;
using Abacus.Tokens;
using Abacus.Tokens.Operators;

namespace Abacus {
	public static class ShuntingYard {
		public static List<Token> ToRpn(List<Token> infixInput) {
			List<Token>  output  = new List<Token>();
			Stack<Token> opStack = new Stack<Token>();
			foreach (Token token in infixInput) {
				switch (token) {
					case Comma:
						while (opStack.Count > 0 && !(opStack.Peek() is LeftParenthesis)) {
							output.Add(opStack.Pop());
						}
						if (!(opStack.Peek() is LeftParenthesis)) throw new MismatchedParenthesesException();
						break;
					case Function func:
						opStack.Push(func);
						break;
					case Symbol sym:
						output.Add(sym);
						break;
					case Operand operand:
						output.Add(operand);
						break;
					case LeftParenthesis:
						opStack.Push(token);
						break;
					case RightParenthesis:
						while (opStack.Count > 0 && !(opStack.Peek() is LeftParenthesis)) {
							output.Add(opStack.Pop());
						}

						if (opStack.Count == 0 || !(opStack.Peek() is LeftParenthesis))
							throw new MismatchedParenthesesException("Mismatched parentheses.");
						opStack.Pop();
						// TODO : function part of algorithm
						if (opStack.Count > 0 && opStack.Peek() is Function)
							output.Add(opStack.Pop());
						break;
					case Operator op1:
						while (opStack.Count > 0) {
							Token op2 = opStack.Peek();
							// don't ask me why all this, it was on wikipedia
							if (op2 is Operator OPop2 && !(OPop2 is LeftParenthesis) &&
							    (OPop2.Precedence > op1.Precedence ||
							     (OPop2.Precedence == op1.Precedence &&
							      op1.Associativity is Associativity.Left))) {
								output.Add(opStack.Pop());
								continue;
							}

							break;
						}

						opStack.Push(op1);
						break;
				}
			}

			while (opStack.Count > 0) {
				if (!(opStack.Peek() is Operator)) {
					throw new SyntaxErrorException();
				}

				if (opStack.Peek() is LeftParenthesis)
					throw new MismatchedParenthesesException("Mismatched parentheses.");
				output.Add(opStack.Pop());
			}

			return output;
		}
	}
}