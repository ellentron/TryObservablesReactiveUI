using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryRectiveUIwithFody
{
    class MyConsoleObserver : IObserver<int>
    {
        public void OnNext(int value)
        {
            Debug.WriteLine($"Received value: {value}");
        }
        
        public void OnError(Exception error)
        {
            Debug.WriteLine($"Sequence faulted with: {error}");
        }
        public void OnCompleted()
        {
            Debug.WriteLine("Sequence terminated");
        }
    }
}
