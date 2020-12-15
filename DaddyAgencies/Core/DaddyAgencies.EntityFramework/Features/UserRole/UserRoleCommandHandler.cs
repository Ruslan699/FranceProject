using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaddyAgencies.EntityFramework.Features.UserRole
{
    using AutoMapper;
    using DaddyAgencies.Common.Contracts;
    using System.Threading;
    using ApplicationFeatures = Application.Features.UserRole;
    using ApplicationModels = Application.Models;
    class UserRoleCommandHandler : RequestHandler,
        IRequestHandler<ApplicationFeatures.GetUsersRoles, Result<IQueryable<ApplicationModels.UserRole>>>
    {
        public UserRoleCommandHandler(IMapper mapper) : base(mapper)
        {

        }
        public Task<Result<IQueryable<ApplicationModels.UserRole>>> Handle(ApplicationFeatures.GetUsersRoles request, CancellationToken cancellationToken)
        {
            var userroles = new ProjectDbContext().Roles.ProjectToQueryable<ApplicationModels.UserRole>(Mapper.ConfigurationProvider);
            return Task.FromResult(request.AsResultOf(userroles));
        }

        //public Task<Result<Guid>> Handle(ApplicationFeatures.SaveUserRole request, CancellationToken cancellationToken) =>
        //    HandleSaveRequest(request, cancellationToken);
    }
}
