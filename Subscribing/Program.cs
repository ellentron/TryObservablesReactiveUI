using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace Subscribing
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create new subject
            var subject1 = new Subject<int>();

            // Subscribe to subject with lambda actions
            subject1.Subscribe(
                v => Console.WriteLine($" Subject1 subscription received : {v}"),
                ex => Console.WriteLine($"Subject1 subscription received exception: {ex}"),
                () => Console.WriteLine(" Subject1 subscription completed")
                );
                        
            subject1.OnNext(0);
            subject1.OnNext(1);
            subject1.OnError(new Exception("Dummy exception"));

            Console.WriteLine("1) Press any key to continue");
            Console.WriteLine("---------------------------------------------------");
            Console.ReadKey();

            // ===========================================================================
            // Create new subject
            var subject2 = new Subject<int>();

            // Subscribe to subject with lambda actions
            subject2.Subscribe(
                v => Console.WriteLine($"Subject 2 1st subscription received : {v}"),
                ex => Console.WriteLine($"Subject 2 1st subscription received exception: {ex}"),
                () => Console.WriteLine("Subject 2 1st subscription completed")
                );

            subject2.OnNext(0);
            subject2.OnNext(1);
            subject2.OnCompleted();

            Console.WriteLine("2) Press any key to continue");
            Console.WriteLine("---------------------------------------------------");
            Console.ReadKey();

            // ===========================================================================
            // Create new subject
            var subject3 = new Subject<int>();

            // Subscribe to subject with lambda actions
            var sb1 = subject3.Subscribe(
                v => Console.WriteLine($"Subject 3 - 1st subscription received : {v}"),
                ex => Console.WriteLine($"Subject 3 - 1st subscription received exception: {ex}"),
                () => Console.WriteLine("Subject 3 - 1st subscription completed")
                );

            var sb2 = subject3.Subscribe(
                v => Console.WriteLine($"Subject 3 - 2nd subscription received : {v}"),
                ex => Console.WriteLine($"Subject 3 - 2nd subscription received exception: {ex}"),
                () => Console.WriteLine("Subject 3 - 2nd subscription completed")
                );
            
            subject3.OnNext(0);
            subject3.OnNext(1);
            subject3.OnNext(2);
            subject3.OnNext(3);

            sb1.Dispose();

            subject3.OnNext(4);
            subject3.OnNext(5);
            subject3.OnNext(6);
            subject3.OnNext(7);

            Console.WriteLine("3) Press any key to continue");
            Console.WriteLine("---------------------------------------------------");
            Console.ReadKey();
        }
    }
}
