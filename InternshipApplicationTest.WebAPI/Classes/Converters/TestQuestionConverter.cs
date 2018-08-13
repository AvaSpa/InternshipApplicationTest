using InternshipApplicationTest.Common.ModelClasses;
using InternshipApplicationTest.WebAPI.Models;

namespace InternshipApplicationTest.WebAPI.Classes
{
    public class TestQuestionConverter
    {
        public static TestQuestionModel TestQuestionToTestQuestionModel(TestQuestion value)
        {
            return new TestQuestionModel { Id = value.Id, Statement = value.Statement, CorrectAnswerId = value.CorrectAnswerId };
        }

        public static TestQuestion TestQuestionModelToTestQuestion(TestQuestionModel value)
        {
            return new TestQuestion { Id = value.Id, Statement = value.Statement, CorrectAnswerId = value.CorrectAnswerId };
        }
    }
}