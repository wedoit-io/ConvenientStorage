namespace Model.DataAccessLayer.Contexts
{
    using System.Data.Entity;
    using Model.Models;

    public class SchoolContext : DbContextWithOurConventions
    {
        public SchoolContext()
            : base("SchoolDatabase")
        {
        }

        public virtual DbSet<Student> Students { get; set; }

        public virtual DbSet<Enrollment> Enrollments { get; set; }

        public virtual DbSet<Course> Courses { get; set; }
    }
}
