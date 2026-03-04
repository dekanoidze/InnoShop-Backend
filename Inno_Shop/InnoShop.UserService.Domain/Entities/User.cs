using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoShop.UserService.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }= string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Role {  get; set; } = string.Empty;
        public Boolean IsActive { get; set; }=true;
        public Boolean IsEmailConfirmed {  get; set; }=false;
        public DateTime CreatedAt {  get; set; }=DateTime.UtcNow;
        public string? PasswordResetToken {  get; set; }
        public DateTime? PasswordResetTokenExpiry {  get; set; }

    }
}
