using InternshipApplicationTest.Common.ModelClasses;
using InternshipApplicationTest.WebAPI.Models;

namespace InternshipApplicationTest.WebAPI.Classes
{
    public class TestAnswerConverter
    {
        public static TestAnswerModel TestAnswerToTestAnswerModel(TestAnswer value)
        {
            return new TestAnswerModel { Id = value.Id, Content = value.Content, QuestionId = value.N_QuestionId };
        }

        public static TestAnswer TestAnswerModelToTestAnswer(TestAnswerModel value)
        {
            return new TestAnswer { Id = value.Id, Content = value.Content, N_QuestionId = value.QuestionId };
        }
    }
}