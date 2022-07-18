using Jobsite.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobsite.DAL.Services
{
    public interface IUserService
    {
        User Authenticate(string email, string password);
    }

}
