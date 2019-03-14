using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Derivative_calculator
{
    class Node{
        public string data;
        public Node left, right;
        public Node(string data){
            this.data=data;
        }
       
    }
    class Program
    {
        public static string [] stack;
        
        static void Main(string[] args)
        {
            Console.WriteLine("Insert equation:");
            string[] tokens = tokenize(Console.ReadLine());
            stack = shuntingYard(tokens);
            List <string> rpn = new List<string>(stack); 

            D derivatives = new D();
            derivatives.solve(buildTree(rpn));

            foreach(string e in stack){
                Console.Write(e);
            }
        }

        public static Node buildTree(List <string> rpn){
            Stack<Node> stack = new Stack<Node>();
            foreach(string item in rpn){
                if(!isOperator(item[0])){
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

        public static string[] tokenize(string str){
            List<string> tokens = new List<string>();
            string token = "";
            for (int i = 0; i < str.Length; i++){
                if (isOperator(str[i]) && !String.IsNullOrWhiteSpace(token)){
                    tokens.Add(token);
                    token = "";
                }
                token += str[i];
                if (isOperator(token[0])){
                    tokens.Add(token);
                    token = "";
                }
            }
            if (!String.IsNullOrWhiteSpace(str[str.Length - 1].ToString()) && token != ""){
                tokens.Add(token);
            }

            return tokens.ToArray();
        }

        public static string [] shuntingYard(string [] elements){
            List<string> output = new List <string>();
            List<string> operatorsStack = new List <string>();
            char before = ' ';
                //just add parentheses
                foreach(string e in elements){
                    if(!isOperator(e[0])){
                        output.Add(e);
                    }else{
                        doOperators(e[0], before, output, operatorsStack);
                        before = e[0];
                    }
                }
                operatorsStack.Reverse();
                output.AddRange(operatorsStack);
                string [] all = output.ToArray();
            return all;
        }

        public static bool isOperator(char c){
            bool symbol = false;
            char [] operators = {'^', '*', '/', '+', '-'};
            foreach(char o in operators){
                if(c == o){
                    symbol = true;
                    break;
                }
            }
            return symbol;
        }
        public static void doOperators(char e, char before, List<string> output, List<string> operatorsStack){
            int ePriority = priority(e);
            int beforePriority = priority(before);

            while(ePriority<beforePriority || (ePriority==beforePriority && e!='^')){
                output.Add(before.ToString());  
                operatorsStack.Remove(before.ToString());
                if (operatorsStack.Count != 0){
                    before = operatorsStack[operatorsStack.Count - 1][0];
                    beforePriority = priority(before);
                }else{
                    break;
                }
            }
            operatorsStack.Add(e.ToString());
        }

        public static int priority(char c){
            int priority = 0;
            switch(c){
                case '^':
                    priority = 3;
                    break;
                case '/':
                    priority = 2;
                    break;
                case '*':
                    priority = 2;
                    break;
                case '+':
                    priority = 1;
                    break;
                case '-':
                    priority = 1;
                    break;

            }
            return priority;
        }
    }
}