using System.Collections.Generic;
using DaddyAgencies.Application.Models;
using DaddyAgencies.Common.Application.Features;
using MediatR;

namespace DaddyAgencies.Application.Features.Recruiment
{
    public class SaveRecruitment : SaveRequest, IRequest
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public List<DucumentBase> Documents { get; set; }
    }
}
