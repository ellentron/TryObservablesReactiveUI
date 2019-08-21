using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace TryRectiveUIwithFody
{
    public class MyViewModel : ReactiveObject
    {

        #region Constructor
        public MyViewModel()
        {
            this.WhenAnyValue(x => x.TheName)
                .Select(theName => theName?.Split(' ')[0])
                .ToPropertyEx(this, x => x.FirstName);

            SetValueTo1000000Command = ReactiveCommand.CreateFromObservable(ExecuteSetValueTo1000000Command);

            
                var subject = new Subject<string>();
                subject.OnNext("a");
                subject.OnNext("b");
                subject.OnNext("c");
            Console.Read();
            subject.Dispose();
        }
        #endregion

        #region Properties
        [Reactive] public string TheName { get; set; }

        [Reactive] public double TheValue { get; set; }

        public extern string FirstName { [ObservableAsProperty]get; }

        /// <summary>
        /// SetValueTo1000000Command (a ReactiveUI command)
        /// </summary>
        public ReactiveCommand<Unit, Unit> SetValueTo1000000Command { get; set; }

        #endregion

        #region Methods
        /// <summary>
        /// Command that executes when 
        /// </summary>
        /// <returns></returns>
        private IObservable<Unit> ExecuteSetValueTo1000000Command()
        {
            //TheValue = "Hello ReactiveUI";
            TheValue = 10000000;

            //TryObserver();

           

            return Observable.Return(Unit.Default);
        }

 

        private static void TryObserver()
        {
            var numbers = new MySequenceOfNumbers();
            var observer = new MyConsoleObserver();
            numbers.Subscribe(observer);
        }

        /// <summary>
        /// Takes an IObservable<string> as its parameter. 
        /// Subject<string> implements this interface.
        /// </summary>
        /// <param name="sequence"></param>
        static void WriteSequenceToConsole(IObservable<string> sequence)
        {
            sequence.Subscribe(value=>Debug.WriteLine(value));
        }
        #endregion


    }
}
