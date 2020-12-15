using DaddyAgencies.Common.Application.Features;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaddyAgencies.Application.Features.Departament
{
   public class FindDepartament : FindRequest<Models.Departament>, IRequest
    {
        public FindDepartament(Guid uid) : base(uid)
        {

        }
    }
}
