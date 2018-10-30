using InternshipApplicationTest.Common.ModelClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace InternshipApplicationTest.Common.Util
{
    public static class DataAccess
    {
        public static async Task<List<InternshipModel>> GetThisYearsInternships()
        {
            var webclient = new WebClient<InternshipModel>(Constants.BASE_URL);
            var results = await webclient.GetAsync<List<InternshipModel>>("");
            var internships = results.Where(i => i.Year == DateTime.Now.Year).ToList();
            return internships;
        }

        public static async Task<int> RegisterApplicant(string firstName, string lastName, string phoneNumber)
        {
            var webclient = new WebClient<ApplicantModel>(Constants.BASE_URL);
            var applicant = new ApplicantModel { FirstName = firstName, LastName = lastName, PhoneNumber = phoneNumber };
            var response = await webclient.PostAsync<ApplicantModel>(applicant);
            var responseContent = await response.Content.ReadAsStringAsync();
            var applicantId = Convert.ToInt32(responseContent);
            return applicantId;
        }

        public static async Task<TestModel> GetTest(int applicantId, int internshipId)
        {
            var webclient = new WebClient<TestModel>(Constants.BASE_URL);
            return await webclient.GetAsync<TestModel>($"applicantId={applicantId}&internshipId={internshipId}");
        }

        public static async Task<HttpResponseMessage> GetTestResult(int applicantInternshipId, short obtainedScore)
        {
            var webclient = new WebClient<TestModel>(Constants.BASE_URL);
            var applicantInternship = new ApplicantInternshipModel { Id = applicantInternshipId, Score = obtainedScore };
            return await webclient.PostAsync<ApplicantInternshipModel>(applicantInternship);
        }

        public static async Task<List<TestAnswerModel>> GetAnswers()
        {
            var webclient = new WebClient<TestAnswerModel>(Constants.BASE_URL);
            var results = await webclient.GetAsync<List<TestAnswerModel>>("");
            return results;
        }
    }
}
