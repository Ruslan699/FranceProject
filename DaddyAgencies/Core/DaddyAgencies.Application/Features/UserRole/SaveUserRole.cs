using DaddyAgencies.Common.Application.Features;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaddyAgencies.Application.Features.UserRole
{
    public class SaveUserRole : SaveRequest, IRequest
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
