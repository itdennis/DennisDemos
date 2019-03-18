using System.ComponentModel.Composition;

namespace ExtendedMethod
{
    [Export(typeof(IOperation))]
    [ExportMetadata("Symbol", '+')]
    class Add : IOperation
    {
        public int Operate(int left, int right)
        {
            return left + right;
        }
    }

    [Export(typeof(IOperation))]
    [ExportMetadata("Symbol", '-')]
    class Subtract : IOperation
    {

        public int Operate(int left, int right)
        {
            return left - right;
        }

    }

    [Export(typeof(IOperation))]
    [ExportMetadata("Symbol", '%')]
    public class Mod : IOperation
    {
        public int Operate(int left, int right)
        {
            return left % right;
        }
    }

    [Export(typeof(IOperation))]
    [ExportMetadata("Symbol", '*')]
    public class Cheng : IOperation
    {
        public int Operate(int left, int right)
        {
            return left * right;
        }
    }
}
