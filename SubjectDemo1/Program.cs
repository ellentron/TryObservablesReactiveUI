using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SubjectDemo1
{
    /// <summary>
    /// The Subject Class is provided by the Rx framework. It's a class that includes both the IObservable interface
    /// and the IObserver interface, mostly for practicing Reactive programming
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Demo();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static void Demo()
        {
            // We create the subject
            var subject = new Subject<int>();

            // We then subscribe to it (with Lambda or Lambdas for OnNext, OnError and OnCompleted...)
            var subscription = subject.Subscribe(Console.WriteLine);

            // As a Subject is also an observable we can call OnNext to produce values.
            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);
            
            // We can dispse the subscription and we will not receive any new values
            subscription.Dispose();

            subject.OnNext(4);

            // Normally it is advised not to use Subject for real programs
            // But here is an example of a practical use case - when we need to multicast or broadcast service 
            // that relays its valuse to many observers like a midiator

            var subject1 = new Subject<string>(); // We instantiate the subject

            // We create an example of observable from an Enumerable
            var o1 = new[] { "Hello", "RxWorld" }.ToObservable();

            // We now create subscriptions
            var s4 = o1.Subscribe(subject1);

            var s1 = subject1.Subscribe(v => PrintToConsole("Subscription1", v));
            var s2 = subject1.Subscribe(v => PrintToDebugWindow("Subscription2", v));
            var s3 = subject1.Subscribe(v => PrintToConsoleSlowly("Subscription3", v));
        }

        private static void PrintToConsoleSlowly<T>(string subscriberName, T value)
        {
            Thread.Sleep(500);
            Console.WriteLine($"{subscriberName}: {value.ToString()}");
        }

        private static void PrintToDebugWindow<T>(string subscriberName, T value)
        {
            Debug.WriteLine($"{subscriberName}: {value.ToString()}");
        }

        private static void PrintToConsole<T>(string subscriberName, T value)
        {
            Console.WriteLine($"{subscriberName}: {value.ToString()}");

        }
    }
}
