using Application.DTOs;
using Share.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Iservice
{
    public interface IUserService
    {
        Task<IEnumerable<ReadUserDTO>> GetAllAsync();
        Task<ReadUserDTO?> GetByIdAsync(Guid id);
        Task<ReadUserDTO> CreateAsync(CreateUserDTO dto);
        Task<ReadUserDTO?> UpdateAsync(Guid id, UpdateUserDTO dto);
        Task<bool> DeleteAsync(Guid id);
        Task<PageResult<ReadUserDTO>> GetPagedAsync(
                    int pageIndex,
                    int pageSize,
                    string? search,
                    string? sortBy,
                    bool desc);
    }
}
