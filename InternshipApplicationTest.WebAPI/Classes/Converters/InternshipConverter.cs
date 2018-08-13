using InternshipApplicationTest.Common.ModelClasses;
using InternshipApplicationTest.WebAPI.Models;

namespace InternshipApplicationTest.WebAPI.Classes
{
    public class InternshipConverter
    {
        public static InternshipModel InternshipToInternshipModel(Internship value)
        {
            return new InternshipModel { Id = value.Id, Name = value.Name, Year = value.Year };
        }

        public static Internship InternshipModelToInternship(InternshipModel value)
        {
            return new Internship { Id = value.Id, Name = value.Name, Year = value.Year };
        }
    }
}