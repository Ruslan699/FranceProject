using System;

namespace WebApp.Models
{
    public class PhysicalPersonModel
    {
        public Guid? Uid { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Skype { get; set; }

        public string DateOfBirth { get; set; }

        public string Gender { get; set; }

        public string Address { get; set; }

        public Guid UserUid { get; set; }
    }
}