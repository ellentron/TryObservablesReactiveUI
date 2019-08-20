using ReactiveUI;
using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;

namespace TryObservablesReactiveUI
{
    /// <summary>
    /// My View Model for ReactiveUI experiments
    /// </summary>
    public class MyViewModel : ReactiveObject
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MyViewModel()
        {
            SetValueTo1000000Command = ReactiveCommand.CreateFromObservable(ExecuteSetValueTo1000000Command);

            firstName = this.WhenAnyValue(x => x.TheName)
                            .Select(theName => theName?.Split(' ')[0])
                            .ToProperty(this, x => x.FirstName);
        }

         ObservableAsPropertyHelper<string> firstName;
        public string FirstName
        {
            get { return firstName.Value; }
        }

        /// <summary>
        /// SetValueTo1000000Command (a ReactiveUI command)
        /// </summary>
        public ReactiveCommand<Unit, Unit> SetValueTo1000000Command { get; set; }

        /// <summary>
        /// private raw text binded to NameTextBox in the UI 
        /// </summary>
        private string theName;

        /// <summary>
        /// Name raw text binded to NameTextBox in the UI XMAL
        /// </summary>
        public string TheName
        {
            get => theName;
            set => this.RaiseAndSetIfChanged(ref theName, value);
        }

        /// <summary>
        /// Private raw double value parsed & clculated from TheValueBox TextBox in the UI XMAL
        /// </summary>
        private double theValue;

        /// <summary>
        /// A double value parsed & clculated from TheValueBox TextBox in the UI XMAL
        /// </summary>
        public double TheValue
        {
            get => theValue;
            set => this.RaiseAndSetIfChanged(ref theValue, value);
        }

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
    }
}
