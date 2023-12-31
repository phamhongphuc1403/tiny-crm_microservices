using AutoMapper;
using People.Application.DTOs.ContactDTOs;
using People.Domain.ContactAggregate.Entities;

namespace People.Infrastructure.EFCore.Profiles;

public class ContactMapper : Profile
{
    public ContactMapper()
    {
        CreateMap<Contact, ContactSummaryDto>();
        CreateMap<Contact, ContactDetailDto>()
            .ForMember(dest => dest.AccountName, opt => opt.MapFrom(src => src.Account.Name));
    }
}