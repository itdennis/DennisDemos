using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.Demoes
{
    delegate void StringProcesser(string input);
    class Person
    {
        string name;
        public Person(string name)
        {
            this.name = name;
        }
        public Person(string name, int age)
        {
            this.name = name;
        }
        public void Say(string message)
        {
            Console.WriteLine("{0} says {1}.", name, message);
        }
    }

    class Background
    {
        public static void Note(string note)
        {
            Console.WriteLine($"{note}");
        }
    }

    class SimpleDelegateUse
    {
        public static void Run()
        {
            Person jon = new Person("Jon");
            Person tom = new Person("Tom");
            StringProcesser jonVoice, tomVoice, background;
            jonVoice = new StringProcesser(jon.Say);
            tomVoice = new StringProcesser(delegate (string s1) { Console.WriteLine(s1); });
            background = (s2) => Console.WriteLine(s2);
            jonVoice("Helllo son.");
            tomVoice.Invoke("Hello Daddy.");
            background("some thing flys.");
        }
    }
}
