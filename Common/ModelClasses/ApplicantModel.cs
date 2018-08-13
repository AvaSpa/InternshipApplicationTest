namespace InternshipApplicationTest.Common.ModelClasses
{
    /// <summary>
    /// Model class for the Applicant entity
    /// </summary>
    public class ApplicantModel
    {
        /// <summary>
        /// The id of the applicant
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The first name of the applicant
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the applicant
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The phone number of the applicant
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}