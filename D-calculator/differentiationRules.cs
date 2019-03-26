using System;
using System.Collections.Generic;

namespace D_calculator
{
    class differentiationRules{
        public bool isConstant(Node node){
            if(Int32.TryParse(node.data, out int num)){
                return true;
            }else{return false;}
        }
        public Node derivateVariable(Node branch){
            Node n = new Node("1");
            return n;
        }

        public Node rule(Node branch){
            switch(branch.data){
                case "^":
                    return powerDerivative(branch);
                case "/":
                    return quotientRule(branch);
                case "*":
                   return productRule(branch);
                case "+":
                    return sumRule(branch);
                case "-":
                    return substractionRule(branch);
                default:
                    return branch;
            }
        }
        public Node powerDerivative(Node branch){
            if (!isConstant(branch.left)){
                if (isConstant(branch.right)){
                    branch.data = $"({branch.right.data}*{branch.left.data})^{(Int16.Parse(branch.right.data) - 1).ToString()}";
                }else { branch.data = $"({branch.left.data}*{branch.right.data})^{branch.right.data}-1"; }
                branch.left = branch.right = null;
            }else if(!isConstant(branch.right)){
                branch.data = $"({branch.left.data}^{branch.right.data})ln({branch.left.data})";
                branch.left = branch.right = null;
            }else{
                branch.data = Math.Pow(Double.Parse(branch.left.data),Double.Parse(branch.right.data)).ToString();
            }
            
            return branch;
        }
        public Node quotientRule(Node branch){
            string answer = "", beforeDerivative = branch.left.data;
            if (!isConstant(branch.left)){
                derivateVariable(branch.left);
                answer += $"{branch.left.data}*{branch.right.data}";
                branch.left = null;
                
                if (!isConstant(branch.right)){
                derivateVariable(branch.right);
                answer += $" - {beforeDerivative}*{branch.right.data}";
                branch.data = $"{answer}/{branch.right.data}^2";
                branch.right = null;
                }else{
                    branch.data = $"{answer}/{branch.right.data}^2";
                }

            }else if(!isConstant(branch.right)){
                answer = branch.right.data;
                derivateVariable(branch.right);
                branch.data = $"-{branch.left.data}*{branch.right.data}/{answer}^2";
            }else if(Int16.Parse(branch.right.data) != 0) {
                branch.data = (Double.Parse(branch.left.data)/Double.Parse(branch.right.data)).ToString();
            }else{
                branch.data = "0";
            }
            return branch;
        }

        public Node productRule(Node branch){
            string beforeDerivative = "";
            if (!isConstant(branch.left)){
                beforeDerivative = branch.left.data;
                derivateVariable(branch.left);
                branch.data += $"{branch.left.data}*{branch.right.data}";

                if (!isConstant(branch.right)){
                    derivateVariable(branch.right);
                    branch.data += $" + {beforeDerivative}*{branch.right.data}";
                }
            }else if(!isConstant(branch.right)){
                derivateVariable(branch.right);
                branch.data += $"{branch.left.data}*{branch.right.data}";
            }
            
            if(isConstant(branch.left) && isConstant(branch.right)){
                branch.data = (Int16.Parse(branch.left.data)*Int16.Parse(branch.right.data)).ToString();
            }
            return branch;
        }

        public Node sumRule(Node branch){
            return branch;
        }

        public Node substractionRule(Node branch){
            return branch;
        }
    }
}