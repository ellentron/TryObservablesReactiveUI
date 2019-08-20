using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;

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
            return Observable.Return(Unit.Default);
        }
        #endregion


    }
}
