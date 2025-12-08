using Application.DTOs;
using Application.DTOs.Common;
using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<PageResult<ReadUserDTO>> GetPagedAsync(int pageNumber, int pageSize)
        {
            var (items, totalItems) = await _uow.Users.GetPagedAsync(pageNumber, pageSize);

            var mapped = items.Select(u => new ReadUserDTO
            {
                Id = u.Id,
                Code = u.Code,
                FullName = u.FullName,
                BirthDate = u.BirthDate,
                Email = u.Email,
                Phone = u.Phone,
                Address = u.Address
            });

            return new PageResult<ReadUserDTO>
            {
                Items = mapped,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
            };
        }
    }
}
