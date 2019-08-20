using ReactiveUI;
using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;

namespace TryObservablesReactiveUI
{
    public class MyViewModel : ReactiveObject
    {
        readonly ObservableAsPropertyHelper<string> firstName;
        //public string FirstName => firstName.Value;

        public MyViewModel()
        {
            //Clear = ReactiveCommand.Create(() => { Name = string.Empty; });
            //this.WhenAnyValue(x => x.Name)
            //    .Select(userName => $"Hello, {userName}!")
            //    .ToProperty(this, x => x.Greeting, out greeting);

            TheTextCommand = ReactiveCommand.CreateFromObservable(ExecuteTextCommand);

            //this.WhenAnyValue(x => x.Name)
            //    .Select(name => name.Split(' ')[0])
            //    .ToProperty(this, x => x.FirstName, out firstName);

            this.WhenAnyValue(x => x.theName)
                .ToProperty(this, x => x.Name, out name);
        }

        ObservableAsPropertyHelper<string> name;
        public string Name
        {
            get { return name.Value; }
        }

        //public string Name
        //{
        //    get { return name.Value; }
        //}

        public ReactiveCommand<Unit, Unit> TheTextCommand { get; set; }

        private string theName;
        public string TheName
        {
            get => theName;
            set => this.RaiseAndSetIfChanged(ref theName, value);
        }

        private double theValue;
        public double TheValue
        {
            get => theValue;
            set => this.RaiseAndSetIfChanged(ref theValue, value);
        }

        private IObservable<Unit> ExecuteTextCommand()
        {
            //TheValue = "Hello ReactiveUI";
            TheValue = 10000000;
            return Observable.Return(Unit.Default);
        }
    }
}
