using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.Helpers
{
    public static class StringHelper
    {
        public static string GetCode(int count = 10) => Guid.NewGuid().ToString().Substring(0, count); 
    }
}
