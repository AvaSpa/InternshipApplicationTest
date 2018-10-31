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
    /// The controller for the ApplicantInternship entity
    /// </summary>
    public class ApplicantInternshipController : ApiController
    {
        private DBContainer db;
        private DbSet<C_ApplicantInternship> applicantInternships;

        public ApplicantInternshipController()
        {
            db = new DBContainer();
            applicantInternships = db.C_ApplicantInternship;
        }

        /// <summary>
        /// Method used to get all the applicantInternships
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<ApplicantInternshipModel> Get()
        {
            return applicantInternships.ToList().Select(a => ApplicantInternshipConverter.ApplicantInternshipToApplicantInternshipModel(a));
        }

        /// <summary>
        /// Method used to get an applicantInternship
        /// </summary>
        /// <param name="id">The id of the applicant</param>
        /// <returns></returns>
        [HttpGet]
        public ApplicantInternshipModel Get(int id)
        {
            return ApplicantInternshipConverter.ApplicantInternshipToApplicantInternshipModel(applicantInternships.Find(id));
        }

        /// <summary>
        /// Method used to add an applicantInternship
        /// </summary>
        /// <param name="value">The applicantInternship to be added</param>
        [HttpPost]
        public void Add([FromBody]ApplicantInternshipModel value)
        {
            applicantInternships.Add(ApplicantInternshipConverter.ApplicantInternshipModelToApplicantInternship(value));
            db.SaveChanges();
        }

        /// <summary>
        /// Method used to update an applicantInternship
        /// </summary>
        /// <param name="id">The id of the applicantInternship</param>
        /// <param name="value">The new value for the applicantInternship</param>
        [HttpPut]
        public void Update(int id, [FromBody]ApplicantInternshipModel value)
        {
            var existingApplicantInternship = applicantInternships.Find(id);
            if (existingApplicantInternship != null)
            {
                existingApplicantInternship.Score = value.Score;
                existingApplicantInternship.ApplicantPassedTheTest = value.ApplicantPassedTheTest;
                existingApplicantInternship.DateTestTaken = value.DateTestTaken;
                existingApplicantInternship.N_InternshipId = value.InternshipId;
                existingApplicantInternship.N_ApplicantId = value.ApplicantId;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Method used to delete an applicantInternship
        /// </summary>
        /// <param name="id">The id of the applicantInternship</param>
        [HttpDelete]
        public void Delete(int id)
        {
            var existingApplicantInternship = applicantInternships.Find(id);
            if (existingApplicantInternship != null)
            {
                applicantInternships.Remove(existingApplicantInternship);
            }
        }
    }
}
