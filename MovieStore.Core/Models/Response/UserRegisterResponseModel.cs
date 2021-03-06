﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MovieStore.Core.Models.Response
{

    //ViewModels, we are creating these model classes just for the Views/Client
    //They will have only properties that are required by the View
    //ViewModels are also useful when you want to combin multiple models, like different properties from different entity class
    //They are called ViewModels in MVC
    //DTO (Data transfer objects) in API's

    public class UserRegisterResponseModel
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
