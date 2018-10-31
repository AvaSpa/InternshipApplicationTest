using InternshipApplicationTest.Common.ModelClasses;
using InternshipApplicationTest.Common.Util;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InternshipApplicationTest.WinformsUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            await LoadInternships();
        }

        private async Task LoadInternships()
        {
            var internships = await DataAccess.GetThisYearsInternships();
            cmbInternships.DataSource = internships;
        }

        private async void btnStartTest_Click(object sender, EventArgs e)
        {
            var applicantId = await DataAccess.RegisterApplicant(txtFirstName.Text, txtLastName.Text, txtPhoneNumber.Text);

            var selectedInternship = (InternshipModel)cmbInternships.SelectedItem;
            var internshipId = selectedInternship.Id;
            var test = await DataAccess.GetTest(applicantId, internshipId);

            if (test.ApplicantInternshipId > 0)
            {
                var testForm = new TestForm(test);
                this.Hide();
                testForm.ShowDialog();

                this.Show();
                var obtainedScore = ScoreCalculator.Instance.Score;
                var response = await DataAccess.GetTestResult(test.ApplicantInternshipId, obtainedScore);
                var passed = response.StatusCode == HttpStatusCode.Accepted;

                if (passed)
                {
                    MessageBox.Show($"Congratulations! You passed!{Environment.NewLine}Your score: {obtainedScore}{Environment.NewLine}We will transmit to you the date and time of the selection test.");
                    Application.Exit();
                }
                else
                {
                    MessageBox.Show($"Sorry, you failed.{Environment.NewLine}Try again next year or choose another internship!");
                }
            }
            else
            {
                MessageBox.Show($"Cannot apply again to this internship.{Environment.NewLine}Please choose another internship!");
            }
        }
    }
}
