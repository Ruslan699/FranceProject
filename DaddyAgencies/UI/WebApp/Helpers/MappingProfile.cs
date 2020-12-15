using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Identity = DaddyAgencies.Common.EntityFramework.Identity;
using Application = DaddyAgencies.Application.Models;
using Features = DaddyAgencies.Application.Features;

namespace WebApp.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Models.RecruitmentModel, Features.Recruiment.SaveRecruitment>()
                .ForMember(dest => dest.Uid, opt => opt.MapFrom(src => src.Uid))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message))
                .ForMember(dest => dest.Documents, opt => opt.MapFrom(src => src.Documents))
                .ForAllOtherMembers(opt => opt.Ignore());

            //CreateMap<Models.RegionModel, Features.Region.SaveRegion>()
            //    .ForMember(dest => dest.Uid, opt => opt.MapFrom(src => src.Uid))
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            //    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            //    .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<Application.DucumentBase, Models.ImageDocument>()
                .ForMember(dest => dest.Uid, opt => opt.MapFrom(src => src.Uid))
                .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.FileName))
                .ForMember(dest => dest.Extension, opt => opt.MapFrom(src => src.Extension))
                .ForMember(dest => dest.RowNo, opt => opt.MapFrom(src => src.RowNo))
                .ForMember(dest => dest.AsBase64, opt => opt.MapFrom(src => Convert.ToBase64String(src.Body)))
                .ForAllOtherMembers(opt => opt.Ignore());


            //CreateMap<Models.PropertyDraftModel, Features.Property.SaveProperty>()
            //    .ForMember(dest => dest.Uid, opt => opt.MapFrom(src => src.Uid))
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Caption))
            //    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            //    .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Longitude))
            //    .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Latitude))
            //    .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
            //    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            //    .ForMember(dest => dest.SalePrice, opt => opt.MapFrom(src => src.SalePrice))
            //    .ForMember(dest => dest.TotalSquare, opt => opt.MapFrom(src => src.TotalSquare))
            //    .ForMember(dest => dest.Files, opt => opt.MapFrom(src => src.Images))
            //    .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<HttpPostedFileBase, Application.DucumentBase>()
                .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.FileName))
                .ForMember(dest => dest.Body, opt => opt.MapFrom(src => src.InputStream.ToByteArray()))
                .ForMember(dest => dest.Extension, opt => opt.MapFrom(src => GetFileExtension(src.FileName)))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<Application.PhysicalPerson, Models.PhysicalPersonModel>()
                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => string.Join(" ", src.Surname, src.Name, src.LastName)))
                .ForMember(dest => dest.Uid, opt => opt.MapFrom(src => src.Uid))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Skype, opt => opt.MapFrom(src => src.Skype))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForAllOtherMembers(opt => opt.Ignore());


            CreateMap<Models.RegisterViewModel, Identity.IdentityUser>()
                //.ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Fullname))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Fullname))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<Models.InseptionModel, Features.Inseption.SaveInseption>()
                .ForMember(dest => dest.CustomerPhone, opt => opt.MapFrom(src => src.CustomerPhone))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (int)Application.InseptionStatus.Nouveau))
                .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src => src.CustomerEmail))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerName))
                .ForMember(dest => dest.PostalCodeUid, opt => opt.MapFrom(src => src.PostalCodeUid))
                .ForMember(dest => dest.PropertyUid, opt => opt.MapFrom(src => src.PropertyUid))
                .ForMember(dest => dest.RegionUid, opt => opt.MapFrom(src => src.RegionUid))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.RequestDate, opt => opt.MapFrom(src => new DateTime(src.Date.Value.Year, src.Date.Value.Month, src.Date.Value.Day, src.Time.Value, 0, 0)))
                .ForAllOtherMembers(opt => opt.Ignore());


            CreateMap<Models.PageableFilter, Application.PropertyFilter>()
                .ForMember(dest => dest.Regions, opt => opt.MapFrom(src => src.SelectedRegions))
                .ForMember(dest => dest.Departaments, opt => opt.MapFrom(src => src.SelectedDepartaments))
                .ForMember(dest => dest.PostalCodes, opt => opt.MapFrom(src => src.SelectedPostalCodes))
                .ForMember(dest => dest.PropertyTypes, opt => opt.MapFrom(src => src.SelectedTypes))
                .ForMember(dest => dest.Keyword, opt => opt.MapFrom(src => src.Keyword))
                .ForMember(dest => dest.PriceMin, opt => opt.MapFrom(src => src.PriceMin))
                .ForMember(dest => dest.PriceMax, opt => opt.MapFrom(src => src.PriceMax))
                .ForMember(dest => dest.SquareMin, opt => opt.MapFrom(src => src.SquareMin))
                .ForMember(dest => dest.SquareMax, opt => opt.MapFrom(src => src.SquareMax))
                .ForMember(dest => dest.RoomsMin, opt => opt.MapFrom(src => src.RoomsMin))
                .ForMember(dest => dest.RoomsMax, opt => opt.MapFrom(src => src.RoomsMax))
                .ForMember(dest => dest.Parking, opt => opt.MapFrom(src => src.Parking))
                .ForMember(dest => dest.EnergyClass, opt => opt.MapFrom(src => src.EnergyClass))
                .ForMember(dest => dest.Ges, opt => opt.MapFrom(src => src.Ges))
                .ForMember(dest => dest.Skip, opt => opt.MapFrom(src => src.Skip))
                .ForMember(dest => dest.Take, opt => opt.MapFrom(src => src.Take))
                .ForMember(dest => dest.Sortby, opt => opt.MapFrom(src => src.Sortby))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<Models.PageableFilter, Features.Property.GetPropertiesByFilter>()
                .ForMember(dest => dest.Filter, opt => opt.MapFrom(src => src))
                .ForAllOtherMembers(opt => opt.Ignore());
            CreateMap<Models.PageableFilter, Features.Property.GetPropertyCountByFilter>()
                .ForMember(dest => dest.Filter, opt => opt.MapFrom(src => src))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<Application.Region, SelectListItem>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Uid))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Selected, opt => opt.MapFrom(src => true))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<IEnumerable<Application.PropertyPreview>, Models.PropertyFilter>()
                .ForMember(dest => dest.PriceMax, opt => opt.MapFrom(src => (int)Math.Round(src.Max(p => p.SalePrice))))
                .ForMember(dest => dest.PriceMin, opt => opt.MapFrom(src => (int)Math.Round(src.Min(p => p.SalePrice))))
                .ForAllOtherMembers(opt => opt.Ignore());
            
            CreateMap<IEnumerable<Application.Region>, Models.PropertyFilter>()
                .ForMember(dest => dest.AllRegions, opt => opt.MapFrom(src => src))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<Application.Region, Models.RegionModel>()
                .ForMember(dest => dest.Uid, opt => opt.MapFrom(src => src.Uid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<Application.PropertyPreview, Models.PropertyModel>()
                .ForMember(dest => dest.Uid, opt => opt.MapFrom(src => src.Uid))
                .ForMember(dest => dest.RegionUid, opt => opt.MapFrom(src => src.RegionUid))
                .ForMember(dest => dest.Caption, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.SalePrice, opt => opt.MapFrom(src => src.SalePrice))
                .ForMember(dest => dest.TotalSquare, opt => opt.MapFrom(src => src.TotalSquare))
                .ForMember(dest => dest.RoomsCount, opt => opt.MapFrom(src => src.RoomsCount))
                .ForMember(dest => dest.PropertyType, opt => opt.MapFrom(src => src.PropertyType))
                .ForMember(dest => dest.EnergyClass, opt => opt.MapFrom(src => src.EnergyClass))
                .ForMember(dest => dest.Ges, opt => opt.MapFrom(src => src.Ges))
                .ForMember(dest => dest.Parking, opt => opt.MapFrom(src => src.Parking))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
        private static string GetFileExtension(string fileName)
        {
            var splitted = fileName.Split('.');
            return splitted.Last();
        }

    }
}