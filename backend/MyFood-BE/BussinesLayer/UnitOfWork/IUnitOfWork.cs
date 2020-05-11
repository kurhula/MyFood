using BussinesLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.UnitOfWork
{
    public interface IUnitOfWork
    {
        IAuthService AuthService { get; }
        IUserService UserService { get; }
    }
}
