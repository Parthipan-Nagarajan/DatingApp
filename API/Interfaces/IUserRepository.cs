using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        void UpdateAsync(AppUser user);
        Task<bool> SaveChangesAsync();

        Task<IEnumerable<AppUser>> GetAppUsersAsync();

        Task<AppUser> GetUserByIdAsync(int id);

        Task<AppUser> GetUserByUserNameAsync(string username);     

        Task<IEnumerable<MemberDto>> GetMembersAsync();
        Task<MemberDto> GetMemberAsync(string username);

    }
}