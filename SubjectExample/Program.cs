using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace SubjectExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var subject = new Subject<string>();
            WriteSequenceToConsole(subject);
            subject.OnNext("aaa");
            subject.OnNext("bbb");
            subject.OnNext("cbb");
            //subject.OnError(new Exception("This example faulted and is now reporting with subject.OnError(This example ...)"));
            subject.OnCompleted();
            Console.ReadKey();
        }

        //Takes an IObservable<string> as its parameter. 
        //Subject<string> implements this interface.
        static void WriteSequenceToConsole(IObservable<string> sequence)
        {
            //The next two lines are equivalent.
            sequence.Subscribe(value=>Console.WriteLine($"{value}"));
            //sequence.Subscribe(Console.WriteLine);
        }
    }
}
