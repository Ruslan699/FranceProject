using System;
using System.IO;
using System.Linq;
using System.Web;
using AutoMapper;
using DaddyAgencies.Common.EntityFramework.Identity;

namespace AdminUI.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DaddyAgencies.Application.Models.RegionMapData, Models.RegionModel>()
                .ForMember(dest => dest.Uid, opt => opt.MapFrom(src => src.Uid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.PathString, opt => opt.MapFrom(src => src.PathString))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForAllOtherMembers(opt => opt.Ignore());


            CreateMap<Models.RegionModel, DaddyAgencies.Application.Features.Region.SaveRegion>()
                .ForMember(dest => dest.Uid, opt => opt.MapFrom(src => src.Uid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.PathString, opt => opt.MapFrom(src => src.PathString))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<Models.UserModel, DaddyAgencies.Application.Features.User.SaveUser>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<Models.UserModel, IdentityUser>()
               //.ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Fullname))
               .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
               .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
               .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<Models.CreateModel, IdentityUser>()
                //.ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Fullname))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                //.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Fullname))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<IdentityUser, Models.CreateModel > ()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Fullname, opt => opt.MapFrom(src => src.UserName))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<Models.UserRoleModel, DaddyAgencies.Application.Features.UserRole.SaveUserRole>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId))
            .ForAllOtherMembers(opt => opt.Ignore());


            CreateMap<Models.AnnouncementModel, DaddyAgencies.Application.Features.Property.SaveProperty >()
                .ForMember(dest => dest.Uid, opt => opt.MapFrom(src => src.Uid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Longitude))
                .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Latitude))
                .ForMember(dest => dest.PastalCodeUid, opt => opt.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.SalePrice, opt => opt.MapFrom(src => src.SalePrice))
                .ForMember(dest => dest.TotalSquare, opt => opt.MapFrom(src => src.TotalSquare))
                .ForMember(dest => dest.Files, opt => opt.MapFrom(src => src.Files.Where(x => x != null)))
                .ForMember(dest => dest.DepartamentUid, opt => opt.MapFrom(src => src.DepartamentUid))
                .ForMember(dest => dest.PropertyType, opt => opt.MapFrom(src => src.PropertyType))
                .ForMember(dest => dest.FloorsNumber, opt => opt.MapFrom(src => src.FloorsNumber))
                .ForMember(dest => dest.RoomsCount, opt => opt.MapFrom(src => src.RoomsCount))
                .ForMember(dest => dest.Ges, opt => opt.MapFrom(src => src.Ges))
                .ForMember(dest => dest.EnergyClass, opt => opt.MapFrom(src => src.EnergyClass))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Parking, opt => opt.MapFrom(src => src.Parking))
                .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.OwnerName))
                .ForMember(dest => dest.OwnerEmail, opt => opt.MapFrom(src => src.OwnerEmail))
                .ForMember(dest => dest.OwnerPhone, opt => opt.MapFrom(src => src.OwnerPhone))
                .ForMember(dest => dest.OwnerNote, opt => opt.MapFrom(src => src.OwnerNote))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<Models.PropertyDocumentRowNoModel, DaddyAgencies.Application.Features.Property.UpdatePropertyDocumentRowNo>()
                .ForMember(dest => dest.PropertyUids, opt => opt.MapFrom(src => src.PropertyUids))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<HttpPostedFileBase, DaddyAgencies.Application.Models.DucumentBase>()
                .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.FileName))
                .ForMember(dest => dest.Extension, opt => opt.MapFrom(src => Path.GetExtension(src.FileName)))
                .ForMember(dest => dest.Body, opt => opt.MapFrom(src => MapImageStream(src)))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<DaddyAgencies.Application.Models.PropertyPreview, Models.AnnouncementModel>()
                .ForMember(dest => dest.Uid, opt => opt.MapFrom(src => src.Uid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCodeUid))
                .ForMember(dest => dest.DepartamentUid, opt => opt.MapFrom(src => src.DepartamentUid))
                .ForMember(dest => dest.SalePrice, opt => opt.MapFrom(src => src.SalePrice))
                .ForMember(dest => dest.TotalSquare, opt => opt.MapFrom(src => src.TotalSquare))
                .ForMember(dest => dest.RoomsCount, opt => opt.MapFrom(src => src.RoomsCount))
                .ForMember(dest => dest.FloorsNumber, opt => opt.MapFrom(src => src.FloorsNumber))
                .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Latitude))
                .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Longitude))
                .ForMember(dest => dest.Ges, opt => opt.MapFrom(src => src.Ges))
                .ForMember(dest => dest.EnergyClass, opt => opt.MapFrom(src => src.EnergyClass))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Parking, opt => opt.MapFrom(src => src.Parking))
                .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.OwnerName))
                .ForMember(dest => dest.OwnerEmail, opt => opt.MapFrom(src => src.OwnerEmail))
                .ForMember(dest => dest.OwnerPhone, opt => opt.MapFrom(src => src.OwnerPhone))
                .ForMember(dest => dest.OwnerNote, opt => opt.MapFrom(src => src.OwnerNote))
                .ForMember(dest => dest.PropertyType, opt => opt.MapFrom(src => src.PropertyType))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<Models.DepartamentModel, DaddyAgencies.Application.Features.Departament.SaveDepartament>()
                .ForMember(dest => dest.RegionUid, opt => opt.MapFrom(src => src.RegionModelUid ?? default(Guid)))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Uid, opt => opt.MapFrom(src => src.Uid ?? default(Guid)))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<DaddyAgencies.Application.Models.Departament, Models.DepartamentModel>()
                .ForMember(dest => dest.RegionModelUid, opt => opt.MapFrom(src => src.RegionUid ?? default(Guid)))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Uid, opt => opt.MapFrom(src => src.Uid ?? default(Guid)))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<Models.PostalCodeModel, DaddyAgencies.Application.Features.PostalCode.SavePostalCode>()
                .ForMember(dest => dest.DepartamentUid, opt => opt.MapFrom(src => src.DepartamentModelUid ?? default(Guid)))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Uid, opt => opt.MapFrom(src => src.Uid ?? default(Guid)))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<DaddyAgencies.Application.Models.PostalCode, Models.PostalCodeModel>()
                .ForMember(dest => dest.DepartamentModelUid, opt => opt.MapFrom(src => src.DepartamentUid))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Uid, opt => opt.MapFrom(src => src.Uid))
                .ForAllOtherMembers(opt => opt.Ignore());

        }

        private byte[] MapImageStream(HttpPostedFileBase httpFile)
        {
            byte[] data;
            using (var inputStream = httpFile.InputStream)
            {
                if (!(inputStream is MemoryStream memoryStream))
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }
                data = memoryStream.ToArray();
            }
            return data;
        }
    }
}