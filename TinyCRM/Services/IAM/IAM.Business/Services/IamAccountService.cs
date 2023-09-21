using BuildingBlock.Application.DTOs;
using IAM.Business.Models.Users;
using IAM.Business.Models.Users.Dto;
using IAM.Business.Services.IServices;
using IAM.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using System.Linq.Dynamic.Core;
using AutoMapper;
using BuildingBlock.Domain.Exceptions;
using BuildingBlock.Domain.Interfaces;
using IAM.Domain.Entities.Roles;
using Microsoft.EntityFrameworkCore;

namespace IAM.Business.Services;

public class IamAccountService : IIamAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly  IUnitOfWork _unitOfWork;
    public IamAccountService(UserManager<ApplicationUser> userManager, IMapper mapper, RoleManager<ApplicationRole> roleManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _mapper = mapper;
        _roleManager = roleManager;
        _unitOfWork = unitOfWork;
    }

    public async Task<FilterAndPagingResultDto<UserSummaryDto>> FilterAndPagingUsersAsync(
        FilterAndPagingUsersDto filterAndPagingUsersDto)
    {
        var query = _userManager.Users.Where(x => x.Name.Contains(filterAndPagingUsersDto.Keyword)
                                                  || x.Email!.Contains(filterAndPagingUsersDto.Keyword));
        var totalCount = query.CountAsync();        
        query = string.IsNullOrEmpty(filterAndPagingUsersDto.ConvertSort())
            ? query.OrderBy("CreatedDate")
            : query.OrderBy(filterAndPagingUsersDto.ConvertSort());
        
        var users = await query.Skip(filterAndPagingUsersDto.PageSize * (filterAndPagingUsersDto.PageIndex - 1))
            .Take(filterAndPagingUsersDto.PageSize).ToListAsync();

        return new FilterAndPagingResultDto<UserSummaryDto>(_mapper.Map<List<UserSummaryDto>>(users),
            filterAndPagingUsersDto.PageIndex,
            filterAndPagingUsersDto.PageSize,
            await totalCount);
    }

    public async Task<UserDetailDto> CreateUserAsync(UserCreateDto userCreateDto)
    {
        if(!await _roleManager.RoleExistsAsync(Role.User))
            await _roleManager.CreateAsync(new ApplicationRole(Role.User));
        
        var user = _mapper.Map<ApplicationUser>(userCreateDto);
        _unitOfWork.BeginTransaction();
        try
        {
            var result = await _userManager.CreateAsync(user, userCreateDto.Password);
            if (!result.Succeeded)
                throw new InvalidUpdateException(result.Errors.First().Description);
            await _userManager.AddToRoleAsync(user, Role.User);
            _unitOfWork.Commit();
        }
        catch
        {
            _unitOfWork.Rollback();
            throw;
        }
        return _mapper.Map<UserDetailDto>(user);
    }

    public async Task<UserDetailDto> GetDetailUserAsync(Guid id)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id.Equals(id.ToString()))
                   ?? throw new EntityNotFoundException($"User with Id[{id}] not found");
        return _mapper.Map<UserDetailDto>(user);
    }

    public async Task ChangePasswordAsync(Guid id, UserChangePasswordDto userChangePasswordDto)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id.Equals(id.ToString()))
                   ?? throw new EntityNotFoundException($"User with Id[{id}] not found");
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var result = await _userManager.ResetPasswordAsync(user, token, userChangePasswordDto.Password);
        if (!result.Succeeded)
            throw new InvalidUpdateException(result.Errors.First().Description);
    }

    public async Task<UserDetailDto> UpdateUserAsync(Guid id, UserEditDto userEditDto)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id.Equals(id.ToString()))
                   ?? throw new EntityNotFoundException($"User with Id[{id}] not found");
        _mapper.Map(userEditDto, user);
        var result = await _userManager.UpdateAsync(user);
        if(!result.Succeeded)
            throw new InvalidUpdateException(result.Errors.First().Description);
        return _mapper.Map<UserDetailDto>(user);
    }
}