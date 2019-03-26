using System;
using System.Collections.Generic;


namespace D_calculator
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
            infixToPostfix translate = new infixToPostfix();
            Console.WriteLine("Insert equation:");
            string[] tokens = translate.tokenize(Console.ReadLine());
            stack = translate.shuntingYard(tokens);

            List <string> reversePolishNotation = new List<string>(stack); 
            differentiate derivate = new differentiate();
            Node tree = derivate.buildTree(reversePolishNotation);
            Node derivatedTree = derivate.solveDerivatives(tree);
            string finalEquation = derivate.turnIntoString(derivatedTree);

            Console.WriteLine(finalEquation);
        }

    }

}
