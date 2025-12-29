using System.Windows;
using Ninject;
using GameStore.BusinessLogic;
using GameStore.Presenter;

namespace GameStore.View
{
    public partial class App : Application
    {
        private IKernel _kernel;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _kernel = new StandardKernel(new SimpleConfigModule());
            var logic = _kernel.Get<Logic>();
            var mainViewModel = new MainViewModel(logic);
            var viewManager = new ViewManager();
            viewManager.ShowMain(mainViewModel);
        }
    }
}