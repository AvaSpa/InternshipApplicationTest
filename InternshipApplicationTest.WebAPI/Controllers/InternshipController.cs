using InternshipApplicationTest.Common.ModelClasses;
using InternshipApplicationTest.WebAPI.Classes;
using InternshipApplicationTest.WebAPI.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace InternshipApplicationTest.WebAPI.Controllers
{
    /// <summary>
    /// The controller for the Internship entity
    /// </summary>
    public class InternshipController : ApiController
    {
        private DBContainer db;
        private DbSet<Internship> internships;

        public InternshipController()
        {
            db = new DBContainer();
            internships = db.Internships;
        }

        /// <summary>
        /// Method used to get all the internships
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<InternshipModel> Get()
        {
            return internships.ToList().Select(a => InternshipConverter.InternshipToInternshipModel(a));
        }

        /// <summary>
        /// Method used to get an internship
        /// </summary>
        /// <param name="id">The id of the applicant</param>
        /// <returns></returns>
        [HttpGet]
        public InternshipModel Get(int id)
        {
            return InternshipConverter.InternshipToInternshipModel(internships.Find(id));
        }

        /// <summary>
        /// Method used to add an internship
        /// </summary>
        /// <param name="value">The internship to be added</param>
        [HttpPost]
        public void Add([FromBody]InternshipModel value)
        {
            internships.Add(InternshipConverter.InternshipModelToInternship(value));
            db.SaveChanges();
        }

        /// <summary>
        /// Method used to update an internship
        /// </summary>
        /// <param name="id">The id of the internship</param>
        /// <param name="value">The new value for the internship</param>
        [HttpPut]
        public void Update(int id, [FromBody]InternshipModel value)
        {
            var existingInternship = internships.Find(id);
            if (existingInternship != null)
            {
                existingInternship.Name = value.Name;
                existingInternship.Year = value.Year;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Method used to delete an internship
        /// </summary>
        /// <param name="id">The id of the internship</param>
        [HttpDelete]
        public void Delete(int id)
        {
            var existingInternship = internships.Find(id);
            if (existingInternship != null)
            {
                internships.Remove(existingInternship);
            }
        }
    }
}
