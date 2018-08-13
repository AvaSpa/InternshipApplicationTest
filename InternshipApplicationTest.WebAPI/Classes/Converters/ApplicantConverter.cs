using InternshipApplicationTest.Common.ModelClasses;
using InternshipApplicationTest.WebAPI.Models;

namespace InternshipApplicationTest.WebAPI.Classes
{
    public class ApplicantConverter
    {
        public static ApplicantModel ApplicantToApplicantModel(Applicant value)
        {
            return new ApplicantModel { Id = value.Id, FirstName = value.FirstName, LastName = value.LastName, PhoneNumber = value.PhoneNumber };
        }

        public static Applicant ApplicantModelToApplicant(ApplicantModel value)
        {
            return new Applicant { Id = value.Id, FirstName = value.FirstName, LastName = value.LastName, PhoneNumber = value.PhoneNumber };
        }
    }
}