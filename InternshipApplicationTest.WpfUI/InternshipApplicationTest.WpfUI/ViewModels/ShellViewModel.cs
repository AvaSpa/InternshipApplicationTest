using System;
using System.Net;
using Caliburn.Micro;
using InternshipApplicationTest.Common.ModelClasses;
using InternshipApplicationTest.Common.Util;

namespace InternshipApplicationTest.WpfUI.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private ApplicationFormViewModel form;
        private TestViewModel testControl;

        protected override void OnViewReady(object view)
        {
            form = new ApplicationFormViewModel();
            ActivateItem(form);
        }

        /// <summary>
        /// Called from ApplicationFormViewModel
        /// </summary>
        public void BeginTest(TestModel test)
        {
            DeactivateItem(form, true);
            testControl = new TestViewModel(test);
            ActivateItem(testControl);
        }

        /// <summary>
        /// Called from TestViewModel
        /// </summary>
        public async void EndTest(int applicantInternshipId, short obtainedScore)
        {

            var response = await DataAccess.GetTestResult(applicantInternshipId, obtainedScore);
            var passed = response.StatusCode == HttpStatusCode.Accepted;
            if (passed)
            {
                ShowMessage($"Congratulations! You passed!{Environment.NewLine}Your score: {obtainedScore}{Environment.NewLine}We will transmit to you the date and time of the selection test.");
                TryClose();
            }
            else
            {
                ShowMessage($"Sorry, you failed.{Environment.NewLine}Try again next year or choose another internship!");
                form = new ApplicationFormViewModel();
                ActivateItem(form);
            }

            DeactivateItem(testControl, true);
        }

        private static void ShowMessage(string message)
        {
            var messageBox = new MessageBoxViewModel(message);
            new WindowManager().ShowDialog(messageBox);
        }
    }
}
