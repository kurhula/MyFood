using DataBaseLayer.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseLayer.Models.Commons
{
    public class CommonsProperty
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; } = DateTime.Now;
        public string CreatedAtStr => CreatedAt.ToString("dd/MM/yyyy");
        public string UpdateAtStr => UpdateAt.ToString("dd/MM/yyyy");
        public State State { get; set; } = State.Active;
    }
}
