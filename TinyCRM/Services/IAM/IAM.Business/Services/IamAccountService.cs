using System.Linq.Dynamic.Core;
using AutoMapper;
using BuildingBlock.Application.DTOs;
using BuildingBlock.Application.Identity;
using BuildingBlock.Domain.Exceptions;
using BuildingBlock.Domain.Interfaces;
using IAM.Business.Models.Users;
using IAM.Business.Models.Users.Dto;
using IAM.Business.Services.IServices;
using IAM.Domain.Entities.Roles;
using IAM.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IAM.Business.Services;

public class IamAccountService : IIamAccountService
{
    private readonly IMapper _mapper;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ICurrentUser _currentUser;

    public IamAccountService(UserManager<ApplicationUser> userManager, IMapper mapper,
        RoleManager<ApplicationRole> roleManager, IUnitOfWork unitOfWork, ICurrentUser currentUser)
    {
        _userManager = userManager;
        _mapper = mapper;
        _roleManager = roleManager;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
    }

    public async Task<FilterAndPagingResultDto<UserSummaryDto>> FilterAndPagingUsersAsync(
        FilterAndPagingUsersDto filterAndPagingUsersDto)
    {
        var query = _userManager.Users.Where(x => x.Name.ToUpper().Contains(filterAndPagingUsersDto.Keyword.ToUpper())
                                                  || x.Email!.ToUpper()
                                                      .Contains(filterAndPagingUsersDto.Keyword.ToUpper()));
        var totalCount = await query.CountAsync();
        query = string.IsNullOrEmpty(filterAndPagingUsersDto.ConvertSort())
            ? query.OrderBy("CreatedDate")
            : query.OrderBy(filterAndPagingUsersDto.ConvertSort());

        var users = await query.Skip(filterAndPagingUsersDto.Skip)
            .Take(filterAndPagingUsersDto.Take).ToListAsync();

        return new FilterAndPagingResultDto<UserSummaryDto>(_mapper.Map<List<UserSummaryDto>>(users),
            filterAndPagingUsersDto.Skip,
            filterAndPagingUsersDto.Take,
            totalCount);
    }

    // public async Task<UserDetailDto> CreateUserAsync(UserCreateDto userCreateDto)
    // {
    //     if (!await _roleManager.RoleExistsAsync(Role.User))
    //         await _roleManager.CreateAsync(new ApplicationRole(Role.User));
    //
    //     var user = _mapper.Map<ApplicationUser>(userCreateDto);
    //     _unitOfWork.BeginTransaction();
    //     try
    //     {
    //         var result = await _userManager.CreateAsync(user, userCreateDto.Password);
    //         if (!result.Succeeded)
    //             throw new InvalidUpdateException(result.Errors.First().Description);
    //         await _userManager.AddToRoleAsync(user, Role.User);
    //         _unitOfWork.Commit();
    //     }
    //     catch
    //     {
    //         _unitOfWork.Rollback();
    //         throw;
    //     }
    //
    //     return _mapper.Map<UserDetailDto>(user);
    // }

    public async Task<UserDetailDto> CreateUserAsync(UserCreateDto userCreateDto)
    {
        if (!await _roleManager.RoleExistsAsync(Role.Admin))
            await _roleManager.CreateAsync(new ApplicationRole(Role.Admin));

        var user = _mapper.Map<ApplicationUser>(userCreateDto);
        _unitOfWork.BeginTransaction();
        try
        {
            var result = await _userManager.CreateAsync(user, userCreateDto.Password);
            if (!result.Succeeded)
                throw new InvalidUpdateException(result.Errors.First().Description);
            await _userManager.AddToRoleAsync(user, Role.Admin);
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
        var user = await FindUserAsync(id);
        return _mapper.Map<UserDetailDto>(user);
    }

    public async Task ChangePasswordAsync(Guid id, UserChangePasswordDto userChangePasswordDto)
    {
        var user = await FindUserAsync(id);
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var result = await _userManager.ResetPasswordAsync(user, token, userChangePasswordDto.Password);
        if (!result.Succeeded)
            throw new InvalidUpdateException(result.Errors.First().Description);
    }

    public async Task<UserDetailDto> UpdateUserAsync(Guid id, UserEditDto userEditDto)
    {
        var user = await FindUserAsync(id);
        _mapper.Map(userEditDto, user);
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
            throw new InvalidUpdateException(result.Errors.First().Description);
        return _mapper.Map<UserDetailDto>(user);
    }

    public async Task DeleteManyUsersAsync(DeleteManyUsersDto deleteManyUsersDto)
    {
        foreach (var id in deleteManyUsersDto.Ids)
        {
            if (_currentUser.Id == id.ToString())
            {
                continue;
            }

            var user = await FindUserAsync(id);
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                throw new InvalidUpdateException(result.Errors.First().Description);
        }
    }

    public async Task<IEnumerable<UserSummaryDto>> FilterUsersAsync(FilterUsersDto dto)
    {
        var users = await _userManager.Users
            .Where(u => u.Name.ToUpper().Contains(dto.Keyword.ToUpper()))
            .ToListAsync();
        return _mapper.Map<IEnumerable<UserSummaryDto>>(users);
    }

    public async Task DeleteFilteredUsersAsync(FilterUsersDto dto)
    {
        var users = await _userManager.Users
            .Where(u => u.Name.ToUpper().Contains(dto.Keyword.ToUpper()))
            .ToListAsync();
        foreach (var user in users.Where(user => _currentUser.Id != user.Id.ToString()))
        {
            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
                throw new InvalidUpdateException(result.Errors.First().Description);
        }
    }

    private async Task<ApplicationUser> FindUserAsync(Guid id)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id.Equals(id.ToString()))
                   ?? throw new EntityNotFoundException($"User with Id[{id}] not found");
        return user;
    }
}