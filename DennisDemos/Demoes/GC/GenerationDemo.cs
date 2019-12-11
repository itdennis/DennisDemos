using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Demoes.GC
{
    class GenObj
    {
        private string v;

        public GenObj(string v)
        {
            this.v = v;
        }

        public void DisplayGeneration()
        {
            Console.WriteLine("my generation is " + System.GC.GetGeneration(this));
        }

        ~GenObj()
        {
            Console.WriteLine("My Finalize method called");
        }
    }
    public class GenerationDemo
    {

        private static void GenerationDemoRun()
        {
            // Let's see how many generations the GCH supports (we know it's 2)
            Display("Maximum GC generations: " + System.GC.MaxGeneration);

            // Create a new BaseObj in the heap
            GenObj obj = new GenObj("Generation");

            // Since this object is newly created, it should be in generation 0
            obj.DisplayGeneration(); // Displays 0

            // Performing a garbage collection promotes the object's generation
            System.GC.Collect();
            obj.DisplayGeneration(); // Displays 1

            System.GC.Collect();
            obj.DisplayGeneration(); // Displays 2

            System.GC.Collect();
            obj.DisplayGeneration(); // Displays 2 (max generation)

            obj = null; // Destroy the strong reference to this object

            System.GC.Collect(0); // Collect objects in generation 0
            System.GC.WaitForPendingFinalizers(); // We should see nothing

            System.GC.Collect(1); // Collect objects in generation 1
            System.GC.WaitForPendingFinalizers(); // We should see nothing

            System.GC.Collect(2); // Same as Collect()
            System.GC.WaitForPendingFinalizers(); // Now, we should see the Finalize 
                                           // method run

        }

        private static void Display(object p)
        {
            Console.WriteLine(p.ToString());
        }
    }
}
