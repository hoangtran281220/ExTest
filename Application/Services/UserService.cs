using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;
using Share.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;

        public UserService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<ReadUserDTO>> GetAllAsync()
        {
            var users = await _uow.Users.GetAllAsync();

            return users.Select(u => new ReadUserDTO
            {
                Id = u.Id,
                Code = u.Code,
                FullName = u.FullName,
                BirthDate = u.BirthDate,
                Email = u.Email,
                Phone = u.Phone,
                Address = u.Address
            });
        }

        public async Task<ReadUserDTO?> GetByIdAsync(Guid id)
        {
            var user = await _uow.Users.GetByIdAsync(id);

            if (user == null) return null;

            return new ReadUserDTO
            {
                Id = user.Id,
                Code = user.Code,
                FullName = user.FullName,
                BirthDate = user.BirthDate,
                Email = user.Email,
                Phone = user.Phone,
                Address = user.Address
            };
        }

        public async Task<ReadUserDTO> CreateAsync(CreateUserDTO dto)
        {
            var user = new User
            {
                Code = dto.Code,
                FullName = dto.FullName,
                BirthDate = dto.BirthDate,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address
            };

            await _uow.Users.AddAsync(user);
            await _uow.SaveAsync();

            return new ReadUserDTO
            {
                Id = user.Id,
                Code = user.Code,
                FullName = user.FullName,
                BirthDate = user.BirthDate,
                Email = user.Email,
                Phone = user.Phone,
                Address = user.Address
            };
        }

        public async Task<ReadUserDTO?> UpdateAsync(Guid id, UpdateUserDTO dto)
        {
            var user = await _uow.Users.GetByIdAsync(id);

            if (user == null) return null;

            user.Code = dto.Code;
            user.FullName = dto.FullName;
            user.BirthDate = dto.BirthDate;
            user.Email = dto.Email;
            user.Phone = dto.Phone;
            user.Address = dto.Address;

            _uow.Users.Update(user);
            await _uow.SaveAsync();

            return new ReadUserDTO
            {
                Id = user.Id,
                Code = user.Code,
                FullName = user.FullName,
                BirthDate = user.BirthDate,
                Email = user.Email,
                Phone = user.Phone,
                Address = user.Address
            };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await _uow.Users.GetByIdAsync(id);

            if (user == null) return false;

            _uow.Users.Delete(user);
            await _uow.SaveAsync();

            return true;
        }

        public async Task<PageResult<ReadUserDTO>> GetPagedAsync(
            int pageIndex,
            int pageSize,
            string? search,
            string? sortBy,
            bool desc)
        {
            // ========== SEARCH ==========
            Expression<Func<User, bool>>? filter = null;

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim().ToLower();

                filter = u =>
                    u.Code.ToLower().Contains(search) ||
                    u.FullName.ToLower().Contains(search) ||
                    (u.Email != null && u.Email.ToLower().Contains(search));
            }

            // ========== SORT ==========
            Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy = sortBy?.ToLower() switch
            {
                "code" => desc
                    ? q => q.OrderByDescending(x => x.Code)
                    : q => q.OrderBy(x => x.Code),

                "email" => desc
                    ? q => q.OrderByDescending(x => x.Email)
                    : q => q.OrderBy(x => x.Email),

                "fullname" or _ => desc
                    ? q => q.OrderByDescending(x => x.FullName)
                    : q => q.OrderBy(x => x.FullName),
            };

            // ========== CALL REPOSITORY (Repo làm paging) ==========
            var paged = await _uow.Users.GetPagedAsync(
                filter: filter,
                orderBy: orderBy,
                pageIndex: pageIndex,
                pageSize: pageSize
            );

            // ========== MAP TO DTO ==========
            var items = paged.Items.Select(u => new ReadUserDTO
            {
                Id = u.Id,
                Code = u.Code,
                FullName = u.FullName,
                BirthDate = u.BirthDate,
                Email = u.Email,
                Phone = u.Phone,
                Address = u.Address
            });

            return new PageResult<ReadUserDTO>(items, paged.TotalItems, pageIndex, pageSize);
        }
    }
}
