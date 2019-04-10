using System;

namespace PublishSubscriptDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //ConcreateSubject1 notifier = new ConcreateSubject1();
            //notifier.Attach(new ConcreateObserverA(notifier, "bear big"));
            //notifier.Attach(new ConcreateObserverA(notifier, "bear two"));
            //notifier.status = "The bald head named Qiang is cutting trees!";
            //notifier.Notify();

            JiJiKing jiJiKing = new JiJiKing();

            BearBig bearBig = new BearBig("Bear Big", jiJiKing);

            jiJiKing.Update += new EventHandler(bearBig.ProtectTree);

            jiJiKing.Status = "The bald head named Qiang is cutting trees!";
            jiJiKing.Notify();

            Console.ReadLine();
        }
    }
}
