using DataBaseLayer.Enums;
using DataBaseLayer.Models.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseLayer.ViewModels.Users
{
    public class UserVM
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; } = DateTime.Now;
        public string CreatedAtStr => CreatedAt.ToString("dd/MM/yyyy");
        public string UpdateAtStr => UpdateAt.ToString("dd/MM/yyyy");
        public State State { get; set; } = State.Active;

        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
    }
}
