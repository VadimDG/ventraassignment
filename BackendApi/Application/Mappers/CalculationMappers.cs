using Application.Dtos;
using Application.Models;
using AutoMapper;

namespace Application.Mappers
{
    public class CalculationMappers : Profile
    {
        public CalculationMappers() 
        {
            CreateMap<CalculationModel, CalculationDto>();
            CreateMap<SubSkuItemModel, SubSkuItemDto>();
            CreateMap<SkuModel, SkuDto>();
            CreateMap<SubSkuItemModel, SubSkuItemDto>();
        }
    }
}