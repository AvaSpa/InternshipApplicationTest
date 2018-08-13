namespace InternshipApplicationTest.Common.ModelClasses
{
    /// <summary>
    /// Model class for the TestQuestion entity
    /// </summary>
    public class TestQuestionModel
    {
        /// <summary>
        /// The Id of the applicant
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The statement of the question
        /// </summary>
        public string Statement { get; set; }

        /// <summary>
        /// The Id of the correct answer
        /// </summary>
        public int CorrectAnswerId { get; set; }
    }
}