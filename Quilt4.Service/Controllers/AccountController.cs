using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Quilt4.Service;
using Quilt4.Service.Authentication;
using Quilt4.Service.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        private ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            //private set
            //{
            //    _userManager = value;
            //}
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        ////
        //// POST: /Account/Login
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    // Invoke the "token" OWIN service to perform the login: /api/token
        //    // Ugly hack: I use a server-side HTTP POST because I cannot directly invoke the service (it is deeply hidden in the OAuthAuthorizationServerHandler class)
        //    var request = System.Web.HttpContext.Current.Request;
        //    var tokenServiceUrl = request.Url.GetLeftPart(UriPartial.Authority) + request.ApplicationPath + "/api/Account/Token";
        //    using (var client = new HttpClient())
        //    {
        //        var requestParams = new List<KeyValuePair<string, string>>
        //        {
        //            new KeyValuePair<string, string>("grant_type", "password"),
        //            new KeyValuePair<string, string>("username", model.Username),
        //            new KeyValuePair<string, string>("password", model.Password)
        //        };
        //        var requestParamsFormUrlEncoded = new FormUrlEncodedContent(requestParams);
        //        var tokenServiceResponse = await client.PostAsync(tokenServiceUrl, requestParamsFormUrlEncoded);
        //        var responseString = await tokenServiceResponse.Content.ReadAsStringAsync();
        //        var responseCode = tokenServiceResponse.StatusCode;
        //        //var responseMsg = new HttpResponseMessage(responseCode)
        //        //{
        //        //    Content = new StringContent(responseString, Encoding.UTF8, "application/json")
        //        //};

        //        ////TODO: Lock account when too many failed logins has been performed within a too short time.

        //        //return responseMsg;

        //        //// This doesn't count login failures towards account lockout
        //        //// To enable password failures to trigger account lockout, change to shouldLockout: true
        //        //var result = await SignInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, shouldLockout: false);
        //        switch (responseCode)
        //        {
        //            case HttpStatusCode.OK:
        //                //case SignInStatus.Success:
        //                return RedirectToLocal(returnUrl);
        //            //case SignInStatus.LockedOut:
        //            //    return View("Lockout");
        //            //case SignInStatus.RequiresVerification:
        //            //    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
        //            //case SignInStatus.Failure:
        //            default:
        //                ModelState.AddModelError("", "Invalid login attempt.");
        //                return View(model);
        //        }
        //    }
        //}

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            throw new NotImplementedException();
            // Require that the user has already logged in via username/password or external login
            //if (!await SignInManager.HasBeenVerifiedAsync())
            //{
            //    return View("Error");
            //}
            //return System.Web.UI.WebControls.View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            throw new NotImplementedException();
            //if (!ModelState.IsValid)
            //{
            //    return System.Web.UI.WebControls.View(model);
            //}

            //// The following code protects for brute force attacks against the two factor codes. 
            //// If a user enters incorrect codes for a specified amount of time then the user account 
            //// will be locked out for a specified amount of time. 
            //// You can configure the account lockout settings in IdentityConfig
            //var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            //switch (result)
            //{
            //    case SignInStatus.Success:
            //        return RedirectToLocal(model.ReturnUrl);
            //    case SignInStatus.LockedOut:
            //        return View("Lockout");
            //    case SignInStatus.Failure:
            //    default:
            //        ModelState.AddModelError("", "Invalid code.");
            //        return System.Web.UI.WebControls.View(model);
            //}
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Username };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    await SignInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            throw new NotImplementedException();
            //if (userId == null || code == null)
            //{
            //    return View("Error");
            //}
            //var result = await UserManager.ConfirmEmailAsync(userId, code);
            //return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            throw new NotImplementedException();
            //return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            throw new NotImplementedException();
            //if (ModelState.IsValid)
            //{
            //    var user = await UserManager.FindByNameAsync(model.Username);
            //    if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
            //    {
            //        // Don't reveal that the user does not exist or is not confirmed
            //        return View("ForgotPasswordConfirmation");
            //    }

            //    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
            //    // Send an email with this link
            //    // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
            //    // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
            //    // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
            //    // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            //}

            //// If we got this far, something failed, redisplay form
            //return System.Web.UI.WebControls.View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            throw new NotImplementedException();
            //return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            throw new NotImplementedException();
            //return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            throw new NotImplementedException();
            //if (!ModelState.IsValid)
            //{
            //    return System.Web.UI.WebControls.View(model);
            //}
            //var user = await UserManager.FindByNameAsync(model.Username);
            //if (user == null)
            //{
            //    // Don't reveal that the user does not exist
            //    return RedirectToAction("ResetPasswordConfirmation", "Account");
            //}
            //var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            //if (result.Succeeded)
            //{
            //    return RedirectToAction("ResetPasswordConfirmation", "Account");
            //}
            //AddErrors(result);
            //return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            throw new NotImplementedException();
            //return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            throw new NotImplementedException();
            //var userId = await SignInManager.GetVerifiedUserIdAsync();
            //if (userId == null)
            //{
            //    return View("Error");
            //}
            //var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            //var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            //return System.Web.UI.WebControls.View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            throw new NotImplementedException();
            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}

            //// Generate the token and send it
            //if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            //{
            //    return View("Error");
            //}
            //return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            throw new NotImplementedException();
            //var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            //if (loginInfo == null)
            //{
            //    return RedirectToAction("Login");
            //}

            //// Sign in the user with this external login provider if the user already has a login
            //var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            //switch (result)
            //{
            //    case SignInStatus.Success:
            //        return RedirectToLocal(returnUrl);
            //    case SignInStatus.LockedOut:
            //        return View("Lockout");
            //    case SignInStatus.RequiresVerification:
            //        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
            //    case SignInStatus.Failure:
            //    default:
            //        // If the user does not have an account, then prompt the user to create an account
            //        ViewBag.ReturnUrl = returnUrl;
            //        ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
            //        return System.Web.UI.WebControls.View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Username = loginInfo.Username });
            //}
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            throw new NotImplementedException();
            //if (User.Identity.IsAuthenticated)
            //{
            //    return RedirectToAction("Index", "Manage");
            //}

            //if (ModelState.IsValid)
            //{
            //    // Get the information about the user from the external login provider
            //    var info = await AuthenticationManager.GetExternalLoginInfoAsync();
            //    if (info == null)
            //    {
            //        return View("ExternalLoginFailure");
            //    }
            //    var user = new ApplicationUser { UserName = model.Username, Username = model.Username };
            //    var result = await UserManager.CreateAsync(user);
            //    if (result.Succeeded)
            //    {
            //        result = await UserManager.AddLoginAsync(user.Id, info.Login);
            //        if (result.Succeeded)
            //        {
            //            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            //            return RedirectToLocal(returnUrl);
            //        }
            //    }
            //    AddErrors(result);
            //}

            //ViewBag.ReturnUrl = returnUrl;
            //return System.Web.UI.WebControls.View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            throw new NotImplementedException();
            //return View();
        }

        protected override void Dispose(bool disposing)
        {
            //if (disposing)
            //{
            //    if (_userManager != null)
            //    {
            //        _userManager.Dispose();
            //        _userManager = null;
            //    }

            //    if (_signInManager != null)
            //    {
            //        _signInManager.Dispose();
            //        _signInManager = null;
            //    }
            //}

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}