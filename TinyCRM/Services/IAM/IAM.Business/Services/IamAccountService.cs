using BuildingBlock.Application.DTOs;
using IAM.Business.Models.Users;
using IAM.Business.Models.Users.Dto;
using IAM.Business.Services.IServices;
using IAM.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using System.Linq.Dynamic.Core;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace IAM.Business.Services;

public class IamAccountService : IIamAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public IamAccountService(UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<FilterAndPagingResultDto<UserSummaryDto>> FilterAndPagingUsersAsync(
        FilterAndPagingUsersDto filterAndPagingUsersDto)
    {
        var query = _userManager.Users.Where(x => x.Name.Contains(filterAndPagingUsersDto.Keyword)
                                                  || x.Email!.Contains(filterAndPagingUsersDto.Keyword));

        query = string.IsNullOrEmpty(filterAndPagingUsersDto.ConvertSort())
            ? query.OrderBy("CreatedDate")
            : query.OrderBy(filterAndPagingUsersDto.ConvertSort());

        var users = await query.Skip(filterAndPagingUsersDto.PageSize * (filterAndPagingUsersDto.PageIndex - 1))
            .Take(filterAndPagingUsersDto.PageSize).ToListAsync();

        return new FilterAndPagingResultDto<UserSummaryDto>(_mapper.Map<List<UserSummaryDto>>(users),
            filterAndPagingUsersDto.PageIndex,
            filterAndPagingUsersDto.PageSize,
            users.Count);
    }
}