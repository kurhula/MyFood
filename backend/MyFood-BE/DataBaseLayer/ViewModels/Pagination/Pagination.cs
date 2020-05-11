using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseLayer.ViewModels.Pagination
{
    public class Pagination<TEntity> where TEntity : class
    {
        public int Total { get; set; }
        public int Pages { get; set; }
        public int ActualPage { get; set; }
        public ICollection<TEntity> Entities { get; set; }
    }
}
