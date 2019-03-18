using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Services;

namespace MEFDemo
{
    class Program
    {
        private CompositionContainer _container;

        [Import(typeof(ICalculatorService))]
        public ICalculatorService calculator;

        private Program()
        {
            //An aggregate catalog that combines multiple catalogs
            var catalog = new AggregateCatalog();
            //把从Program所在程序集中发现的部件添加到目录中
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Program).Assembly));
            ////把从指定path发现的部件添加到目录中
            catalog.Catalogs.Add(new DirectoryCatalog(@"C:\Users\v-yanywu\Source\Repos\DennisDemos\IOCDemo\Extensions"));

            //Create the CompositionContainer with the parts in the catalog
            _container = new CompositionContainer(catalog);

            //Fill the imports of this object
            try
            {
                this._container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
        }

        static void Main(string[] args)
        {
            Program p = new Program(); //Composition is performed in the constructor
            String s;
            Console.WriteLine("Enter Command:");
            while (true)
            {
                s = Console.ReadLine();
                Console.WriteLine(p.calculator.Calculate(s));
            }
        }
    }
}
