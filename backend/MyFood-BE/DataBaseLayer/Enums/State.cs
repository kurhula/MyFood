using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseLayer.Enums
{
    public enum State
    {
        Active,
        Deleted,
        NotAvaible,

        #region For Orders
        Pending,
        Proccess,
        Canceled,
        Delivered
        #endregion
    }
}
