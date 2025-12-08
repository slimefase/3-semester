using System.Windows.Forms;
using GameStore.BusinessLogic;
using GameStore.WinFormsApp;
using Ninject;

namespace GameStore.Presenter
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // DI
            IKernel kernel = new StandardKernel(new SimpleConfigModule());
            var logic = kernel.Get<Logic>();

            // View
            var form = new Form1();

            // Presenter
            var presenter = new Presenter(form, logic);

            // Run
            Application.Run(form);
        }
    }
}
