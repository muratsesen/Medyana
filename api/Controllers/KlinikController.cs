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
    [Route("api/clinic")]
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

        [HttpGet("clinics")]
        public async Task<IEnumerable<Klinik>> GetClinics()
        {
            var clinics = await _context.Klinik.ToListAsync();
            return clinics;
        }

        [HttpPost("searchClinic")]
        public async Task<IEnumerable<Klinik>> Search(string key)
        {
            return await _context.Klinik.Where(c => c.Adi.Contains(key)).ToListAsync();
        }

        [HttpPost("create")]
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

        [HttpPut("update")]
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

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Klinik klinik)
        {
            try
            {
                _context.Klinik.Remove(klinik);
                await _context.SaveChangesAsync();
                return Ok(klinik);
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