using Caliburn.Micro;

namespace InternshipApplicationTest.WpfUI.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private string _submitText = "Submit";

        public string SubmitText
        {
            get
            {
                return _submitText;
            }
            set
            {
                _submitText = value;
                NotifyOfPropertyChange(() => SubmitText);
            }
        }

        public void BeginTest()
        {

        }

    }
}
