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
    /// The controller for the TestAnswer entity
    /// </summary>
    public class TestAnswerController : ApiController
    {
        private static DBContainer db = new DBContainer();
        private static DbSet<TestAnswer> testAnswers = db.TestAnswers;

        /// <summary>
        /// Method used to get all the testAnswers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<TestAnswerModel> Get()
        {
            return testAnswers.ToList().Select(a => TestAnswerConverter.TestAnswerToTestAnswerModel(a));
        }

        /// <summary>
        /// Method used to get a testAnswer
        /// </summary>
        /// <param name="id">The id of the testAnswer</param>
        /// <returns></returns>
        [HttpGet]
        public TestAnswerModel Get(int id)
        {
            return TestAnswerConverter.TestAnswerToTestAnswerModel(testAnswers.Find(id));
        }

        /// <summary>
        /// Method used to add a testAnswer
        /// </summary>
        /// <param name="value">The testAnswer to be added</param>
        [HttpPost]
        public void Add([FromBody]TestAnswerModel value)
        {
            testAnswers.Add(TestAnswerConverter.TestAnswerModelToTestAnswer(value));
            db.SaveChanges();
        }

        /// <summary>
        /// Method used to update a testAnswer
        /// </summary>
        /// <param name="id">The id of the testAnswer</param>
        /// <param name="value">The new value for the testAnswer</param>
        [HttpPut]
        public void Update(int id, [FromBody]TestAnswerModel value)
        {
            var existingTestAnswer = testAnswers.Find(id);
            if (existingTestAnswer != null)
            {
                existingTestAnswer.Content = value.Content;
                existingTestAnswer.N_QuestionId = value.QuestionId;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Method used to delete a testAnswer
        /// </summary>
        /// <param name="id">The id of the testAnswer</param>
        [HttpDelete]
        public void Delete(int id)
        {
            var existingTestAnswer = testAnswers.Find(id);
            if (existingTestAnswer != null)
            {
                testAnswers.Remove(existingTestAnswer);
            }
        }
    }
}
