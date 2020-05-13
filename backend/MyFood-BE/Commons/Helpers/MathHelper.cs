using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.Helpers
{
    public static class MathHelper
    {
        public static decimal Rating(decimal TotalRating, decimal quantity)
            => TotalRating / quantity;
    }
}
