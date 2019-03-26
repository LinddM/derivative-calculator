using System;
using System.Collections.Generic;

namespace D_calculator
{
    class differentiate{
        differentiationRules applyRules = new differentiationRules();
        infixToPostfix check = new infixToPostfix();
        public Node buildTree(List <string> reversePolishNotation){
            
            Stack<Node> stack = new Stack<Node>();
            foreach(string item in reversePolishNotation){
                if(!check.isOperator(item[0])){
                    stack.Push(new Node(item));
                }else{
                    stack.Push(new Node (item){
                        right = stack.Pop(),
                        left = stack.Pop()
                    });
                }
            }
            Node buildedTree = stack.ToArray()[0];
            return buildedTree;
        }

        public Node solveDerivatives(Node branch){
            if(branch != null){
                solveDerivatives(branch.left);
                solveDerivatives(branch.right);

                if(check.isOperator(branch.data[0])){
                    branch = applyRules.rule(branch);
                }else if(!applyRules.isConstant(branch)){
                    branch = applyRules.derivateVariable(branch);
                }
            }
            return branch;
        }

        public string turnIntoString(Node derivatedBranch){
            string equation = "";
            if(derivatedBranch != null){
                turnIntoString(derivatedBranch.left);
                turnIntoString(derivatedBranch.right);

                equation += derivatedBranch.data;
            }
            return equation;
        }

    }
    
}