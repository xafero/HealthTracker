using HealthTracker.API;
using System;

namespace HealthTracker.Data
{
    public class Person : IPerson
    {
        public virtual Guid Id { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual string FullName => $"{FirstName} {LastName}";

        public virtual Sex Sex { get; set; }

        public virtual int Height { get; set; }

        public virtual DateTime Birthday { get; set; }

        public virtual int Age => (int)((DateTime.Now - Birthday).TotalDays / 365);
    }
}