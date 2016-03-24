namespace Model.Seeding
{
    using System;
    using Convenient.Data.Entity.Seeding;
    using Model.Models;

    public class StudentFactory : ISeedDataFactory<Student>
    {
        public Student[] All()
        {
            return new[]
            {
                new Student { FirstMidName = "Jack", LastName = "Black", EnrollmentDate = DateTime.Now, }, 
                new Student { FirstMidName = "Ethan", LastName = "Carmine", EnrollmentDate = DateTime.Now, }
            };
        }
    }
}
