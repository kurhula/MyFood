using BussinesLayer.Repositories.Interfaces;
using DataBaseLayer.Models.Users;
using DataBaseLayer.Persistence;
using DataBaseLayer.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Interfaces
{
    public interface IUserService : IBaseRepository<AppUser, ApplicationDbContext>, IFilterRepository<UserVM, FilterUserVM>
    {
        Task<bool> CreateUser(CreateUserVM model);
        Task<AppUser> GetUser(string id);
        Task<AppUser> GetByEmail(string email);
        Task<bool> SoftDelete(string id);
    }
}
