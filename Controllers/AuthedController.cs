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
    
    public class AuthedController : Controller
    {

     
        
     private UserManager<ApplicationUser> _usrMngr;
    private SignInManager<ApplicationUser> _signInMngr;

    public AuthedController(UserManager<ApplicationUser> usrMngr,SignInManager<ApplicationUser> signInMngr)
    {
        _usrMngr = usrMngr;
        _signInMngr = _signInMngr;
        // _context = context;
    }
        public IActionResult Index(){
            if(!User.Identity.IsAuthenticated){

                return RedirectToAction("Login","Account");
                
            }
            
            
            return View();
        }
   public IActionResult homepage()
    {
        if(!User.Identity.IsAuthenticated){

                return RedirectToAction("Login","Account");
                
            }
        return View();
    }
     public IActionResult Profile()
    {
        if(!User.Identity.IsAuthenticated){

                return RedirectToAction("Login","Account");
                
            }
        return View();
    }

        
    }
}
