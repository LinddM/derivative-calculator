using System;
using System.Collections.Generic;

namespace Derivative_calculator
{
    class D
    {
        public void solve(Node branch){
            if (branch != null){
                solve(branch.left);
                solve(branch.right);
            }

            if(isOperator(branch.data)){
                rules(branch);
            }else if(!constant(branch)){
                branch = derivateVariable(branch);
            }else{branch = null;}
        }
         bool constant(Node node){
            if(Int32.TryParse(node.data, out int num)){
                return true;
            }else{return false;}
        }
        public  bool isOperator(string c){
            bool symbol = false;
            string [] operators = {"^", "*", "/", "+", "-"};
            foreach(string o in operators){
                if(c == o){
                    symbol = true;
                    break;
                }
            }
            return symbol;
        } 
        public  Node derivateVariable(Node branch){
            Node n = new Node("(1)");
            return n;
        }
        public  void rules(Node branch){
            switch(branch.data){
                case "^":
                //regla de 
                    if(!constant(branch.left)){
                        if(constant(branch.right)){
                            branch.data = $"({branch.left.data}*{branch.right.data})^{(Int16.Parse(branch.right.data) - 1).ToString()}";
                        }else{ branch.data = $"({branch.left.data}*{branch.right.data})^{branch.right.data}-1"; }
                        branch.left = branch.right = null;
                    }else{
                        branch = null;
                    }
                    break;
                case "/":
                    string answer = "", beforeDerivative = branch.left.data;
                    if (!constant(branch.left)){
                        variable(branch.left);
                        answer += $"{branch.left.data}*{branch.right.data} -";
                    }
                    branch.left = null;
                    if (!constant(branch.right)){
                        variable(branch.right);
                        answer += $"{beforeDerivative}*{branch.right.data}";
                    }
                    branch.data = $"{answer}/{branch.right.data}^2";
                    branch.right = null;

                    if (constant(branch.left) && constant(branch.right)){
                        branch = null;
                    }
                    break;
                case "*":
                   // branch.data = productRule(branch);
                    branch.left = branch.right = null;
                    break;
                case "+":
                    break;
                case "-":
                    break;

            }
        }
    }
}