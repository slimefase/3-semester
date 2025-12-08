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

            IKernel kernel = new StandardKernel(new SimpleConfigModule());
            var logic = kernel.Get<Logic>();

            var form = new Form1();

            var presenter = new Presenter(form, logic);

            Application.Run(form);
        }
    }
}
