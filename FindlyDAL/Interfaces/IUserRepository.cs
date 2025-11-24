using System.Linq.Expressions;
using FindlyDAL.Entities;

namespace FindlyDAL.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<bool> IsLoginNotUnique(string login);
}