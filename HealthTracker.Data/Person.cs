using System;

namespace HealthTracker.Data
{
    public class Person
    {
        public virtual Guid Id { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual string FullName => $"{FirstName} {LastName}";
    }
}