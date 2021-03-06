﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Quilt4.Service.Authentication;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Models;
using Quilt4Net.Core.DataTransfer;
using ChangePasswordBindingModel = Quilt4.Service.Models.ChangePasswordBindingModel;
using UserInfoViewModel = Quilt4.Service.Models.UserInfoViewModel;

namespace Quilt4.Service.Controllers.WebAPI
{
    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private readonly IUserBusiness _userBusiness;
        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        //[Obsolete("Acces via the business layer.")]
        //private readonly IRepository _repository;

        public AccountController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
            //_repository = repository;
        }

        //public AccountController(ApplicationUserManager userManager, ApplicationRoleManager roleManager, ISecureDataFormat<AuthenticationTicket> accessTokenFormat, IRepository repository, IUserBusiness userBusiness)
        //{
        //    _repository = repository;
        //    UserManager = userManager;
        //    RoleManager = roleManager;
        //    AccessTokenFormat = accessTokenFormat;
        //    _userBusiness = userBusiness;
        //}

        //public ApplicationUserManager UserManager
        //{
        //    get
        //    {
        //        return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    }
        //    private set
        //    {
        //        _userManager = value;
        //    }
        //}

        //public ApplicationRoleManager RoleManager
        //{
        //    get
        //    {
        //        return _roleManager ?? Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();
        //    }
        //    private set
        //    {
        //        _roleManager = value;
        //    }
        //}

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        // GET api/Account/UserInfo
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("UserInfo")]
        public UserInfoViewModel GetUserInfo()
        {
            var externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);
            
            var userInfo = _userBusiness.GetUser(User.Identity.Name);

            var response = new UserInfoViewModel
            {
                UserName = User.Identity.GetUserName(),
                Email = userInfo.Email,
                FullName = userInfo.FullName,
                AvatarUrl = userInfo.AvatarUrl,
                HasRegistered = externalLogin == null,
                LoginProvider = externalLogin != null ? externalLogin.LoginProvider : null,                
            };
            return response;
        }

        // POST api/Account/Logout
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        //// GET api/Account/ManageInfo?returnUrl=%2F&generateState=true
        //[Route("ManageInfo")]
        //public async Task<ManageInfoViewModel> GetManageInfo(string returnUrl, bool generateState = false)
        //{
        //    throw new NotImplementedException();
        //    //IdentityUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

        //    //if (user == null)
        //    //{
        //    //    return null;
        //    //}

        //    //List<UserLoginInfoViewModel> logins = new List<UserLoginInfoViewModel>();

        //    //foreach (IdentityUserLogin linkedAccount in user.Logins)
        //    //{
        //    //    logins.Add(new UserLoginInfoViewModel
        //    //    {
        //    //        LoginProvider = linkedAccount.LoginProvider,
        //    //        ProviderKey = linkedAccount.ProviderKey
        //    //    });
        //    //}

        //    //if (user.PasswordHash != null)
        //    //{
        //    //    logins.Add(new UserLoginInfoViewModel
        //    //    {
        //    //        LoginProvider = LocalLoginProvider,
        //    //        ProviderKey = user.UserName,
        //    //    });
        //    //}

        //    //return new ManageInfoViewModel
        //    //{
        //    //    LocalLoginProvider = LocalLoginProvider,
        //    //    Username = user.UserName,
        //    //    Logins = logins,
        //    //    ExternalLoginProviders = GetExternalLogins(returnUrl, generateState)
        //    //};
        //}

        // POST api/Account/ChangePassword
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            throw new NotImplementedException();
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
            //    model.NewPassword);
            
            //if (!result.Succeeded)
            //{
            //    return GetErrorResult(result);
            //}

            //return Ok();
        }

        // POST api/Account/SetPassword
        [Route("SetPassword")]
        public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
        {
            throw new NotImplementedException();
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);

            //if (!result.Succeeded)
            //{
            //    return GetErrorResult(result);
            //}

            //return Ok();
        }

        // POST api/Account/AddExternalLogin
        [Route("AddExternalLogin")]
        public async Task<IHttpActionResult> AddExternalLogin(AddExternalLoginBindingModel model)
        {
            throw new NotImplementedException();
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

            //AuthenticationTicket ticket = AccessTokenFormat.Unprotect(model.ExternalAccessToken);

            //if (ticket == null || ticket.Identity == null || (ticket.Properties != null
            //    && ticket.Properties.ExpiresUtc.HasValue
            //    && ticket.Properties.ExpiresUtc.Value < DateTimeOffset.UtcNow))
            //{
            //    return BadRequest("External login failure.");
            //}

            //ExternalLoginData externalData = ExternalLoginData.FromIdentity(ticket.Identity);

            //if (externalData == null)
            //{
            //    return BadRequest("The external login is already associated with an account.");
            //}

            //IdentityResult result = await UserManager.AddLoginAsync(User.Identity.GetUserId(),
            //    new UserLoginInfo(externalData.LoginProvider, externalData.ProviderKey));

            //if (!result.Succeeded)
            //{
            //    return GetErrorResult(result);
            //}

            //return Ok();
        }

        // POST api/Account/RemoveLogin
        [Route("RemoveLogin")]
        public async Task<IHttpActionResult> RemoveLogin(RemoveLoginBindingModel model)
        {
            throw new NotImplementedException();
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //IdentityResult result;

            //if (model.LoginProvider == LocalLoginProvider)
            //{
            //    result = await UserManager.RemovePasswordAsync(User.Identity.GetUserId());
            //}
            //else
            //{
            //    result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(),
            //        new UserLoginInfo(model.LoginProvider, model.ProviderKey));
            //}

            //if (!result.Succeeded)
            //{
            //    return GetErrorResult(result);
            //}

            //return Ok();
        }

        //// GET api/Account/ExternalLogin
        //[OverrideAuthentication]
        //[HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        //[AllowAnonymous]
        //[Route("ExternalLogin", Name = "ExternalLogin")]
        //public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        //{
        //    throw new NotImplementedException();
        //    //if (error != null)
        //    //{
        //    //    return Redirect(Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));
        //    //}

        //    //if (!User.Identity.IsAuthenticated)
        //    //{
        //    //    return new ChallengeResult(provider, this);
        //    //}

        //    //ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

        //    //if (externalLogin == null)
        //    //{
        //    //    return InternalServerError();
        //    //}

        //    //if (externalLogin.LoginProvider != provider)
        //    //{
        //    //    Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
        //    //    return new ChallengeResult(provider, this);
        //    //}

        //    //ApplicationUser user = await UserManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider,
        //    //    externalLogin.ProviderKey));

        //    //bool hasRegistered = user != null;

        //    //if (hasRegistered)
        //    //{
        //    //    Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

        //    //     ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(UserManager, OAuthDefaults.AuthenticationType);
        //    //    ClaimsIdentity cookieIdentity = await user.GenerateUserIdentityAsync(UserManager, CookieAuthenticationDefaults.AuthenticationType);

        //    //    AuthenticationProperties properties = ApplicationOAuthProvider.CreateProperties(user.UserName);
        //    //    Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);
        //    //}
        //    //else
        //    //{
        //    //    IEnumerable<Claim> claims = externalLogin.GetClaims();
        //    //    ClaimsIdentity identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
        //    //    Authentication.SignIn(identity);
        //    //}

        //    //return Ok();
        //}

        // GET api/Account/ExternalLogins?returnUrl=%2F&generateState=true
        [AllowAnonymous]
        [Route("ExternalLogins")]
        public IEnumerable<ExternalLoginViewModel> GetExternalLogins(string returnUrl, bool generateState = false)
        {
            IEnumerable<AuthenticationDescription> descriptions = Authentication.GetExternalAuthenticationTypes();
            List<ExternalLoginViewModel> logins = new List<ExternalLoginViewModel>();

            string state;

            if (generateState)
            {
                const int strengthInBits = 256;
                state = RandomOAuthStateGenerator.Generate(strengthInBits);
            }
            else
            {
                state = null;
            }

            foreach (AuthenticationDescription description in descriptions)
            {
                ExternalLoginViewModel login = new ExternalLoginViewModel
                {
                    Name = description.Caption,
                    Url = Url.Route("ExternalLogin", new
                    {
                        provider = description.AuthenticationType,
                        response_type = "token",
                        client_id = Startup.PublicClientId,
                        redirect_uri = new Uri(Request.RequestUri, returnUrl).AbsoluteUri,
                        state = state
                    }),
                    State = state
                };
                logins.Add(login);
            }

            return logins;
        }

        //TODO: Change this into a GET function. Use post directly to the /Token if that is perfered.
        // POST api/Account/Login
        [HttpPost]
        //[HttpGet]
        [AllowAnonymous]
        [Route("Login")]
        //public async Task<HttpResponseMessage> LoginUser([FromUri]LoginUserBindingModel model)
        public async Task<HttpResponseMessage> LoginUser([FromBody]LoginUserBindingModel model)
        {
            // Invoke the "token" OWIN service to perform the login: /api/token
            // Ugly hack: I use a server-side HTTP POST because I cannot directly invoke the service (it is deeply hidden in the OAuthAuthorizationServerHandler class)
            var request = HttpContext.Current.Request;
            var tokenServiceUrl = request.Url.GetLeftPart(UriPartial.Authority) + request.ApplicationPath + "/api/Account/Token";
            using (var client = new HttpClient())
            {
                var requestParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", model.Username),
                    new KeyValuePair<string, string>("password", model.Password)
                };
                var requestParamsFormUrlEncoded = new FormUrlEncodedContent(requestParams);
                var tokenServiceResponse = await client.PostAsync(tokenServiceUrl, requestParamsFormUrlEncoded);
                var responseString = await tokenServiceResponse.Content.ReadAsStringAsync();
                var responseCode = tokenServiceResponse.StatusCode;
                var responseMsg = new HttpResponseMessage(responseCode)
                {
                    Content = new StringContent(responseString, Encoding.UTF8, "application/json")
                };

                //TODO: Lock account when too many failed logins has been performed within a too short time.

                return responseMsg;
            }
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
            throw new NotImplementedException();
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };

            //var callerIp = HttpContext.Current.Request.UserHostAddress;
            //var result = await UserManager.CreateAsync(user, model.Password, callerIp);
            //if (result.Succeeded)
            //{
            //    result = await AutoAdminAssignment(user) ?? result;

            //    await AddExtraInfo(user, model);

            //}

            //if (!result.Succeeded)
            //{
            //    return GetErrorResult(result);
            //}

            //return Ok();
        }

        private async Task AddExtraInfo(ApplicationUser user, RegisterBindingModel model)
        {
            throw new NotImplementedException();
            ////TODO: Get from settings in db;
            ////var defaultAvatarUrl = "http://ci.quilt4.com/master/web/images/avatar5.png";
            //var defaultAvatarUrl = (string)null;

            //try
            //{
            //    //TODO: Access through business layer
            //    _repository.AddUserExtraInfo(user.UserName, model.FirstName, model.LastName, defaultAvatarUrl);
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }

        private async Task<IdentityResult> AutoAdminAssignment(ApplicationUser user)
        {
            throw new NotImplementedException();
            //var autoAdminCountString = System.Configuration.ConfigurationManager.AppSettings["AutoAdminCount"];
            //int autoAdminCount;
            //if (!int.TryParse(autoAdminCountString, out autoAdminCount))
            //{
            //    autoAdminCount = 2;
            //}

            //IdentityResult result = null;
            //if (UserManager.Users.Count() <= autoAdminCount)
            //{
            //    var role = await RoleManager.FindByNameAsync(Constants.Administrators);
            //    if (role == null)
            //    {
            //        result = await RoleManager.CreateAsync(new ApplicationRole { Name = Constants.Administrators });
            //    }

            //    if (result == null || result.Succeeded)
            //    {
            //        var userId = UserManager.FindByName(user.UserName).Id;
            //        result = await UserManager.AddToRoleAsync(userId, Constants.Administrators);
            //    }
            //}

            //return result;
        }

        [Authorize(Roles = Constants.Administrators)]
        [Route("Role/Assign")]
        public async Task<IHttpActionResult> AddRole(AddRoleModel addRoleModel)
        {
            throw new NotImplementedException();
            //var result = await UserManager.AddToRoleAsync(addRoleModel.UserName, addRoleModel.Role);

            //return Ok();
        }

        // POST api/Account/RegisterExternal
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("RegisterExternal")]
        public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel model)
        {
            throw new NotImplementedException();
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //var info = await Authentication.GetExternalLoginInfoAsync();
            //if (info == null)
            //{
            //    return InternalServerError();
            //}

            //var user = new ApplicationUser { UserName = model.Username, Username = model.Username };

            //IdentityResult result = await UserManager.CreateAsync(user);
            //if (!result.Succeeded)
            //{
            //    return GetErrorResult(result);
            //}

            //result = await UserManager.AddLoginAsync(user.Id, info.Login);
            //if (!result.Succeeded)
            //{
            //    return GetErrorResult(result); 
            //}
            //return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }

        private static class RandomOAuthStateGenerator
        {
            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }

        #endregion
    }
}