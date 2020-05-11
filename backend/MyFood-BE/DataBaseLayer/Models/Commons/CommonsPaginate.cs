using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseLayer.Models.Commons
{
    public class CommonsPaginate
    {
        public int Page { get; set; } = 1;
        public int QuantityByPage { get; set; } = 10;
    }
}
