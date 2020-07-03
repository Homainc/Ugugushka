using System;

namespace Ugugushka.Common.Interfaces
{
    public interface IOrderFilterInfo
    {
        DateTime Date { get; }
        string FirstName { get; }
        string LastName { get; }
        string PhoneNumber { get; }
        string Email { get; }
    }
}
