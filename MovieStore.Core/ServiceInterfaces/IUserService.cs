using MovieStore.Core.Entities;
using MovieStore.Core.Models.Request;
using MovieStore.Core.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Core.ServiceInterfaces
{
   public interface IUserService
    {
        Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel requestModel); //register

        Task<UserLoginResponseModel> ValidatUser(string email, string password); //Login



    }
}
