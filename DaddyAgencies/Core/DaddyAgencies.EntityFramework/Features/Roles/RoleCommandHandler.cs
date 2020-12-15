using AutoMapper;
using DaddyAgencies.Common.Contracts;
using DaddyAgencies.Common.EntityFramework.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DaddyAgencies.EntityFramework.Features.Roles
{
    using ApplicationFeatures = Application.Features.Role;
    using ApplicationModels = Application.Models;
    public class RoleCommandHandler : RequestHandler,
        IRequestHandler<ApplicationFeatures.GetRoles, Result<IQueryable<ApplicationModels.Role>>>
        
    {

        public RoleCommandHandler(IMapper mapper) : base(mapper)
        {

        }

        public Task<Result<IQueryable<ApplicationModels.Role>>> Handle(ApplicationFeatures.GetRoles request, CancellationToken cancellationToken)
        {
            var roles = new ProjectDbContext().Roles.ProjectToQueryable<ApplicationModels.Role>(Mapper.ConfigurationProvider);
            return Task.FromResult(request.AsResultOf(roles));
        }
    }
}
