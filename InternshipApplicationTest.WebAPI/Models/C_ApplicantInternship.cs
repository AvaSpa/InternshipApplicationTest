//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InternshipApplicationTest.WebAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class C_ApplicantInternship
    {
        public int Id { get; set; }
        public Nullable<short> Score { get; set; }
        public Nullable<bool> ApplicantPassedTheTest { get; set; }
        public Nullable<System.DateTime> DateTestTaken { get; set; }
        public int N_ApplicantId { get; set; }
        public int N_InternshipId { get; set; }
    
        public virtual Applicant Applicant { get; set; }
        public virtual Internship Internship { get; set; }
    }
}
