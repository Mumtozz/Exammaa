using System.Net;
using AutoMapper;
using Domain;
using Domain.Dtos.ClassScheduleDto;
using Domain.Entities;
using Domain.Filters;
using Domain.Response;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.ClassScheduleService;

public class ClassScheduleService : IClassScheduleService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public ClassScheduleService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<Response<string>> AddClassSchedule(AddClassScheduleDto addClassScheduleDto)
    {
        try
        {
            var mapped = _mapper.Map<ClassSchedule>(addClassScheduleDto);
            await _context.ClassSchedules.AddAsync(mapped);
            await _context.SaveChangesAsync();
            return new Response<string>("ClassSchedule added successfully");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<PagedResponse<List<GetClassScheduleDto>>> GetClassSchedule(ClassScheduleFilter filter)
    {
        try
        {
            var ClassSchedules = _context.ClassSchedules.AsQueryable();
           
            var ClassSchedule = await ClassSchedules.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
            var total = await ClassSchedules.CountAsync();

            var response = _mapper.Map<List<GetClassScheduleDto>>(ClassSchedules);
            return new PagedResponse<List<GetClassScheduleDto>>(response, total, filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetClassScheduleDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteClassSchedule(int id)
    {
        try
        {
            var existing = await _context.ClassSchedules.Where(e => e.Id == id).ExecuteDeleteAsync();
            if (existing == 0) return new Response<bool>(HttpStatusCode.BadRequest, "ClassSchedule not found!");
            return new Response<bool>(true);
        }
        catch (Exception ex)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }


    public async Task<Response<string>> UpdateClassSchedule(UpdateClassScheduleDto updateClassScheduleDto)
    {
        try
        {
            var existing = await _context.ClassSchedules.AnyAsync(e => e.Id == updateClassScheduleDto.Id);
            if (!existing) return new Response<string>(HttpStatusCode.BadRequest, "ClassSchedule not found!");
            var mapped = _mapper.Map<ClassSchedule>(updateClassScheduleDto);
            _context.ClassSchedules.Update(mapped);

            await _context.SaveChangesAsync();
            return new Response<string>("Updated successfully");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<GetClassScheduleDto>> GetClassScheduleById(int id)
    {
        try
        {
            var existing = await _context.ClassSchedules.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null) return new Response<GetClassScheduleDto>(HttpStatusCode.BadRequest, "ClassSchedule not found");
            var ClassSchedule = _mapper.Map<GetClassScheduleDto>(existing);
            return new Response<GetClassScheduleDto>(ClassSchedule);
        }
        catch (Exception e)
        {
            return new Response<GetClassScheduleDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}
