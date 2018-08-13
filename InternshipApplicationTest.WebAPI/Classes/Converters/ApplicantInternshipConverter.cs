using InternshipApplicationTest.Common.ModelClasses;
using InternshipApplicationTest.WebAPI.Models;

namespace InternshipApplicationTest.WebAPI.Classes
{
    public class ApplicantInternshipConverter
    {
        public static ApplicantInternshipModel ApplicantInternshipToApplicantInternshipModel(C_ApplicantInternship value)
        {
            return new ApplicantInternshipModel { Id = value.Id, Score = value.Score, DateTestTaken = value.DateTestTaken, ApplicantPassedTheTest = value.ApplicantPassedTheTest, ApplicantId = value.N_ApplicantId, InternshipId = value.N_InternshipId };
        }

        public static C_ApplicantInternship ApplicantInternshipModelToApplicantInternship(ApplicantInternshipModel value)
        {
            return new C_ApplicantInternship { Id = value.Id, Score = value.Score, DateTestTaken = value.DateTestTaken, ApplicantPassedTheTest = value.ApplicantPassedTheTest, N_ApplicantId = value.ApplicantId, N_InternshipId = value.InternshipId };
        }
    }
}