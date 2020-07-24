using MovieStore.Core.Entities;
using MovieStore.Core.Models.Request;
using MovieStore.Core.Models.Response;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICryptoService _cryptoService;

        public UserService(IUserRepository userRepository, ICryptoService cryptoService)
        {
            _userRepository = userRepository;
            _cryptoService = cryptoService;
        }

       

        public async Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel requestModel)
        {
            //step1: Check whether this user already exists in that database
            var dbUser = await _userRepository.GetUserByEmail(requestModel.Email); //duandian
            if (dbUser != null) { //We already have this user(Email) in our table

                //return or throw an exception saying conflict user
                throw new Exception("User already registered, please try to Login");
            }

            //step2: Lets create a random unique salt
            //Note: Never ever create your own hashing algorithm, always use industry tested/tried hashing algorithm
            //step3: We hash the password with the salt created in the above step

            var salt = _cryptoService.GenerateSalt();

            var hashedPassword = _cryptoService.HashPassword(requestModel.Password, salt);

            //Create User object so that we can save it to User Table
            var user = new User
            {
                Email = requestModel.Email,
                Salt = salt,
                HashedPassword = hashedPassword,
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName

            };
            //Step4: Save it to Database
            var createUser = await _userRepository.AddAsync(user);

            var response = new UserRegisterResponseModel
            {
                Id = createUser.Id,
                Email = createUser.Email,
                FirstName = createUser.FirstName,
                LastName = createUser.LastName

            };

            return response;
        }


        public async Task<UserLoginResponseModel> ValidatUser(string email, string password)
        {
            //Step1: Get user record from the database by email
            var user = await _userRepository.GetUserByEmail(email);

            if (user == null)
            {
                //user does not exist
                throw new Exception("Register first, user does not exist");

            }

            //step3: Compare the database hashed password with hashed password genereated in step 2
            var hashedPassword = _cryptoService.HashPassword(password, user.Salt);

            if (user.HashedPassword == hashedPassword)
            { //user entered the right password
                //send some user details

                var response = new UserLoginResponseModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DateOfBirth = user.DateOfBirth,
                    Email = user.Email

                };
                return response;
            }
            return null;
        }

        
    }
}
