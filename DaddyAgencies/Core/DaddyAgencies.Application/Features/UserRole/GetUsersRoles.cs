using DaddyAgencies.Common.Application.Features;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaddyAgencies.Application.Features.UserRole
{
    public class GetUsersRoles : UidRequest<IQueryable<Models.UserRole>>, IRequest
    {
        public GetUsersRoles()
        {

        }
    }
}
