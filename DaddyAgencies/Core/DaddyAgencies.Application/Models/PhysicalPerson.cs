
using System;

namespace DaddyAgencies.Application.Models
{
    public class PhysicalPerson
    {
        public Guid? Uid { get; set; }

        public string Surname { get; set; }
        
        public string Name { get; set; }
        
        public string LastName { get; set; }
        
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string Skype { get; set; }
        
        public DateTime? DateOfBirth { get; set; }
        
        public bool? Gender { get; set; }
       
        public string Address { get; set; }
        
        public Guid UserUid { get; set; }
    }
}
