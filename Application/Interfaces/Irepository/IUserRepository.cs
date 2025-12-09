using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Irepository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByCodeAsync(string code);
    }
}
