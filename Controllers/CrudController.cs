using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using login.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using login.Identity;


namespace login.Controllers
{
    public class CrudController : Controller
    {
        private readonly dataofmedContext _context;
        private UserManager<ApplicationUser> _usrMngr;
        private SignInManager<ApplicationUser> _signInMngr;

        public CrudController(UserManager<ApplicationUser> usrMngr,SignInManager<ApplicationUser> signInMngr,dataofmedContext context)
        {
            _context = context;
             _usrMngr = usrMngr;
          _signInMngr = signInMngr;
        }

        // GET: pd
        public async Task<IActionResult> Index()
        {
             if(!User.Identity.IsAuthenticated){

                return RedirectToAction("Login","Account");
                
            }

            return View(await _context.Typesmeds.ToListAsync());

        }

        // GET: pd/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if(!User.Identity.IsAuthenticated){

                return RedirectToAction("Login","Account");
                
            }

            if (id == null)
            {
                return NotFound();
            }

            var @type = await _context.Typesmeds
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@type == null)
            {
                return NotFound();
            }

            return View(@type);
        }

        // GET: pd/Create
        public IActionResult Create()
        {
            if(!User.Identity.IsAuthenticated){

                return RedirectToAction("Login","Account");
                
            }
            
            return View();
        }

        // POST: pd/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Typesmed @type)
        {

            if (ModelState.IsValid)
            {
                _context.Add(@type);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@type);
        }

        // GET: pd/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if(!User.Identity.IsAuthenticated){

                return RedirectToAction("Login","Account");
                
            }
            if (id == null)
            {
                return NotFound();
            }

            var @type = await _context.Typesmeds.FindAsync(id);
            if (@type == null)
            {
                return NotFound();
            }
            return View(@type);
        }

        // POST: pd/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")]Typesmed @type)
        {
            if (id != @type.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@type);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeExists(@type.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(@type);
        }

        // GET: pd/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if(!User.Identity.IsAuthenticated){

                return RedirectToAction("Login","Account");
                
            }
            if (id == null)
            {
                return NotFound();
            }

            var @type = await _context.Typesmeds
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@type == null)
            {
                return NotFound();
            }

            return View(@type);
        }

        // POST: pd/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @type = await _context.Typesmeds.FindAsync(id);
            _context.Typesmeds.Remove(@type);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeExists(int id)
        {
            return _context.Typesmeds.Any(e => e.Id == id);
        }
    }

    public class Types
    {
        public int Id { get; internal set; }
    }
}
