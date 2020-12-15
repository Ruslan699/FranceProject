using DaddyAgencies.Common.Application.Features;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaddyAgencies.Application.Features.User
{
    public class FindUser : FindRequest<Models.User>, IRequest
    {
        public FindUser(Guid uid) : base(uid)
        {

        }
    }
}
