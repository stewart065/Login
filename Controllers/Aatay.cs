using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using login.Entities;
using login.Models;
using login.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace login.Controllers
{
    // [ApiController]
    // [Route("api/[controller]")]
    [Route("[controller]")]
    public class Aatay : ControllerBase
    {
        private readonly dataofmedContext _context;

        private readonly sampleappContext _sample;
        public Aatay(dataofmedContext context, sampleappContext sample)
        {
            _context = context;
            _sample = sample;
        }

        [HttpGet("Bayot")]
        public async Task<ActionResult<List<Invt>>> Bayot()
        {

            var invs = await _context.Invts.ToListAsync();
            var usrs = await _sample.AspNetUsers.ToListAsync();

            var invtrs =  (
            from inv in invs
            join usr in usrs
            on inv.Userid equals usr.Id
            where inv.Medicinetype == "Tablet"

            select new dataofalls
            {
                Id = inv.Id,
                Userid = inv.Userid,
                //UserName = usr.UserName,
                storeName = usr.StName,
                address = usr.Addres2,
                Typemed = inv.Typemed,
                Mendname = inv.Mendname,
                Price = inv.Price,
                Medis = inv.Medis,
                Stck = inv.Stock,
                Statusmed = inv.Statusmed,
                Medicinetyp = inv.Medicinetype

            }).ToList();

            return Ok(invtrs);
        }


    }
}