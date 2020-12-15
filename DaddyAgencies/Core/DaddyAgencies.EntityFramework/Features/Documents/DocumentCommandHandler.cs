using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DaddyAgencies.Common.Contracts;
using MediatR;

namespace DaddyAgencies.EntityFramework.Features.Documents
{
    using ApplicationFeatures = Application.Features.Documents;
    using ApplicationModels = Application.Models;
    using Models;
    using DaddyAgencies.Common.Util;

    public class DocumentCommandHandler : RequestHandler<ApplicationModels.DucumentBase, PropertyDocumentEntity>,
        IRequestHandler<ApplicationFeatures.GetDocumentsByProperty, ResultOfCollection<ApplicationModels.DucumentBase>>,
        IRequestHandler<ApplicationFeatures.GetDocumentsByPropertyId, ResultOfCollection<Guid>>,
        IRequestHandler<ApplicationFeatures.GetMainDocumentByProperty, Result<string>>,
        IRequestHandler<ApplicationFeatures.GetMainPropertyImageId, Result<Guid>>,
        IRequestHandler<ApplicationFeatures.DeleteDocument, Result>,
        IRequestHandler<ApplicationFeatures.FindDocument, Result<ApplicationModels.DucumentBase>>
    {
        public DocumentCommandHandler(IMapper mapper) : base(mapper)
        {
        }

        public async Task<ResultOfCollection<ApplicationModels.DucumentBase>> Handle(
            ApplicationFeatures.GetDocumentsByProperty request, CancellationToken cancellationToken)
        {
            ResultOfCollection<ApplicationModels.DucumentBase> models;
            using (var db = new ProjectDbContext())
            {
                var queryable = db.Set<PropertyDocumentEntity>()
                    .AsNoTracking()
                    .Where(c => (request.IcludeInactivcve || !c.IsDeleted) && c.PropertyUid == request.PropertyUid)
                    .OrderBy(x => x.RowNo);
                models = await queryable.ProjectToListAsync<ApplicationModels.DucumentBase>(Mapper.ConfigurationProvider)
                    .Map(request.AsResultOfCollection);
            }
            return models;
        }

        public async Task<ResultOfCollection<Guid>> Handle(ApplicationFeatures.GetDocumentsByPropertyId request, CancellationToken cancellationToken)
        {
            ResultOfCollection<Guid> models;
            using (var db = new ProjectDbContext())
            {
                var queryable = db.Set<PropertyDocumentEntity>()
                    .AsNoTracking()
                    .Where(c => (request.IcludeInactivcve || !c.IsDeleted) && c.PropertyUid == request.PropertyUid)
                    .OrderBy(x => x.RowNo)
                    .Select(x=>x.Uid);
                models = new ResultOfCollection<Guid>(request.Id, queryable.ToList(), request.LeadTime);
            }
            return models;
        }

        public async Task<Result<string>> Handle(ApplicationFeatures.GetMainDocumentByProperty request, CancellationToken cancellationToken)
        {
            byte[] imageBytes;
            using (var db = new ProjectDbContext())
            {
                imageBytes = await db.PropertyDocumentEntities
                    .AsNoTracking()
                    .Where(c => !c.IsDeleted && c.PropertyUid == request.Uid)
                    .OrderBy(x => x.RowNo)
                    .Select(x => x.Body)
                    .FirstOrDefaultAsync(cancellationToken);
            }

            if (imageBytes == null)
                throw new ObjectNotFoundException("Can't find image for property");

            Image image;
            using (var ms = new MemoryStream(imageBytes))
                image = Image.FromStream(ms);

            var thumb = image.GetThumbnailImage(648, 426, () => false, IntPtr.Zero);
            var imageConverter = new ImageConverter();
            var thumbBytes = (byte[])imageConverter.ConvertTo(thumb, typeof(byte[]));
            if (thumbBytes == null)
                throw new ObjectNotFoundException("Can't convert thumbnal");

            return request.AsResultOf(Convert.ToBase64String(thumbBytes));
        }

        public async Task<Result<Guid>> Handle(ApplicationFeatures.GetMainPropertyImageId request, CancellationToken cancellationToken)
        {

            Guid imageId;
            using (var db = new ProjectDbContext())
            {
                imageId = await db.PropertyDocumentEntities
                    .AsNoTracking()
                    .Where(c => !c.IsDeleted && c.PropertyUid == request.Uid)
                    .OrderBy(x => x.RowNo)
                    .Select(x => x.Uid)
                    .FirstOrDefaultAsync(cancellationToken);
            }

            return request.AsResultOf(imageId);
        }

        public Task<Result> Handle(ApplicationFeatures.DeleteDocument request, CancellationToken cancellationToken) =>
            HandleDeleteRequest(request, cancellationToken);

        public Task<Result<ApplicationModels.DucumentBase>> Handle(ApplicationFeatures.FindDocument request,
             CancellationToken cancellationToken) => HandleFindRequest(request, cancellationToken);
    }
}
