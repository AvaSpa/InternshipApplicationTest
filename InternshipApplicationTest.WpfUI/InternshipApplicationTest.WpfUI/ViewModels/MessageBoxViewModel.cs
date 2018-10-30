using Caliburn.Micro;

namespace InternshipApplicationTest.WpfUI.ViewModels
{
    public class MessageBoxViewModel : Screen
    {
        private string _message;

        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
                NotifyOfPropertyChange(() => Message);
            }
        }

        public MessageBoxViewModel(string message)
        {
            Message = message;
        }

        public void Acknowledge()
        {
            TryClose(true);
        }
    }
}
