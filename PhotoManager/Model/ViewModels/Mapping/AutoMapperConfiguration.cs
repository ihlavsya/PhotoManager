using System;
using System.Globalization;
using AutoMapper;
using PhotoManager.DAL.Entities;

namespace PhotoManager.Model.ViewModels.Mapping
{
    public class AutoMapperConfiguration
    {
        public static IMapper MapperInstance =>

            new MapperConfiguration(cfg =>
      {
          //Map Model to ViewModel


          cfg.CreateMap<Album, AlbumViewModel>()
              .ForAllMembers(opt => opt.Condition(srs => srs!=null));

          cfg.CreateMap<Photo, PhotoViewModel>()
              .ForMember(dest => dest.FilePath, opt => opt.MapFrom(src => src.Name))
              .ForMember(dest => dest.Diaphragm, opt => opt.ResolveUsing(src=>
              {
                  if (src.Diaphragm != 0)
                  {
                      return "f/" + src.Diaphragm.ToString();
                  }

                  return "diaphragm";
              }))
              .ForMember(dest => dest.ShutterSpeed, opt => opt.ResolveUsing(src => new ShutterSpeedValues(src.ShutterSpeedNumerator,src.ShutterSpeedDenominator)))
              .ForAllMembers(opt => opt.Condition(srs => srs != null));

          //Map ViewModel to Model
          cfg.CreateMap<RegisterViewModel, User>()
              .ForAllMembers(opt => opt.Condition(srs => srs != null));

          cfg.CreateMap<AlbumViewModel, Album>()
              .ForMember(dest=>dest.Username,opt=>opt.ResolveUsing(src =>
              {
                  if (src.Username == null)
                  {
                      return String.Empty;
                  }

                  return src.Username;
              }))
              .ForAllMembers(opt => opt.Condition(srs => srs != null));

          cfg.CreateMap<PhotoViewModel, Photo>()
              .ForMember(dest=>dest.ShutterSpeedNumerator,opt=>opt.ResolveUsing(src=>
              {
                  if (src.ShutterSpeed.Numerator == 0)
                  {
                      return 1;
                  }
                  return src.ShutterSpeed.Numerator;
              }))
              .ForMember(dest => dest.ShutterSpeedDenominator, opt => opt.ResolveUsing(src =>
              {
                  if (src.ShutterSpeed.Denominator == 0)
                  {
                      return 1;
                  }
                  return src.ShutterSpeed.Denominator;
              }))
              .ForMember(dest => dest.Description, opt => opt.ResolveUsing(src =>
              {
                  if (src.Description == null)
                  {
                      return String.Empty;
                  }

                  return src.Description;
              }))
              .ForMember(dest => dest.Name, opt => opt.ResolveUsing(src =>
              {
                  if (src.Name == null)
                  {
                      return String.Empty;
                  }

                  return src.Name;
              }))
              .ForMember(dest => dest.PhotoTakingPlace, opt => opt.ResolveUsing(src =>
              {
                  if (src.PhotoTakingPlace == null)
                  {
                      return String.Empty;
                  }

                  return src.PhotoTakingPlace;
              }))
              .ForMember(dest => dest.CameraModel, opt => opt.ResolveUsing(src =>
              {
                  if (src.CameraModel == null)
                  {
                      return String.Empty;
                  }

                  return src.CameraModel;
              }))
              .ForMember(dest => dest.OriginalName, opt => opt.ResolveUsing(src =>
              {
                  if (src.OriginalName == null)
                  {
                      return String.Empty;
                  }

                  return src.OriginalName;
              }))
              .ForMember(dest=>dest.Diaphragm, opt=>opt.ResolveUsing(src =>
              {
                  if (src.Diaphragm != null && src.Diaphragm!="diaphragm")
                  {
                      return float.Parse(src.Diaphragm.Remove(0, 2), CultureInfo.InvariantCulture.NumberFormat);
                  }

                  return 0;
              }))
              .ForAllMembers(opt => opt.Condition(srs => srs!=null));


      }).CreateMapper();
    }
}