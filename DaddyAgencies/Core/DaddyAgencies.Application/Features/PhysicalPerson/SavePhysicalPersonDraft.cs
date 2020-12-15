using System;
using DaddyAgencies.Common.Application.Features;
using MediatR;

namespace DaddyAgencies.Application.Features.PhysicalPerson
{
    public class SavePhysicalPersonDraft : SaveRequest, IRequest
    {

        public Guid UserUid { get; set; }

        public string Surname { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
