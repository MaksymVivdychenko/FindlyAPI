using FindlyDAL.DB;
using FindlyDAL.Entities;
using FindlyDAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FindlyDAL.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(FindlyDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> IsLoginNotUnique(string login)
    {
        return await DbContext.Users.AnyAsync(q => q.Login == login);
    }
}