using DaddyAgencies.Common.Application.Features;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaddyAgencies.Application.Features.Documents
{
    public class FindDocument : FindRequest<Models.DucumentBase>, IRequest
    {
        public FindDocument(Guid uid) : base(uid)
        {

        }
    }
}
