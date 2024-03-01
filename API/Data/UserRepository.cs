using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private DataContext _context;
        private readonly IMapper _mapper;
        public UserRepository(DataContext dbContext,IMapper mapper)
        {
            _mapper = mapper;
            _context = dbContext;
        }

        public async Task<IEnumerable<AppUser>> GetAppUsersAsync()
        {
            return await _context.AppUsers.Include(p => p.Photos).ToListAsync();
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
           return await _context.AppUsers.Where(a => a.UserName == username)
           .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
           .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            return await _context.AppUsers.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.AppUsers.FindAsync(id);
        }

        public async Task<AppUser> GetUserByUserNameAsync(string username)
        {
            return await _context.AppUsers.Include(p => p.Photos).SingleOrDefaultAsync(a => a.UserName == username);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async void UpdateAsync(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}