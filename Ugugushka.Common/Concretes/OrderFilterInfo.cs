using System;
using Ugugushka.Common.Interfaces;

namespace Ugugushka.Common.Concretes
{
    public class OrderFilterInfo : IOrderFilterInfo
    {
        public DateTime Date { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
