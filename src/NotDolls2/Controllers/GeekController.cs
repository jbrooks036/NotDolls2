using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
// using Microsoft.Data.Entity;
using NotDolls2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;


namespace NotDolls2.Controllers
{
    [Route("api/[controller]")]
    public class GeekController : Controller
    {
        private NotDolls2Context _context;

        public GeekController(NotDolls2Context context)
        {
            _context = context;
        }

        // GET: api/geek
        [HttpGet]
        public IActionResult Get()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IQueryable<object> geek = from g in _context.Geek
                                           select g;

            if (geek == null)
            {
                return NotFound();
            }

            return Ok(geek);
        }

        // GET api/geek/5
        [HttpGet("{id}", Name = "GetGeek")]
        public IActionResult Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Geek geek = _context.Geek.Single(m => m.UserId == id);

            if (geek == null)
            {
                return NotFound();
            }

            return Ok(geek);
        }

        // POST api/geek
        [HttpPost]
        public IActionResult Post([FromBody]Geek geek)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Geek.Add(geek);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (GeekExists(geek.UserId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetGeek", new { id = geek.UserId }, geek);

        }


        private bool GeekExists(int id)
        {
            return _context.Geek.Count(e => e.UserId == id) > 0;
        }

    }
}
