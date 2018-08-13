using InternshipApplicationTest.Common.ModelClasses;
using InternshipApplicationTest.WebAPI.Classes;
using InternshipApplicationTest.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InternshipApplicationTest.WebAPI.Controllers
{
    /// <summary>
    /// The controller for the TestQuestion entity
    /// </summary>
    public class TestQuestionController : ApiController
    {
        private static DBContainer db = new DBContainer();
        private static DbSet<TestQuestion> testQuestions = db.TestQuestions;

        /// <summary>
        /// Method used to get all the testQuestions
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<TestQuestionModel> Get()
        {
            return testQuestions.ToList().Select(a => TestQuestionConverter.TestQuestionToTestQuestionModel(a));
        }

        /// <summary>
        /// Method used to get a testQuestion
        /// </summary>
        /// <param name="id">The id of the testQuestion</param>
        /// <returns></returns>
        [HttpGet]
        public TestQuestionModel Get(int id)
        {
            return TestQuestionConverter.TestQuestionToTestQuestionModel(testQuestions.Find(id));
        }

        /// <summary>
        /// Method used to add a testQuestion
        /// </summary>
        /// <param name="value">The testQuestion to be added</param>
        [HttpPost]
        public void Add([FromBody]TestQuestionModel value)
        {
            testQuestions.Add(TestQuestionConverter.TestQuestionModelToTestQuestion(value));
            db.SaveChanges();
        }

        /// <summary>
        /// Method used to update a testQuestion
        /// </summary>
        /// <param name="id">The id of the testQuestion</param>
        /// <param name="value">The new value for the testQuestion</param>
        [HttpPut]
        public void Update(int id, [FromBody]TestQuestionModel value)
        {
            var existingTestQuestion = testQuestions.Find(id);
            if (existingTestQuestion != null)
            {
                existingTestQuestion.CorrectAnswerId = value.CorrectAnswerId;
                existingTestQuestion.Statement = value.Statement;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Method used to delete a testQuestion
        /// </summary>
        /// <param name="id">The id of the testQuestion</param>
        [HttpDelete]
        public void Delete(int id)
        {
            var existingTestQuestion = testQuestions.Find(id);
            if (existingTestQuestion != null)
            {
                testQuestions.Remove(existingTestQuestion);
            }
        }
    }
}
