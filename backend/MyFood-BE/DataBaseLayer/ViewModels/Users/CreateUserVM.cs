using DataBaseLayer.Enums.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataBaseLayer.ViewModels.Users
{
    public class CreateUserVM
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        public AuthLevel Rol { get; set; } = AuthLevel.User;
    }

    public class LoginUserVm
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
