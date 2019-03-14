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
            }else if(!isConstant(branch)){
                branch = derivateVariable(branch);
            }else{branch = null;}
            
        }
         bool isConstant(Node node){
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
                    powerDerivative(branch);
                    break;
                case "/":
                    quotientRule(branch);
                    break;
                case "*":
                   productRule(branch);
                    break;
                case "+":

                    break;
                case "-":
                    break;

            }
        }
        public void powerDerivative(Node branch){
            if (!isConstant(branch.left)){
                if (isConstant(branch.right)){
                    branch.data = $"({branch.left.data}*{branch.right.data})^{(Int16.Parse(branch.right.data) - 1).ToString()}";
                }else { branch.data = $"({branch.left.data}*{branch.right.data})^{branch.right.data}-1"; }
                branch.left = branch.right = null;
            }else{branch = null;}
        }
        public void quotientRule(Node branch){
            string answer = "", beforeDerivative = branch.left.data;
            if (!isConstant(branch.left)){
                derivateVariable(branch.left);
                answer += $"{branch.left.data}*{branch.right.data} -";
                branch.left = null;
            }
            if (!isConstant(branch.right)){
                derivateVariable(branch.right);
                answer += $"{beforeDerivative}*{branch.right.data}";
                branch.data = $"{answer}/{branch.right.data}^2";
                branch.right = null;
            }
            if (isConstant(branch.left) && isConstant(branch.right)){
                branch.data = (Int16.Parse(branch.left.data)/Int16.Parse(branch.right.data)).ToString();
            }
        }

        public void productRule(Node branch){
            string beforeDerivative = "";
            if (!isConstant(branch.left)){
                beforeDerivative = branch.left.data;
                derivateVariable(branch.left);
                branch.data += $"{branch.left.data}*{branch.right.data} +";
            }
            if (!isConstant(branch.right)){
                derivateVariable(branch.right);
                branch.data += $"{beforeDerivative}*{branch.right.data}";
            }
        }

    }
}