using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Models.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/klinik")]
    public class KlinikController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<KlinikController> logger;
        public KlinikController(ILogger<KlinikController> logger, ApplicationDbContext _context)
        {
            this.logger = logger;
            this._context = _context;

        }

        #region API Methods     

        [HttpGet]
        public async Task<IEnumerable<Klinik>> GetClinics()
        {
            var clinics = await _context.Klinik.ToListAsync();
            return clinics;
        }

        [HttpPost("ara")]
        public async Task<IEnumerable<Klinik>> Search(string key)
        {
            return await _context.Klinik.Where(c => c.Adi.Contains(key)).ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Klinik klinik)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _context.Klinik.AddAsync(klinik);
                await _context.SaveChangesAsync();
                return Ok(klinik);
            }
            catch (System.Exception)
            {
                ModelState.AddModelError("Item couldn't created!", "error");
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(Klinik klinik)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _context.Klinik.Update(klinik);
                await _context.SaveChangesAsync();
                return Ok(klinik);
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError("Item couldn't updated!", "error");
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var klinik = _context.Klinik.Find(id);
                if (klinik != null)
                {
                    _context.Klinik.Remove(klinik);
                    await _context.SaveChangesAsync();
                    return Ok(klinik);
                }
                else
                {
                    ModelState.AddModelError("Item couldn't found!", "error");
                    return BadRequest(ModelState);
                }

            }
            catch (System.Exception)
            {
                ModelState.AddModelError("Item doesn't exist", "error");
                return BadRequest(ModelState);
            }
        }
        #endregion
    }
}