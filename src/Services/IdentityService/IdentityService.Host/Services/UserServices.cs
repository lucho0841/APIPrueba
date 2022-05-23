using IdentityService.Host.Models;
using SharedService.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Host.Services
{

    public interface IUserService
    {
        Task<PetitionResponse> Authenticate(User user);
    }
    public class UserServices : IUserService
    {
        public async Task<PetitionResponse> Authenticate(User user)
        {
            User newUser = new User();
            newUser.userId = 1;
            newUser.email = "luis.solano7960@gmail.com";
            newUser.password = "lucho0841";

            if (user != null) { 
                if (user.email.Equals(newUser.email) && user.password.Equals(newUser.password))
                {
                    return new PetitionResponse
                    {
                        success = true,
                        message = "Ok",
                        result = newUser
                    };
                }
            }
            throw new NotImplementedException();
        }
    }
}
