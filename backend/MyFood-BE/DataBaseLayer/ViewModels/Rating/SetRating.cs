using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataBaseLayer.ViewModels.Rating
{
    public class SetRating
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public int Rating { get; set; }
    }
}
