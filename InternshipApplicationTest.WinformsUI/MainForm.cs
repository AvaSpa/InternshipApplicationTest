using InternshipApplicationTest.Common.ModelClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InternshipApplicationTest.WinformsUI
{
    public partial class MainForm : Form
    {
        private const string BASE_URL = "http://localhost:52044/api/";

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
            var webclient = new WebClient<InternshipModel>(BASE_URL);
            var results = await webclient.GetAsync<List<InternshipModel>>("");
            var internships = results.Where(i => i.Year == DateTime.Now.Year).ToList();
            cmbInternships.DataSource = internships;
        }

        private async void btnStartTest_Click(object sender, EventArgs e)
        {
            var applicantId = await RegisterApplicant();

            var webclient = new WebClient<TestModel>("http://localhost:52044/api/");
            var selectedInternship = (InternshipModel)cmbInternships.SelectedItem;
            var internshipId = selectedInternship.Id;
            var test = await webclient.GetAsync<TestModel>($"applicantId={applicantId}&internshipId={internshipId}");

            if (test.ApplicantInternshipId > 0)
            {
                var testForm = new TestForm(BASE_URL, test);
                this.Hide();
                testForm.ShowDialog();

                this.Show();
                var obtainedScore = ScoreCalculator.Instance.Score;
                var applicantInternship = new ApplicantInternshipModel { Id = test.ApplicantInternshipId, Score = obtainedScore };
                var response = await webclient.PostAsync<ApplicantInternshipModel>(applicantInternship);
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

        private async Task<int> RegisterApplicant()
        {
            var webclient = new WebClient<ApplicantModel>(BASE_URL);
            var applicant = new ApplicantModel { FirstName = txtFirstName.Text, LastName = txtLastName.Text, PhoneNumber = txtPhoneNumber.Text };
            var response = await webclient.PostAsync<ApplicantModel>(applicant);
            var responseContent = await response.Content.ReadAsStringAsync();
            var applicantId = Convert.ToInt32(responseContent);
            return applicantId;
        }
    }
}
