using DaddyAgencies.Common.Application.Features;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaddyAgencies.Application.Features.Departament
{
  public class SaveDepartament : SaveRequest, IRequest
    {
        public Guid RegionUid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
