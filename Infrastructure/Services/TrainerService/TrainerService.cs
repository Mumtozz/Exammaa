using System.Net;
using AutoMapper;
using Domain;
using Domain.Dtos.TrainerDto;
using Domain.Entities;
using Domain.Filters;
using Domain.Response;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.TrainerService;

public class TrainerService : ITrainerService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public TrainerService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<Response<string>> AddTrainer(AddTrainerDto addTrainerDto)
    {
        try
        {
            var mapped = _mapper.Map<Trainer>(addTrainerDto);
            await _context.Trainers.AddAsync(mapped);
            await _context.SaveChangesAsync();
            return new Response<string>("Trainer added successfully");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<PagedResponse<List<GetTrainerDto>>> GetTrainer(TrainerFilter filter)
    {
        try
        {
            var Trainers = _context.Trainers.AsQueryable();
            if (!string.IsNullOrEmpty(filter.Name))
                Trainers = Trainers.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));

            if (!string.IsNullOrEmpty(filter.Email))
                Trainers = Trainers.Where(x => x.Email.ToLower().Contains(filter.Email.ToLower()));

            var Trainer = await Trainers.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
            var total = await Trainers.CountAsync();

            var response = _mapper.Map<List<GetTrainerDto>>(Trainers);
            return new PagedResponse<List<GetTrainerDto>>(response, total, filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetTrainerDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteTrainer(int id)
    {
        try
        {
            var existing = await _context.Trainers.Where(e => e.Id == id).ExecuteDeleteAsync();
            if (existing == 0) return new Response<bool>(HttpStatusCode.BadRequest, "Trainer not found!");
            return new Response<bool>(true);
        }
        catch (Exception ex)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }


    public async Task<Response<string>> UpdateTrainer(UpdateTrainerDto updateTrainerDto)
    {
        try
        {
            var existing = await _context.Trainers.AnyAsync(e => e.Id == updateTrainerDto.Id);
            if (!existing) return new Response<string>(HttpStatusCode.BadRequest, "Trainer not found!");
            var mapped = _mapper.Map<Trainer>(updateTrainerDto);
            _context.Trainers.Update(mapped);

            await _context.SaveChangesAsync();
            return new Response<string>("Updated successfully");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<GetTrainerDto>> GetTrainerById(int id)
    {
        try
        {
            var existing = await _context.Trainers.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null) return new Response<GetTrainerDto>(HttpStatusCode.BadRequest, "Trainer not found");
            var Trainer = _mapper.Map<GetTrainerDto>(existing);
            return new Response<GetTrainerDto>(Trainer);
        }
        catch (Exception e)
        {
            return new Response<GetTrainerDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}
