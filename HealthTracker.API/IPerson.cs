using System;

namespace HealthTracker.API
{
    public interface IPerson
    {
        Guid Id { get; }

        string FirstName { get; }

        string LastName { get; }

        string FullName { get; }

        Sex Sex { get; }

        int Age { get; }

        int Height { get; }
    }
}