using System;

namespace ProtoTypeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                //ConcreatePrototype1 concreatePrototype1 = new ConcreatePrototype1("Dici");
                //ConcreatePrototype1 concreatePrototype2 = (ConcreatePrototype1)concreatePrototype1.Clone();
                //Console.WriteLine(concreatePrototype2.Id);
                //Console.ReadLine();
            }

            {
                Resume resume = new Resume("Dici");
                resume.SetPersonalInfo("29", "Man");
                resume.SetWorkingExperince("Microsoft", "dev of monitor system");
                resume.Display();
                Resume resume2 = (Resume)resume.Clone();
                Resume resume3 = (Resume)resume.Clone();
                Console.WriteLine("=========================================");
                resume2.Display();
                Console.WriteLine("=========================================");
                resume3.Display();
            }
        }
    }
}
