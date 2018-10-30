namespace InternshipApplicationTest.Common.ModelClasses
{
    /// <summary>
    /// Model class for the TestAnswer entity
    /// </summary>
    public class TestAnswerModel
    {
        /// <summary>
        /// The Id of the answer
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The content of the answer
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// The Id of the question for which this answer is a possible answer
        /// </summary>
        public int QuestionId { get; set; }

        public override string ToString()
        {
            return Content;
        }

        public override bool Equals(object obj)
        {
            try
            {
                var other = (TestAnswerModel)obj;
                return this.Id.Equals(other.Id);
            }
            catch (System.InvalidCastException)
            {
                return false;
            }
        }
    }
}