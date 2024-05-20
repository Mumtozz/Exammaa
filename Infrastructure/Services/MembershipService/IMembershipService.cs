using Domain.Dtos.MembershipDto;
using Domain.Filters;
using Domain.Response;

namespace Infrastructure.Services.MembershipService;

public interface IMembershipService
{
    Task<PagedResponse<List<GetMembershipDto>>> GetMembership(MembershipFilter filter);
    Task<Response<string>> AddMembership(AddMembershipDto addMembershipDto);
    Task<Response<string>> UpdateMembership(UpdateMembershipDto updateMembershipDto);
    Task<Response<bool>> DeleteMembership(int id);
    Task<Response<GetMembershipDto>> GetMembershipById(int id);
}
