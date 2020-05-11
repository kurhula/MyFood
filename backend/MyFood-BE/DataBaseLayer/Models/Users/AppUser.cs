using DataBaseLayer.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace DataBaseLayer.Models.Users
{
    public class AppUser : IdentityUser
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; } = DateTime.Now;
        public string CreatedAtStr => CreatedAt.ToString("dd/MM/yyyy");
        public string UpdateAtStr => UpdateAt.ToString("dd/MM/yyyy");
        public State State { get; set; } = State.Active;
        public UserType UserType { get; set; } = UserType.Client;
        [Required]
        public string FullName { get; set; }
        public string Avatar { get; set; }
    }
}
