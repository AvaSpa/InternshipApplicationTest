using Caliburn.Micro;
using InternshipApplicationTest.WpfUI.ViewModels;
using System.Windows;

namespace InternshipApplicationTest.WpfUI
{
    public class Bootstrapper : BootstrapperBase
    {
        #region Constructor
        public Bootstrapper()
        {
            Initialize();
        }
        #endregion

        #region Overrides
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
        #endregion
    }
}
