using Caliburn.Micro;
using InternshipApplicationTest.Common.ModelClasses;
using InternshipApplicationTest.Common.Util;
using System;

namespace InternshipApplicationTest.WpfUI.ViewModels
{
    public class ApplicationFormViewModel : Screen
    {
        private string _firstName;
        private string _lastName;
        private string _phoneNumber;
        private InternshipModel _selectedInternship;

        public BindableCollection<InternshipModel> Internships { get; set; }

        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
                NotifyOfPropertyChange(() => CanBeginTest);
                NotifyOfPropertyChange(() => FirstName);
            }
        }
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                NotifyOfPropertyChange(() => CanBeginTest);
                NotifyOfPropertyChange(() => LastName);
            }
        }
        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                _phoneNumber = value;
                NotifyOfPropertyChange(() => PhoneNumber);
                NotifyOfPropertyChange(() => CanBeginTest);
            }
        }

        public InternshipModel SelectedInternship
        {
            get
            {
                return _selectedInternship;
            }
            set
            {
                _selectedInternship = value;
                NotifyOfPropertyChange(() => SelectedInternship);
                NotifyOfPropertyChange(() => CanBeginTest);
            }
        }

        public bool CanBeginTest
        {
            get
            {
                return !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName) && !string.IsNullOrEmpty(PhoneNumber) && SelectedInternship != null;
            }
        }

        public ApplicationFormViewModel()
        {
            Internships = new BindableCollection<InternshipModel>();
        }

        protected override async void OnViewLoaded(object view)
        {
            var internships = await DataAccess.GetThisYearsInternships();
            Internships.AddRange(internships);
        }

        public async void BeginTest()
        {
            var applicantId = await DataAccess.RegisterApplicant(FirstName, LastName, PhoneNumber);
            var test = await DataAccess.GetTest(applicantId, SelectedInternship.Id);
            if (test.ApplicantInternshipId > 0)
            {
                var shell = (ShellViewModel)Parent;
                shell.BeginTest(test);
            }
            else
            {
                ShowMessage($"Cannot apply again to this internship.{Environment.NewLine}Please choose another internship!");
            }
        }

        private static void ShowMessage(string message)
        {
            var messageBox = new MessageBoxViewModel(message);
            new WindowManager().ShowDialog(messageBox);
        }
    }
}
