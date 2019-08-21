using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace ReplaySubjectExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // ReplaySubject<T> provides the feature of caching values and then replaying them for any late subscriptions.
            // Consider this example where we have moved our first publication to occur before our subscription
            var subject1 = new ReplaySubject<string>();
            subject1.OnNext("a");
            WriteSequenceToConsole(subject1);
            subject1.OnNext("b");
            subject1.OnNext("c");

            Console.WriteLine("1) This was ReplaySubject");
            Console.ReadKey();
            Console.WriteLine("===========================================================================\n\r");
            var subject2 = new ReplaySubject<string>(2);
            subject2.OnNext("d");
            subject2.OnNext("e");
            subject2.OnNext("f");
            subject2.OnNext("g");
            WriteSequenceToConsole(subject2);

            Console.WriteLine("2) This was ReplaySubject with a buffer of 2 values");
            Console.ReadKey();
            Console.WriteLine("===========================================================================\n\r");

            var ms = 2;
            var ts = new TimeSpan(0,0,0,ms);
            var subject3 = new ReplaySubject<string>(ts);
            for (int i = 0; i < 10000; i++)
            {
                subject3.OnNext(i.ToString());
            }
            WriteSequenceToConsole(subject3);

            Console.WriteLine($"3) This was ReplaySubject with a time window of {ms}ms ");
            Console.ReadKey();
            Console.WriteLine("===========================================================================\n\r");

            // BehaviorSubject<T> is similar to ReplaySubject<T> except it only remembers the last publication. 
            // BehaviorSubject<T> also requires you to provide it a default value of T. 
            // This means that all subscribers will receive a value immediately (unless it is already completed).

            var subject4 = new BehaviorSubject<string>("Default Value");
            WriteSequenceToConsole(subject4);

            Console.WriteLine($"4) This demonstrated BehaviorSubject that published its default value due to lack of OnNext()");
            Console.ReadKey();
            Console.WriteLine("===========================================================================\n\r");

            var subject5 = new BehaviorSubject<string>("Default Value");
            subject5.OnNext("X");
            subject5.OnNext("Y");
            subject5.OnNext("Z");
            WriteSequenceToConsole(subject5);

            Console.WriteLine($"5) This demonstrated BehaviorSubject that published the last publication");
            Console.ReadKey();

            Console.WriteLine("===========================================================================\n\r");
            var subject6 = new BehaviorSubject<string>("Default Value");

            subject5.OnNext("X");
            WriteSequenceToConsole(subject6);            
            subject5.OnNext("Y");
            subject5.OnNext("Z");

            Console.WriteLine($"6) This demonstrated BehaviorSubject that published all publications");
            Console.ReadKey();
            Console.WriteLine("===========================================================================\n\r");

            // Finally in this example, no values will be published as the sequence has completed.
            // Nothing is written to the console.

            var subject7 = new BehaviorSubject<string>("a");
            subject7.OnNext("b");
            subject7.OnNext("c");
            subject7.OnCompleted();
            subject7.Subscribe(Console.WriteLine);

            Console.WriteLine($"7) This demonstrated BehaviorSubject that published OnCompleted before subscribing and writes nothing to the console");
            Console.ReadKey();
            Console.WriteLine("===========================================================================\n\r");

            // AsyncSubject<T> is similar to the Replay and Behavior subjects in the way that it caches values, 
            // however it will only store the last value, and only publish it when the sequence is completed.
            // The general usage of the AsyncSubject<T> is to only ever publish one value then immediately complete.
            // This means that is becomes quite comparable to Task<T>.

            // In this example no values will be published as the sequence never completes.
            // No values will be written to the console.

            var subject8 = new AsyncSubject<string>();
            subject8.OnNext("a");
            WriteSequenceToConsole(subject8);
            subject8.OnNext("b");
            subject8.OnNext("c");
            Console.ReadKey();

            Console.WriteLine("8) This demonstrated AsyncSubject that has no OnCompleted and writes nothing to the console");
            Console.WriteLine("===========================================================================\n\r");
            Console.ReadKey();

            // In the following example we invoke the OnCompleted method, so the last value 'c' is written to the console:
            var subject9 = new AsyncSubject<string>();
            subject9.OnNext("a");
            WriteSequenceToConsole(subject9);
            subject9.OnNext("b");
            subject9.OnNext("c");
            subject9.OnCompleted();
            Console.ReadKey();

            Console.WriteLine("9) This demonstrated AsyncSubject that has OnCompleted and writes the latest publication to the console");
            Console.WriteLine("===========================================================================\n\r");
            Console.ReadKey();

        }

        //Takes an IObservable<string> as its parameter. 
        //Subject<string> implements this interface.
        static void WriteSequenceToConsole(IObservable<string> sequence)
        {
            //The next two lines are equivalent.
            //sequence.Subscribe(value=>Console.WriteLine(value));
            sequence.Subscribe(Console.WriteLine);
        }
    }
}
