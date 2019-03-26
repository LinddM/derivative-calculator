using System.Collections.Generic;
namespace D_calculator{
class infixToPostfix
{
    public string[] tokenize(string str){
            List<string> tokens = new List<string>();
            string token = "";
            for (int i = 0; i < str.Length; i++){
                if (isOperator(str[i]) && !string.IsNullOrWhiteSpace(token)){
                    tokens.Add(token);
                    token = "";
                }
                token += str[i];
                if (isOperator(token[0])){
                    tokens.Add(token);
                    token = "";
                }
            }
            if (!string.IsNullOrWhiteSpace(str[str.Length - 1].ToString()) && token != ""){
                tokens.Add(token);
            }

            return tokens.ToArray();
        }

        public string [] shuntingYard(string [] elements){
            List<string> output = new List <string>();
            List<string> operatorsStack = new List <string>();
            char before = ' ';
                //parentheses missing
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

        public bool isOperator(char c){
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
        public void doOperators(char e, char before, List<string> output, List<string> operatorsStack){
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

        public  int priority(char c){
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