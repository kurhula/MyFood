using DataBaseLayer.Models.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseLayer.ViewModels.Users
{
    public class FilterUserVM : CommonsPaginate
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
    }
}
