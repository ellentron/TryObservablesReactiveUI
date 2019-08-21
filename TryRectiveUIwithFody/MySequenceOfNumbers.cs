using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TryRectiveUIwithFody
{
    class MySequenceOfNumbers : IObservable<int>
    {
        public IDisposable Subscribe(IObserver<int> observer)
        {
            for (int i = 0; i < 5; i++)
            {
                if (i==4)
                {
                    observer.OnError(new Exception($"This is a demo exception that happend on itteration #{i}"));
                    return Disposable.Empty;
                }
                observer.OnNext(i);
                Thread.Sleep(1000);
            }
            
            //observer.OnNext(2);
            //observer.OnNext(3);
            observer.OnCompleted();

            return Disposable.Empty;
        }
    }
}
