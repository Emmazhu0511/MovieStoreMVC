using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Core.Models.Request;
using MovieStore.Core.ServiceInterfaces;

namespace MovieStore.MVC.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;

        public AccountController(IUserService userService) {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult Register()  //duandian
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(UserRegisterRequestModel userRegisterRequestModel) //duandian
        {

            if (ModelState.IsValid)
            { //input from UI is valid(Server Side Validation)

                var createUser = await _userService.RegisterUser(userRegisterRequestModel);
                return RedirectToAction("Login");
            }

            //We take this object from the View
            return View();
        }

        [HttpGet]
        public ActionResult Login() {

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(UserLoginRequestModel loginRequest)
        {
            //Http is stateless means each and every request is indepent of each other
            // 10:00 AM u1 -> http: localhost / movies / index
            // 10:00 AM u2 -> http:localhost/movies/index
            // 10:00 AM u3 -> http:localhost/movies/index
            // 10:01 AM u1 -> http:localhost/account/login, we can create an autheticate cookie
                                                          //cookie is one way of storing information on browser,
                                                          //localstorage and sessionstorage, cookies, 
                                                          //if there are any cookies present then those cookies will be automatically sent to the server.

            // 10:02 AM u1 -> http:localhost/user/purchases --->we are expecting a page that shows movies bought by user1
                                                              //we need to check if the cookie is expired or not and valid or not
                                                              //Cookies is one way of state mangement, client-side
            // 10:01 AM u2 -> http:localhost/account/login

            if (ModelState.IsValid) {

                //call service layer to validate user
                var user = await _userService.ValidatUser(loginRequest.Email, loginRequest.Password);  //duandian

                if (user == null)
                {

                    ModelState.AddModelError(string.Empty, "Invalid Login");
                }

                //We want to show First Name, Last Name on header (Navigation bar)

                //Step1:Use Claims, create a claims based on your application needs

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.GivenName, user.FirstName),
                    new Claim(ClaimTypes.Surname, user.LastName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Email),
                };

                //Step2: we need to create an indentity object to hold those claims
                var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                //Step3: Finally, we are going to create a cookie that will be attached to the HTTP response
                //HttpContext is probably the most improtant class in ASP.NET that holds all the information regarding that Http Request/Response
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity));
                //manually creating cookie
                //HttpContext.Response.Cookies.Append("NewCookieName", "English"); //para1: Cookie Name, para2: Content

                //Once ASP.Net Creates Authentication Cookies, it will check for that cookie in the HttpRequest and see if the cookie is not expired
                //and it will decrypt the information present in the Cookie and check whether User is Authenticated and will also get claims from the cookies
                return LocalRedirect("~/"); //return to one level up which is the home page
            }

            return View();
        }

        public async Task<IActionResult> Logout() {

            await HttpContext.SignOutAsync();//Clear the Cookies
            return LocalRedirect("~/");
        }
    }
}
