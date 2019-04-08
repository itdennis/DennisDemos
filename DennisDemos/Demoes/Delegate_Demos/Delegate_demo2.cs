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
            tomVoice = new StringProcesser(tom.Say);
            background = new StringProcesser(Background.Note);
            jonVoice("Helllo son.");
            tomVoice.Invoke("Hello Daddy.");
            background("some thing flys.");
        }
    }
}
