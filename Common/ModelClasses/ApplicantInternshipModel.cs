using System;

namespace InternshipApplicationTest.Common.ModelClasses
{
    /// <summary>
    /// Model class for the C_ApplicantInternship entity
    /// </summary>
    public class ApplicantInternshipModel
    {
        /// <summary>
        /// The Id of the ApplicantInternship
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The score obtained at the internship test
        /// </summary>
        public short? Score { get; set; }

        /// <summary>
        /// The applicant passed the test
        /// </summary>
        public bool? ApplicantPassedTheTest { get; set; }

        /// <summary>
        /// The date when the test was taken
        /// </summary>
        public DateTime? DateTestTaken { get; set; }

        /// <summary>
        /// The Id of the applicant taking the test
        /// </summary>
        public int ApplicantId { get; set; }

        /// <summary>
        /// The Id of the internship for which the test was taken
        /// </summary>
        public int InternshipId { get; set; }
    }
}