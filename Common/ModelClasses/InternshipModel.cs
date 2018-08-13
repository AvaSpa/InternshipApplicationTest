namespace InternshipApplicationTest.Common.ModelClasses
{
    /// <summary>
    /// Model class for the Internship entity
    /// </summary>
    public class InternshipModel
    {
        /// <summary>
        /// The Id of the internship
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the internship
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The year of the internship
        /// </summary>
        public int Year { get; set; }

        public override string ToString()
        {
            return $"{Name} {Year}";
        }
    }
}