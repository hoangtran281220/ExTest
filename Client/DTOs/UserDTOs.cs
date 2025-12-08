using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.DTOs
{
    public class CreateUserDTO
    {
        [Required]
        public string Code { get; set; } = null!;
        [Required]
        public string FullName { get; set; } = null!;
        public DateOnly? BirthDate { get; set; }

        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
    }

    public class UpdateUserDTO
    {
        [Required]
        public string Code { get; set; } = null!;
        [Required]
        public string FullName { get; set; } = null!;
        public DateOnly? BirthDate { get; set; }
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
    }
    public class ReadUserDTO
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public DateOnly? BirthDate { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
    }


}
