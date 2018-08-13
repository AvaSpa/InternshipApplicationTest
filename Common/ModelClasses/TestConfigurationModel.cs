using System;

namespace InternshipApplicationTest.Common.ModelClasses
{
    /// <summary>
    /// Model class for the TestConfiguration entity
    /// </summary>
    public class TestConfigurationModel
    {
        /// <summary>
        /// The Id of the configuration
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The number of questions that should be selected for a test
        /// </summary>
        public short QuestionNumber { get; set; }

        /// <summary>
        /// The minimum score required to pass the test
        /// </summary>
        public short MinimumScore { get; set; }

        /// <summary>
        /// The time limit for a test
        /// </summary>
        public TimeSpan TimeLimit { get; set; }
    }
}