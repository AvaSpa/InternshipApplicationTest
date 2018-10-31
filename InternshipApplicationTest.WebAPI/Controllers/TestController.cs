using InternshipApplicationTest.Common.ModelClasses;
using InternshipApplicationTest.WebAPI.Classes;
using InternshipApplicationTest.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InternshipApplicationTest.WebAPI.Controllers
{
    /// <summary>
    /// The controller for the test.
    /// This is the controller used in the main workflow.
    /// </summary>
    public class TestController : ApiController
    {
        private DBContainer db;

        public TestController()
        {
            db = new DBContainer();
        }

        /// <summary>
        /// Method used to generate a test for an applicant and an internship
        /// </summary>
        /// <param name="applicantId">The id of the applicant</param>
        /// <param name="internshipId">The id of the internship</param>
        /// <returns>A set of questions with the configured length</returns>
        [HttpGet]
        public TestModel GenerateTest(int applicantId, int internshipId)
        {
            var alreadyTested = db.C_ApplicantInternship.Any(ai => ai.N_ApplicantId == applicantId && ai.N_InternshipId == internshipId);
            if (!alreadyTested)
            {
                var applicantInternship = new C_ApplicantInternship { DateTestTaken = DateTime.Now, N_ApplicantId = applicantId, N_InternshipId = internshipId };
                var insertedEntity = db.C_ApplicantInternship.Add(applicantInternship);
                db.SaveChanges();

                Debug.WriteLine("Inserted CApplicantInternship");

                var configuration = db.TestConfigurations.FirstOrDefault();
                var result = new TestModel { ApplicantInternshipId = insertedEntity.Id, TimeLimitInMinutes = (short)configuration.TimeLimit.Minutes };

                Debug.WriteLine($"Configuration: {configuration.QuestionNumber}");

                var randomGenerator = new Random(DateTime.Now.Millisecond);
                var numberOfQuestionsInDB = db.TestQuestions.Count();

                Debug.WriteLine($"Number of questions in db: {numberOfQuestionsInDB}");

                if (numberOfQuestionsInDB > configuration.QuestionNumber)
                {
                    result.Questions = new List<TestQuestionModel>();
                    while (result.Questions.Count < configuration.QuestionNumber)
                    {
                        var randomQuestionId = randomGenerator.Next(configuration.QuestionNumber) + 1;
                        var question = db.TestQuestions.Find(randomQuestionId);
                        var resultContainsGeneratedQuestionId = result.Questions.AsQueryable().Any(q => q.Id == randomQuestionId);
                        if (!resultContainsGeneratedQuestionId && question != null)
                        {
                            result.Questions.Add(TestQuestionConverter.TestQuestionToTestQuestionModel(question));
                        }

                        Debug.WriteLine($"Added question to test: {question.Statement}");
                    }
                }
                else if (numberOfQuestionsInDB == configuration.QuestionNumber)
                {
                    result.Questions = db.TestQuestions.ToList().Select(q => TestQuestionConverter.TestQuestionToTestQuestionModel(q)).ToList();
                }

                return result;
            }
            else
            {
                return new TestModel { ApplicantInternshipId = 0 };
            }
        }

        /// <summary>
        /// Method used at the end of a test to save the test results
        /// </summary>
        /// <param name="applicantInternship">The object containing the results</param>
        [HttpPost]
        public HttpResponseMessage EndTest([FromBody] ApplicantInternshipModel applicantInternship)
        {
            var minimumScore = db.TestConfigurations.FirstOrDefault().MinimumScore;
            var passed = applicantInternship.Score >= minimumScore;
            var existingApplicantInternship = db.C_ApplicantInternship.Find(applicantInternship.Id);
            if (existingApplicantInternship != null)
            {
                existingApplicantInternship.Score = applicantInternship.Score;
                existingApplicantInternship.ApplicantPassedTheTest = passed;
                db.SaveChanges();
            }

            if (passed)
            {
                return Request.CreateResponse(HttpStatusCode.Accepted);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable);
            }
        }
    }
}
