using ReactiveUI;
using System.Reactive.Disposables;
using System.Windows;

namespace TryObservablesReactiveUI
{
    // MainWindow class derives off ReactiveWindow which implements the IViewFor<TViewModel>
    // interface using a WPF DependencyProperty. We need this to use WhenActivated extension
    // method that helps us handling View and ViewModel activation and deactivation.
    public partial class MainWindow : ReactiveWindow<MyViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MyViewModel();

            // We create our bindings here. These are the code behind bindings which allow 
            // type safety. The bindings will only become active when the Window is being shown.
            // We register our subscription in our disposableRegistration, this will cause 
            // the binding subscription to become inactive when the Window is closed.
            // The disposableRegistration is a CompositeDisposable which is a container of 
            // other Disposables. We use the DisposeWith() extension method which simply adds 
            // the subscription disposable to the CompositeDisposable.
            this.WhenActivated(disposable =>
            {
                this.Bind(this.ViewModel, x => x.TheValue, x => x.TheValueBox.Text)
                    .DisposeWith(disposable);

                this.OneWayBind(this.ViewModel, x => x.TheValue, x => x.TheTextBlock.Text)
                    .DisposeWith(disposable);

                this.BindCommand(ViewModel, x => x.SetValueTo1000000Command, x => x.TheTextButton)
                    .DisposeWith(disposable);

                this.Bind(ViewModel, x => x.TheName, x => x.NameTextBox.Text)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel, x => x.FirstName, x => x.NameTextBlock.Text)
                    .DisposeWith(disposable);


                //this.OneWayBind(ViewModel,
                //   viewModel => viewModel.Name,
                //   view => view.Name.Text);

                //this.Bind(ViewModel,
                //    viewModel => viewModel.SearchTerm,
                //    view => view.searchTextBox.Text)
                //    .DisposeWith(disposableRegistration);
            });
        }
    }
}
