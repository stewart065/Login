using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using login.Entities;
using login.Identity;
using login.ViewModel;
using System.Net.Sockets;

namespace login.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private UserManager<ApplicationUser> _usrMngr;
        private SignInManager<ApplicationUser> _signInMngr;
        private readonly sampleappContext _context;

        private readonly ILogger<AccountController> _logger;

        public string Addres2 { get; private set; }

        //LMSCContext context,


        public AccountController(IMapper mapper, sampleappContext context, UserManager<ApplicationUser> usrMngr, SignInManager<ApplicationUser> signInMngr, ILogger<AccountController> logger)
        {
            _mapper = mapper;
            _context = context;
            _usrMngr = usrMngr;
            _signInMngr = signInMngr;
            _logger = logger;

        }
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                System.Security.Claims.ClaimsPrincipal currentUser = this.User;

                if (currentUser.IsInRole("Admin"))
                {
                    _logger.LogInformation("Logged in usertype: ADMIN");
                    return RedirectToAction("DisplayPendingAndApprovedUsers", "Account");
                }
                else if (currentUser.IsInRole("Approved"))
                {
                    _logger.LogInformation("Logged in usertype: APPROVED");
                    return RedirectToAction("Homepage", "Authed");
                }
                else if (currentUser.IsInRole("Pending"))
                {
                    TempData["Message"] = "Your account is still pending approval.";
                    return RedirectToAction("Login", "Account");
                }

                // Optional: Handle case where user is authenticated but does not have a specified role
                _logger.LogWarning("Authenticated user without a recognized role.");
                return RedirectToAction("AccessDenied", "Account");
            }

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registration)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser
                    {
                        Email = registration.Email,
                        UserName = registration.UserName,
                        PhoneNumber = registration.ContactNumber,
                        Addres2 = registration.Address,
                        StName = registration.StoreName
                    };

                    var result = await _usrMngr.CreateAsync(user, registration.Password);

                    if (result.Succeeded)
                    {

                        await _usrMngr.AddToRoleAsync(user, "Pending");

                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            if (error.Code == "DuplicateUserName")
                                ModelState.AddModelError("DuplicateUserName", "Username already taken");
                            if (error.Code == "DuplicateEmail")
                                ModelState.AddModelError("DuplicateEmail", "Email already Registered");
                        }

                        // If registration fails, return the registration view with errors
                        return View("Index", registration);
                    }
                }

                // If model state is invalid, return the registration view with errors
                return View("Index", registration);
            }
            catch (Exception)
            {
                // If an exception occurs, return the registration view with errors
                return View("Index", registration);
            }
        }


        [HttpGet]
        public ActionResult Login()
        {
            if (!User.Identity.IsAuthenticated)

                return View();

            return RedirectToAction("homepage", "Authed");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var user = _context.AspNetUsers.FirstOrDefault(q => q.Email == login.Email);
                var appUser = _mapper.Map<ApplicationUser>(user);

                if (user != null)
                {
                    // Check if the user's role is pending
                    var userRole = await _usrMngr.GetRolesAsync(appUser);
                    if (userRole.Contains("Pending"))
                    {
                        ModelState.AddModelError("", "Your account is still pending approval.");
                        return View("Login", login);
                    }

                    // Check if the user is in the "Admin" role
                    if (userRole.Contains("Admin"))
                    {
                        var result = await _signInMngr.PasswordSignInAsync(appUser, login.Password, false, false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("DisplayPendingAndApprovedUsers", "Account");
                        }
                        else
                        {
                            ModelState.AddModelError("passwordAndEmailNotMatch", "Email and password didn't match");
                            return View("Login", login);
                        }
                    }

                    // Check if the user is in the "Approved" role
                    if (userRole.Contains("Approved"))
                    {
                        var result = await _signInMngr.PasswordSignInAsync(appUser, login.Password, false, false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Homepage", "Authed");
                        }
                        else
                        {
                            ModelState.AddModelError("passwordAndEmailNotMatch", "Email and password didn't match");
                            return View("Login", login);
                        }
                    }

                    // Redirect to the login page for users with other roles
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("noEmailFind", "No email found");
                    return View(login);
                }
            }
            return View();
        }



        public async Task<IActionResult> DisplayPendingAndApprovedUsers(string message = null)
        {
            var pendingUsers = await _usrMngr.GetUsersInRoleAsync("Pending");
            var approvedUsers = await _usrMngr.GetUsersInRoleAsync("Approved");

            var model = new PendingAndApprovedUsersViewModel
            {
                PendingUsers = pendingUsers.ToList(),
                ApprovedUsers = approvedUsers.ToList(),
                PendingUsersCount = pendingUsers.Count,
                ApprovedUsersCount = approvedUsers.Count,
                Message = message // Add a message property to the model
            };

            if (!User.Identity.IsAuthenticated)
            {

                return RedirectToAction("Login", "Account");

            }

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUserRoles(string userId, string newRole)
        {
            var user = await _usrMngr.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            // Remove all existing roles
            var userRoles = await _usrMngr.GetRolesAsync(user);
            await _usrMngr.RemoveFromRolesAsync(user, userRoles);

            // Add the new role
            await _usrMngr.AddToRoleAsync(user, newRole);

            // Redirect to the display page with a success message
            return RedirectToAction(nameof(DisplayPendingAndApprovedUsers), new { message = "User role updated successfully." });
        }


        public IActionResult UserAccountItems()
        {
               if (!User.Identity.IsAuthenticated)
            {

                return RedirectToAction("Login", "Account");

            }
            return View();
        }

        // var result = await _signInMngr.PasswordSignInAsync(appUser, login.Password, false, false);
        // if (result.Succeeded)
        // {
        //     return RedirectToAction("homepage", "Authed");
        // }
        // else
        // {
        //     ModelState.AddModelError("passwordAndEmailNotMatch", "Email and password didn't match");
        //     return View("Login", login);
        // }

        [Route("Account/Logout")]
        [Route("Admin/Account/Logout")]
        [Route("Student/Account/Logout")]
        //[Route("Teacher/Account/Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInMngr.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }




    }
}