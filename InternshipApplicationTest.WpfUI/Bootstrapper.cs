using Caliburn.Micro;
using InternshipApplicationTest.WpfUI.ViewModels;
using System.Windows;

namespace InternshipApplicationTest.WpfUI
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
