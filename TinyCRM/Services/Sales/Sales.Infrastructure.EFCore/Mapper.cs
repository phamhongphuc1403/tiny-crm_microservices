using AutoMapper;
using Sales.Application.DTOs;
using Sales.Domain.Entities;

namespace Sales.Infrastructure.EFCore;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<Lead, LeadDto>();
    }
}