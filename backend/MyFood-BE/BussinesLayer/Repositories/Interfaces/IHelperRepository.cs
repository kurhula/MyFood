using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Repositories.Interfaces
{
    public interface IHelperRepository
    {
        Task<bool> SoftDelete<T>(T id) where T : IEquatable<T>;
    }
}
