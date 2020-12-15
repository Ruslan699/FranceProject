using DaddyAgencies.Common.Application.Features;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaddyAgencies.Application.Features.Role
{
    public class GetRoles : UidRequest<IQueryable<Models.Role>>, IRequest
    {
        public GetRoles()
        {

        }
    }
}
