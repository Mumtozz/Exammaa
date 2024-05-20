using System.Net;
using AutoMapper;
using Domain;
using Domain.Dtos.UserDto;
using Domain.Entities;
using Domain.Filters;
using Domain.Response;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.UserService;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public UserService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<Response<string>> AddUser(AddUserDto addUserDto)
    {
        try
        {
            var mapped = _mapper.Map<User>(addUserDto);
            await _context.Users.AddAsync(mapped);
            await _context.SaveChangesAsync();
            return new Response<string>("User added successfully");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<PagedResponse<List<GetUserDto>>> GetUser(UserFilter filter)
    {
        try
        {
            var Users = _context.Users.AsQueryable();
            if (!string.IsNullOrEmpty(filter.Name))
                Users = Users.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));

            if (!string.IsNullOrEmpty(filter.Email))
                Users = Users.Where(x => x.Email.ToLower().Contains(filter.Email.ToLower()));

            var User = await Users.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
            var total = await Users.CountAsync();

            var response = _mapper.Map<List<GetUserDto>>(Users);
            return new PagedResponse<List<GetUserDto>>(response, total, filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetUserDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteUser(int id)
    {
        try
        {
            var existing = await _context.Users.Where(e => e.Id == id).ExecuteDeleteAsync();
            if (existing == 0) return new Response<bool>(HttpStatusCode.BadRequest, "User not found!");
            return new Response<bool>(true);
        }
        catch (Exception ex)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }


    public async Task<Response<string>> UpdateUser(UpdateUserDto updateUserDto)
    {
        try
        {
            var existing = await _context.Users.AnyAsync(e => e.Id == updateUserDto.Id);
            if (!existing) return new Response<string>(HttpStatusCode.BadRequest, "User not found!");
            var mapped = _mapper.Map<User>(updateUserDto);
            _context.Users.Update(mapped);

            await _context.SaveChangesAsync();
            return new Response<string>("Updated successfully");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<GetUserDto>> GetUserById(int id)
    {
        try
        {
            var existing = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null) return new Response<GetUserDto>(HttpStatusCode.BadRequest, "User Not  Found");
            var User = _mapper.Map<GetUserDto>(existing);
            return new Response<GetUserDto>(User);
        }
        catch (Exception e)
        {
            return new Response<GetUserDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}
