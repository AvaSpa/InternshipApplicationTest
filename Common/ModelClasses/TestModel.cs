using System;
using System.Collections.Generic;

namespace InternshipApplicationTest.Common.ModelClasses
{
    /// <summary>
    /// Model class for the test
    /// </summary>
    public class TestModel
    {
        /// <summary>
        /// The id of the C_ApplicantInternship entity created
        /// </summary>
        public int ApplicantInternshipId { get; set; }

        /// <summary>
        /// The time limit in minutes for the test
        /// </summary>
        public short TimeLimitInMinutes { get; set; }

        /// <summary>
        /// The list of questions
        /// </summary>
        public List<TestQuestionModel> Questions { get; set; }

        public override string ToString()
        {
            return $"ApplicantInternshipId = {ApplicantInternshipId}{Environment.NewLine}" +
                $"TimeLimit = {TimeLimitInMinutes}{Environment.NewLine}" +
                $"Number of questions: {Questions.Count}";
        }
    }
}