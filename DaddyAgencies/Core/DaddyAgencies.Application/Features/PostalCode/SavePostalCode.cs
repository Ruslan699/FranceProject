using DaddyAgencies.Common.Application.Features;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaddyAgencies.Application.Features.PostalCode
{
   public class SavePostalCode : SaveRequest, IRequest
    {
        public Guid DepartamentUid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
