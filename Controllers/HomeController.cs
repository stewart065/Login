using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using login.Identity;
using login.Models;



namespace login.Controllers
{

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

     private UserManager<ApplicationUser> _usrMngr;
    private SignInManager<ApplicationUser> _signInMngr;

    public HomeController(ILogger<HomeController> logger,
            UserManager<ApplicationUser> usrMngr,
            SignInManager<ApplicationUser> signInMngr)
    {
        _logger = logger;
         _usrMngr = usrMngr;
        _signInMngr = _signInMngr;
    }

    public IActionResult Index()
    {
        if(User.Identity.IsAuthenticated){
            return RedirectToAction("Index","Authed");
        }
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult display(){

        if(User.Identity.IsAuthenticated){
            return RedirectToAction("display","Authed");
        }
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

}
