using System;

namespace AdapterDemo
{
    class Client
    {
        static void Main(string[] args)
        {
            Target adapterTarget = new Adapter();
            adapterTarget.Request();
        }
    }
}
