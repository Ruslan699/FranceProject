using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaddyAgencies.EntityFramework.Features.Departament
{
    using AutoMapper;
    using DaddyAgencies.Common.Contracts;
    using DaddyAgencies.EntityFramework.Models;
    using System.Threading;
    using ApplicationFeatures = Application.Features.Departament;
    using ApplicationModels = Application.Models;


    public class DepartamentCommandHandler : RequestHandler<ApplicationModels.Departament, DepartamentEntity>, 
        IRequestHandler<ApplicationFeatures.GetDepartamentsView, ResultOfCollection<ApplicationModels.DepartamentView>>,
        IRequestHandler<ApplicationFeatures.GetDepartaments, ResultOfCollection<ApplicationModels.Departament>>,
        IRequestHandler<ApplicationFeatures.SaveDepartament, Result<Guid>>, 
        IRequestHandler<ApplicationFeatures.FindDepartament, Result<ApplicationModels.Departament>>,
        IRequestHandler<ApplicationFeatures.DeleteDepartament, Result>
        
    {
        public DepartamentCommandHandler(IMapper mapper) : base(mapper)
        {
        }

        public Task<ResultOfCollection<ApplicationModels.Departament>> Handle(ApplicationFeatures.GetDepartaments request, 
            CancellationToken cancellationToken) => HandleGetRequest(request, cancellationToken, query => query.OrderBy(x => x.Name));

        public Task<Result<Guid>> Handle(ApplicationFeatures.SaveDepartament request,
            CancellationToken cancellationToken) => HandleSaveRequest(request, cancellationToken);

        public Task<Result<ApplicationModels.Departament>> Handle(ApplicationFeatures.FindDepartament request,
            CancellationToken cancellationToken) => HandleFindRequest(request, cancellationToken);

        public Task<Result> Handle(ApplicationFeatures.DeleteDepartament request, CancellationToken cancellationToken) =>
            HandleDeleteRequest(request, cancellationToken);

        public Task<ResultOfCollection<ApplicationModels.DepartamentView>> Handle(
            ApplicationFeatures.GetDepartamentsView request, CancellationToken cancellationToken) =>
            HandleGetRequest(request, cancellationToken);
    }
}
