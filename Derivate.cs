using System;
using System.Collections.Generic;

namespace Derivative_calculator
{
    class Derivate {
        //Podria esquematizar mejor si es constante, variable u operacion
        public void readbranch(Node branch){
            if(isOperator(branch.data)){
                solve(branch);
            }else if(!isOperator(branch.data) && !constant(branch)){
                variable(branch);
            }

        }
        static void solve(Node branch){
            //problems here
            //puedo borrar left y right desde aca
            if(branch.left != null && branch.right != null){
               if(!isOperator(branch.left.data)){
                rules(branch);
                }
                if(!isOperator(branch.right.data)){
                    rules(branch);
                } 
            }
        }
        static bool constant(Node node){
            if(Int32.TryParse(node.data, out int num)){
                return true;
            }else{return false;}
        }
        static void variable(Node branch){
            branch.data = "(1)";
        }
        public static bool isOperator(string c){
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
        public static void rules(Node branch){
            switch(branch.data){
                case "^":
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
                    quotientRule(branch);
                    break;
                case "*":
                    branch.data = productRule(branch);
                    branch.left = branch.right = null;
                    break;
                case "+":
                    break;
                case "-":
                    break;

            }
        }
        static void quotientRule(Node branch){
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
            branch.data =  $"{answer}/{branch.right.data}^2";
            branch.right = null;

            if(constant(branch.left) && constant(branch.right)){
                branch = null;
            }
        }
        static string productRule(Node branch){
            string answer = "", beforeDerivative = "";
            if (!constant(branch.left)){
                beforeDerivative = branch.left.data;
                variable(branch.left);
                answer += $"{branch.left.data}*{branch.right.data} +";
            }
            if (!constant(branch.right)){
                variable(branch.right);
                answer += $"{beforeDerivative}*{branch.right.data}";
            }
            return answer;
        }
    }
}