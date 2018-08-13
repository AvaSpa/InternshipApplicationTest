using InternshipApplicationTest.Common.ModelClasses;
using InternshipApplicationTest.WebAPI.Models;

namespace InternshipApplicationTest.WebAPI.Classes
{
    public class TestConfigurationConverter
    {
        public static TestConfigurationModel TestConfigurationToTestConfigurationModel(TestConfiguration value)
        {
            return new TestConfigurationModel { Id = value.Id, QuestionNumber = value.QuestionNumber, MinimumScore = value.MinimumScore, TimeLimit = value.TimeLimit };
        }

        public static TestConfiguration TestConfigurationModelToTestConfiguration(TestConfigurationModel value)
        {
            return new TestConfiguration { Id = value.Id, QuestionNumber = value.QuestionNumber, MinimumScore = value.MinimumScore, TimeLimit = value.TimeLimit };
        }
    }
}