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
    /// The controller for the TestConfiguration entity
    /// </summary>
    public class TestConfigurationController : ApiController
    {
        private static DBContainer db = new DBContainer();
        private static DbSet<TestConfiguration> testConfigurations = db.TestConfigurations;

        /// <summary>
        /// Method used to get all the testConfigurations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<TestConfigurationModel> Get()
        {
            return testConfigurations.ToList().Select(a => TestConfigurationConverter.TestConfigurationToTestConfigurationModel(a));
        }

        /// <summary>
        /// Method used to get a testConfiguration
        /// </summary>
        /// <param name="id">The id of the testConfiguration</param>
        /// <returns></returns>
        [HttpGet]
        public TestConfigurationModel Get(int id)
        {
            return TestConfigurationConverter.TestConfigurationToTestConfigurationModel(testConfigurations.Find(id));
        }

        /// <summary>
        /// Method used to add a testConfiguration
        /// </summary>
        /// <param name="value">The testConfiguration to be added</param>
        [HttpPost]
        public void Add([FromBody]TestConfigurationModel value)
        {
            testConfigurations.Add(TestConfigurationConverter.TestConfigurationModelToTestConfiguration(value));
            db.SaveChanges();
        }

        /// <summary>
        /// Method used to update a testConfiguration
        /// </summary>
        /// <param name="id">The id of the testConfiguration</param>
        /// <param name="value">The new value for the testConfiguration</param>
        [HttpPut]
        public void Update(int id, [FromBody]TestConfigurationModel value)
        {
            var existingTestConfiguration = testConfigurations.Find(id);
            if (existingTestConfiguration != null)
            {
                existingTestConfiguration.MinimumScore = value.MinimumScore;
                existingTestConfiguration.QuestionNumber = value.QuestionNumber;
                existingTestConfiguration.TimeLimit = value.TimeLimit;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Method used to delete a testConfiguration
        /// </summary>
        /// <param name="id">The id of the testConfiguration</param>
        [HttpDelete]
        public void Delete(int id)
        {
            var existingTestConfiguration = testConfigurations.Find(id);
            if (existingTestConfiguration != null)
            {
                testConfigurations.Remove(existingTestConfiguration);
            }
        }
    }
}
