using GameStore.Presenter;

namespace GameStore.View
{
    public class ViewManager
    {
        public void ShowMain(MainViewModel viewModel)
        {
            var window = new MainWindow();
            window.DataContext = viewModel;
            window.Show();
        }
    }
}