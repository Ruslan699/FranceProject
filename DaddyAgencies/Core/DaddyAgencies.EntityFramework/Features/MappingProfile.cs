using AutoMapper;
using DaddyAgencies.Common.AutoMapper;
using DaddyAgencies.Common.EntityFramework.Identity;
using DaddyAgencies.EntityFramework.Models;
using System.Linq;

namespace DaddyAgencies.EntityFramework.Features
{
    using Application = Application.Models;
    using Features = Application.Features;


    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            #region SaveRequests

            CreateMap<Features.Departament.SaveDepartament, DepartamentEntity>()
                .ForMember(dest => dest.Uid, opt => opt.MapFrom(src => src.Uid))
                .ForMember(dest => dest.RegionUid, opt => opt.MapFrom(src => src.RegionUid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .BaseEntityAfterMap()
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<Features.PostalCode.SavePostalCode, PostalCodeEntity>()
                .ForMember(dest => dest.Uid, opt => opt.MapFrom(src => src.Uid))
                .ForMember(dest => dest.DepartamentUid, opt => opt.MapFrom(src => src.DepartamentUid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .BaseEntityAfterMap()
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<Features.Property.SaveProperty, PropertyEntity>()
                .ForMember(dest => dest.Uid, opt => opt.MapFrom(src => src.Uid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Longitude))
                .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Latitude))
                .ForMember(dest => dest.PostalCodeUid, opt => opt.MapFrom(src => src.PastalCodeUid))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.SalePrice, opt => opt.MapFrom(src => src.SalePrice))
                .ForMember(dest => dest.TotalSquare, opt => opt.MapFrom(src => src.TotalSquare))
                .ForMember(dest => dest.PropertyType, opt => opt.MapFrom(src => src.PropertyType))
                .ForMember(dest => dest.FloorsNumber, opt => opt.MapFrom(src => src.FloorsNumber))
                .ForMember(dest => dest.FloorsOutOf, opt => opt.MapFrom(src => src.FloorsOutOf))
                .ForMember(dest => dest.ParkingType, opt => opt.MapFrom(src => src.ParkingType))
                .ForMember(dest => dest.ParkingSize, opt => opt.MapFrom(src => src.ParkingSize))
                .ForMember(dest => dest.ParkingCost, opt => opt.MapFrom(src => src.ParkingCost))
                .ForMember(dest => dest.RoomsCount, opt => opt.MapFrom(src => src.RoomsCount))
                .ForMember(dest => dest.Ges, opt => opt.MapFrom(src => src.Ges))
                .ForMember(dest => dest.EnergyClass, opt => opt.MapFrom(src => src.EnergyClass))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Parking, opt => opt.MapFrom(src => src.Parking))
                .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.OwnerName))
                .ForMember(dest => dest.OwnerEmail, opt => opt.MapFrom(src => src.OwnerEmail))
                .ForMember(dest => dest.OwnerPhone, opt => opt.MapFrom(src => src.OwnerPhone))
                .ForMember(dest => dest.OwnerNote, opt => opt.MapFrom(src => src.OwnerNote))
                .BaseEntityAfterMap()
                .ForAllOtherMembers(opt => opt.Ignore());



            CreateMap<Features.PhysicalPerson.SavePhysicalPersonDraft, PhysicalPersonEntity>()
                .ForMember(dest => dest.Uid, opt => opt.MapFrom(src => src.Uid))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.UserUid, opt => opt.MapFrom(src => src.UserUid))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .BaseEntityAfterMap()
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<Features.Inseption.SaveInseption, InseptionEntity>()
                .ForMember(dest => dest.Uid, opt => opt.MapFrom(src => src.Uid))
                .ForMember(dest => dest.CustomerFullName, opt => opt.MapFrom(src => src.CustomerName))
                .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src => src.CustomerEmail))
                .ForMember(dest => dest.CustomerPhone, opt => opt.MapFrom(src => src.CustomerPhone))
                .ForMember(dest => dest.RequestedDateUtc, opt => opt.MapFrom(src => src.RequestDate))
                .ForMember(dest => dest.PostalCodeUid, opt => opt.MapFrom(src => src.PostalCodeUid))
                .ForMember(dest => dest.PropertyUid, opt => opt.MapFrom(src => src.PropertyUid))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.RegionUid, opt => opt.MapFrom(src => src.RegionUid))
                .ForMember(dest => dest.RequesterUserUid, opt => opt.MapFrom(src => src.UserUid))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .BaseEntityAfterMap()
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<Features.Recruiment.SaveRecruitment, RecruitmentEntity>()
                .ForMember(dest => dest.Uid, opt => opt.MapFrom(src => src.Uid))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Documents, opt => opt.MapFrom(src => src.Documents))
                .BaseEntityAfterMap()
                .ForAllOtherMembers(opt => opt.Ignore());


            CreateMap<Features.Region.SaveRegion, RegionEntity>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.PathString, opt => opt.MapFrom(src => src.PathString))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .BaseEntityAfterMap()
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<Features.User.SaveUser, IdentityUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.PasswordHash))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<Features.UserRole.SaveUserRole, IdentityUserRole>()
               .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
               .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId))
               .ForAllOtherMembers(opt => opt.Ignore());

            #endregion

            CreateMap<DocumentBaseEntity, Application.DucumentBase>()
                .ForMember(dest => dest.Uid, opt => opt.MapFrom(src => src.Uid))
                .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.FileName))
                .ForMember(dest => dest.Extension, opt => opt.MapFrom(src => src.Extension))
                .ForMember(dest => dest.Body, opt => opt.MapFrom(src => src.Body))
                .ForAllOtherMembers(opt => opt.Ignore());

            //TODO: Map both only base class
            CreateMap<Application.DucumentBase, PropertyDocumentEntity>()
                .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.FileName))
                .ForMember(dest => dest.Extension, opt => opt.MapFrom(src => src.Extension))
                .ForMember(dest => dest.RowNo, opt => opt.MapFrom(src => src.RowNo))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Body, opt => opt.MapFrom(src => src.Body))
                .BaseEntityAfterMap()
                .ForAllOtherMembers(opt => opt.Ignore());

            //TODO: Map both only base class
            CreateMap<Application.DucumentBase, RecruitmentDocumentEntity>()
                .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.FileName))
                .ForMember(dest => dest.Extension, opt => opt.MapFrom(src => src.Extension))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Body, opt => opt.MapFrom(src => src.Body))
                .BaseEntityAfterMap()
                .ForAllOtherMembers(opt => opt.Ignore());


            #region ToApplication
            
            CreateMap<InseptionEntity, Application.Inseption>()
                .ForMember(dest => dest.Uid, opt => opt.MapFrom(src => src.Uid))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.RegionUid, opt => opt.MapFrom(src => src.RegionUid))
                .ForMember(dest => dest.RegionName, opt => opt.MapFrom(src => src.Region.Name))
                .ForMember(dest => dest.PostalCodeUid, opt => opt.MapFrom(src => src.PostalCodeUid))
                .ForMember(dest => dest.PostalCodeName, opt => opt.MapFrom(src => src.PostalCode.Name))
                .ForMember(dest => dest.PropertyUid, opt => opt.MapFrom(src => src.PropertyUid))
                .ForMember(dest => dest.PropertyName, opt => opt.MapFrom(src => src.Property.Name))
                .ForMember(dest => dest.PropertyAddress, opt => opt.MapFrom(src => src.Property.Address))
                .ForMember(dest => dest.ConfirmedAddress, opt => opt.MapFrom(src => src.ConfirmedAddress))
                .ForMember(dest => dest.InseptionDate, opt => opt.MapFrom(src => src.CreatedUtc))
                .ForMember(dest => dest.RequestedDate, opt => opt.MapFrom(src => src.RequestedDateUtc))
                .ForMember(dest => dest.ConfirmedDate, opt => opt.MapFrom(src => src.ConfirmedDateUtc))
                .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src => src.CustomerEmail))
                .ForMember(dest => dest.CustomerFullName, opt => opt.MapFrom(src => src.CustomerFullName))
                .ForMember(dest => dest.CustomerPhone, opt => opt.MapFrom(src => src.CustomerPhone))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.RequesterUserUid, opt => opt.MapFrom(src => src.RequesterUserUid))
                .ForMember(dest => dest.ConfirmerUserUid, opt => opt.MapFrom(src => src.ConfirmerUserUid))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<PhysicalPersonEntity, Application.PhysicalPerson>()
                .ForMember(dest => dest.Uid, opt => opt.MapFrom(src => src.Uid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Skype, opt => opt.MapFrom(src => src.Skype))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.UserUid, opt => opt.MapFrom(src => src.UserUid))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<RegionEntity, Application.Region>()
                .ForMember(dest => dest.Uid, opt => opt.MapFrom(src => src.Uid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Departaments, opt => opt.MapFrom(src => src.Departaments.Where(x=> !x.IsDeleted)))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<RegionEntity, Application.RegionMapData>()
                .ForMember(dest => dest.Uid, opt => opt.MapFrom(src => src.Uid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Departaments, opt => opt.MapFrom(src => src.Departaments.Where(dep => !dep.IsDeleted)))
                .ForMember(dest => dest.PathString, opt => opt.MapFrom(src => src.PathString))
                .ForMember(dest => dest.MapBounds, opt => opt.MapFrom(src => src.MapBounds))
                .ForMember(dest => dest.MapCenter, opt => opt.MapFrom(src => src.MapCenter))
                .ForMember(dest => dest.MapZoom, opt => opt.MapFrom(src => src.MapBounds))
                .ForAllOtherMembers(opt => opt.Ignore());


            CreateMap<DepartamentEntity, Application.Departament>()
                .ForMember(dest => dest.Uid, opt => opt.MapFrom(src => src.Uid))
                .ForMember(dest => dest.RegionUid, opt => opt.MapFrom(src => src.RegionUid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.PostalCodes, opt => opt.MapFrom(src => src.PostalCodes.Where(x => !x.IsDeleted)))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<DepartamentEntity, Application.DepartamentView>()
                .ForMember(dest => dest.Uid, opt => opt.MapFrom(src => src.Uid))
                .ForMember(dest => dest.RegionUid, opt => opt.MapFrom(src => src.RegionUid))
                .ForMember(dest => dest.RegionName, opt => opt.MapFrom(src => src.Region.Name))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<DepartamentEntity, Application.DepartamentMapData>()
                .ForMember(dest => dest.Uid, opt => opt.MapFrom(src => src.Uid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.PostalCodes, opt => opt.MapFrom(src => src.PostalCodes.Where(x=>!x.IsDeleted)))
                .ForMember(dest => dest.MapBounds, opt => opt.MapFrom(src => src.MapBounds))
                .ForMember(dest => dest.MapCenter, opt => opt.MapFrom(src => src.MapCenter))
                .ForMember(dest => dest.MapZoom, opt => opt.MapFrom(src => src.MapBounds))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<PostalCodeEntity, Application.PostalCode>()
                .ForMember(dest => dest.Uid, opt => opt.MapFrom(src => src.Uid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.DepartamentUid, opt => opt.MapFrom(src => src.DepartamentUid))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<PostalCodeEntity, Application.PostalCodeView>()
                .ForMember(dest => dest.Uid, opt => opt.MapFrom(src => src.Uid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.DepartamentName, opt => opt.MapFrom(src => src.Departament.Name))
                .ForMember(dest => dest.DepartamentUid, opt => opt.MapFrom(src => src.DepartamentUid))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<PostalCodeEntity, Application.PostalCodeMapData>()
                .ForMember(dest => dest.Uid, opt => opt.MapFrom(src => src.Uid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.MapBounds, opt => opt.MapFrom(src => src.MapBounds))
                .ForMember(dest => dest.MapCenter, opt => opt.MapFrom(src => src.MapCenter))
                .ForMember(dest => dest.MapZoom, opt => opt.MapFrom(src => src.MapBounds))
                .ForAllOtherMembers(opt => opt.Ignore());


            CreateMap<PropertyEntity, Application.PropertyPreview>()
                .ForMember(dest => dest.Uid, opt => opt.MapFrom(src => src.Uid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode.Name))
                .ForMember(dest => dest.PostalCodeUid, opt => opt.MapFrom(src => src.PostalCodeUid))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Longitude))
                .ForMember(dest => dest.FloorsNumber, opt => opt.MapFrom(src => src.FloorsNumber))
                .ForMember(dest => dest.RoomsCount, opt => opt.MapFrom(src => src.RoomsCount))
                .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Latitude))
                .ForMember(dest => dest.SalePrice, opt => opt.MapFrom(src => src.SalePrice))
                .ForMember(dest => dest.TotalSquare, opt => opt.MapFrom(src => src.TotalSquare))
                .ForMember(dest => dest.DepartamentName, opt => opt.MapFrom(src => src.PostalCode.Departament.Name))
                .ForMember(dest => dest.DepartamentUid, opt => opt.MapFrom(src => src.PostalCode.DepartamentUid))
                .ForMember(dest => dest.RegionUid, opt => opt.MapFrom(src => src.PostalCode.Departament.RegionUid))
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

            CreateMap<IdentityUser, Application.User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.PasswordHash))
                .ForAllOtherMembers(opt => opt.Ignore());
            #endregion

        }
    }
}
