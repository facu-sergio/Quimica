using AutoMapper;
using FormulasApi.DTOS;
using Quimica.Core.Models;
using Quimica.Service.DTOS;

namespace FormulasApi.Helpers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Shipment, ShipmentDto>()
                .ForMember(
                dest => dest.Location,
                opt => opt.MapFrom(src => src.Address.Location.Name))
                .ForMember(
                dest => dest.Street,
                opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.Number,
                opt => opt.MapFrom(src => src.Address.Number))
                .ReverseMap();
            CreateMap<ProductOfShipment, ProductShipmentDto>().ReverseMap();

            CreateMap<ShipmentCreateDto, Shipment>()
                .ForPath(dest => dest.Address.Location.Id, opt => opt.MapFrom(src => src.LocationId))
                .ForPath(dest => dest.Address.Street, opt => opt.MapFrom(src => src.Street))
                .ForPath(dest => dest.Address.Number, opt => opt.MapFrom(src => src.Number));

            CreateMap<ShipmentUpdateDTO, Shipment>()
                .ForPath(dest => dest.Address.Location.Id, opt => opt.MapFrom(src => src.LocationId))
                .ForPath(dest => dest.Address.Street, opt => opt.MapFrom(src => src.Street))
                .ForPath(dest => dest.Address.Number, opt => opt.MapFrom(src => src.Number))
                .ForPath(dest => dest.Address.Id, opt => opt.MapFrom(src => src.AddressId));

        }
        
    }
}
