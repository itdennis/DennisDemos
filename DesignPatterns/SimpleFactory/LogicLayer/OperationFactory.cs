using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleFactory.LogicLayer
{
    public class OperationFactory
    {
        public static Operation CreateOperation(string op)
        {
            Operation operation = null;
            switch (op)
            {
                case "+":
                    operation = new OperationAdd();
                    break;
                case "-":
                    operation = new OperationSub();
                    break;
                default:
                    break;
            }
            return operation;
        }
    }
}
