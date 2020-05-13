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

    public interface IRatingRepository
    {
        Task<bool> SetRating(Guid id , int newRating);
    }
}
