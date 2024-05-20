using AutoMapper;
using Domain.Dtos.ClassScheduleDto;
using Domain.Dtos.MembershipDto;
using Domain.Dtos.PaymentDto;
using Domain.Dtos.TrainerDto;
using Domain.Dtos.UserDto;
using Domain.Dtos.WorkOut;
using Domain.Entities;

namespace Infrastructure.AutoMapper;

public class MapperProfile:Profile
{
    public MapperProfile()
    {
        CreateMap<User, AddUserDto>().ReverseMap();
        CreateMap<User, GetUserDto>().ReverseMap();
        CreateMap<User, UpdateUserDto>().ReverseMap();
        
        
        CreateMap<Payment, AddPaymentDto>().ReverseMap();
        CreateMap<Payment, GetPaymentDto>().ReverseMap();
        CreateMap<Payment, UpdatePaymentDto>().ReverseMap();
        
        CreateMap<ClassSchedule, AddClassScheduleDto>().ReverseMap();
        CreateMap<ClassSchedule, GetClassScheduleDto>().ReverseMap();
        CreateMap<ClassSchedule, UpdateClassScheduleDto>().ReverseMap();
        
        CreateMap<Membership, AddMembershipDto>().ReverseMap();
        CreateMap<Membership, GetMembershipDto>().ReverseMap();
        CreateMap<Membership, UpdateMembershipDto>().ReverseMap();
        
        CreateMap<Trainer, AddTrainerDto>().ReverseMap();
        CreateMap<Trainer, GetTrainerDto>().ReverseMap();
        CreateMap<Trainer, UpdateTrainerDto>().ReverseMap();
        
        CreateMap<WorkOut, AddWorkOutDto>().ReverseMap();
        CreateMap<WorkOut, GetWorkOutDto>().ReverseMap();
        CreateMap<WorkOut, UpdateWorkOutDto>().ReverseMap();


    }
}