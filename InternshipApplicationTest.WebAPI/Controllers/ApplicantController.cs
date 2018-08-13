using InternshipApplicationTest.Common.ModelClasses;
using InternshipApplicationTest.WebAPI.Classes;
using InternshipApplicationTest.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InternshipApplicationTest.WebAPI.Controllers
{
    /// <summary>
    /// The controller for the Applicant entity
    /// </summary>
    public class ApplicantController : ApiController
    {
        private static DBContainer db = new DBContainer();
        private static DbSet<Applicant> applicants = db.Applicants;

        /// <summary>
        /// Method used to get all the applicants
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<ApplicantModel> Get()
        {
            return applicants.ToList().Select(a => ApplicantConverter.ApplicantToApplicantModel(a));
        }

        /// <summary>
        /// Method used to get an applicant
        /// </summary>
        /// <param name="id">The id of the applicant</param>
        /// <returns></returns>
        [HttpGet]
        public ApplicantModel Get(int id)
        {
            return ApplicantConverter.ApplicantToApplicantModel(applicants.Find(id));
        }

        /// <summary>
        /// Method used to add an applicant
        /// </summary>
        /// <param name="value">The applicant to be added</param>
        /// <returns>The id of the newly created applicant or the id of the applicant with the same phone number</returns>
        [HttpPost]
        public int Add([FromBody]ApplicantModel value)
        {
            var existingApplicant = applicants.FirstOrDefault(a => a.PhoneNumber == value.PhoneNumber);
            if (existingApplicant == null)
            {
                var addedApplicant = applicants.Add(ApplicantConverter.ApplicantModelToApplicant(value));
                db.SaveChanges();
                return addedApplicant.Id;
            }
            else
            {
                return existingApplicant.Id;
            }
        }

        /// <summary>
        /// Method used to update an applicant
        /// </summary>
        /// <param name="id">The id of the applicant</param>
        /// <param name="value">The new value for the applicant</param>
        [HttpPut]
        public void Update(int id, [FromBody]ApplicantModel value)
        {
            var existingApplicant = applicants.Find(id);
            if (existingApplicant != null)
            {
                existingApplicant.FirstName = value.FirstName;
                existingApplicant.LastName = value.LastName;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Method used to delete an applicant
        /// </summary>
        /// <param name="id">The id of the applicant</param>
        [HttpDelete]
        public void Delete(int id)
        {
            var existingApplicant = applicants.Find(id);
            if (existingApplicant != null)
            {
                applicants.Remove(existingApplicant);
            }
        }
    }
}
