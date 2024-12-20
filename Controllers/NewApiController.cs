using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using login.Models;
using login.Entities;
using login.ViewModel;
using login.Identity;
using Microsoft.AspNetCore.Identity;

namespace login.Controllers
{

    [Route("api/[controller]/[action]")]
    public class NewApiController : ControllerBase
    {
        private readonly dataofmedContext _context;

        private readonly sampleappContext _sample;
        private UserManager<ApplicationUser> _usrMngr;
        private SignInManager<ApplicationUser> _signInMngr;




        public NewApiController(dataofmedContext context, sampleappContext sample, UserManager<ApplicationUser> usrMngr, SignInManager<ApplicationUser> signInMngr)
        {
            _context = context;
            _sample = sample;
            _usrMngr = usrMngr;
            _signInMngr = signInMngr;
        }
        public IActionResult inv(Invt i)
        {
            var loguser = _sample.AspNetUsers.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();

            if (loguser == null)
            {
                return BadRequest("User not found.");
            }

            if (ModelState.IsValid)
            {
                var invt = new Invt
                {
                    Userid = loguser.Id,
                    Typemed = i.Typemed,
                    Mendname = i.Mendname,
                    Price = i.Price,
                    Medis = i.Medis,
                    Stock = i.Stock,
                    Statusmed = i.Statusmed,
                    Medicinetype = i.Medicinetype
                };

                _context.Invts.Add(invt);
                _context.SaveChanges();
                return Ok();
            }

            return BadRequest(ModelState);
        }

        public IActionResult delete(int dl)
        {
            var res = _context.Invts.Where(element => element.Id == dl).FirstOrDefault();
            _context.Invts.Remove(res);
            _context.SaveChanges();
            return Ok();
        }



        public ActionResult<List<Typesmed>> getAllCategories()
        {
            return _context.Typesmeds.ToList();
        }

        public ActionResult<List<InventoryViewModel>> getinvts()
        {
            var invs = _context.Invts.ToList();
            var usrs = _sample.AspNetUsers.ToList();

            var loguser = _sample.AspNetUsers.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();


            var invtrs = (
                from inv in invs
                join usr in usrs
                on inv.Userid equals usr.Id
                where inv.Userid == loguser.Id
                select new InventoryViewModel
                {
                    Id = inv.Id,
                    Userid = inv.Userid,
                    UserName = usr.UserName,
                    Typemed = inv.Typemed,
                    Mendname = inv.Mendname,
                    Price = inv.Price,
                    Medis = inv.Medis,
                    Stck = inv.Stock,
                    Statusmed = inv.Statusmed,
                    Medicinetyp = inv.Medicinetype
                }
            ).ToList();
            return invtrs;

        }
        public ActionResult<List<Invt>> getInvt()
        {

            var invs = _context.Invts.ToList();
            var usrs = _sample.AspNetUsers.ToList();

            var invtrs = (
            from inv in invs
            join usr in usrs
            on inv.Userid equals usr.Id

            select new InventoryViewModel
            {
                Id = inv.Id,
                Userid = inv.Userid,
                UserName = usr.UserName,
                Typemed = inv.Typemed,
                Mendname = inv.Mendname,
                Price = inv.Price,
                Medis = inv.Medis,
                Stck = inv.Stock,
                Statusmed = inv.Statusmed,
                Medicinetyp = inv.Medicinetype

            }
        ).ToList();
            return Ok(invtrs);
        }

        public IActionResult updateinv(Invt wew)
        {
            var loguser = _sample.AspNetUsers.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();

            wew.Userid = loguser.Id;
            _context.Invts.Update(wew);
            _context.SaveChanges();
            return Ok();
        }

        //    public ActionResult<Aspnetuser> prof()
        //     {
        //         try
        //         {
        //             var loguser = _sample.AspNetUsers.FirstOrDefault(x => x.UserName == User.Identity.Name);

        //             if (loguser != null)
        //             {
        //                 // You might want to remove sensitive information before returning it.
        //                 var userInformation = new
        //                 {
        //                     loguser.Id,
        //                     loguser.UserName,
        //                     // Include other properties you want to expose.
        //                     // loguser.Email, loguser.FullName, etc.
        //                 };

        //                 return Ok(userInformation);
        //             }
        //             else
        //             {
        //                 return NotFound("User not found");
        //             }
        //         }
        //         catch (Exception ex)
        //         {
        //             // Log the exception for debugging purposes
        //             // Log.Error("Error retrieving user information", ex);
        //             return StatusCode(500, "Internal server error");
        //         }
        //     }
        [HttpGet]
        public IActionResult GetProfile()
        {
            try
            {
                var loguser = _sample.AspNetUsers.FirstOrDefault(x => x.UserName == User.Identity.Name);

                if (loguser != null)
                {
                    var userData = new
                    {
                        storeName = loguser.StName, // Replace with actual property name
                        userName = loguser.UserName,
                        phoneNumber = loguser.PhoneNumber,
                        email = loguser.Email,
                        address = loguser.Addres2
                        // Add more properties as needed
                    };

                    return Ok(userData);
                }
                else
                {
                    return NotFound("User not found");
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                // Log.Error("Error retrieving user information", ex);
                return StatusCode(500, "Internal server error");
            }
        }

       


    }
}