using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Models.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using api.Controllers.Resources;

namespace api.Controllers
{
    [ApiController]
    [Route("api/ekipman")]
    public class EkipmanController : ControllerBase
    {
        private readonly ILogger<EkipmanController> logger;
        private readonly ApplicationDbContext _context;
        public EkipmanController(ILogger<EkipmanController> logger, ApplicationDbContext _context)
        {
            this._context = _context;
            this.logger = logger;

        }

        #region API Methods     

        [HttpGet]
        public async Task<IEnumerable<Ekipman>> GetEquipments()
        {
            var ekipman = await _context.Ekipman.Include(ekipman => ekipman.Klinik).ToListAsync();
            return ekipman;
        }

        [HttpPost("ara")]
        public async Task<IEnumerable<Ekipman>> Search(string key)
        {
            return await _context.Ekipman.Where(c => c.Adi.Contains(key)).ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post(EkipmanResource ekipmanResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _context.Ekipman.AddAsync(new Ekipman
                {
                    Adi = ekipmanResource.Adi,
                    Adet = ekipmanResource.Adet,
                    BirimFiyat = ekipmanResource.BirimFiyat,
                    KullanimOrani = ekipmanResource.KullanimOrani,
                    TeminTarihi = ekipmanResource.TeminTarihi,
                    KlinikId = ekipmanResource.KlinikId
                });
                await _context.SaveChangesAsync();
                return Ok(ekipmanResource);
            }
            catch (System.Exception)
            {
                ModelState.AddModelError("Item couldn't created!", "error");
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(Ekipman ekipman)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _context.Ekipman.Update(ekipman);
                await _context.SaveChangesAsync();
                return Ok(ekipman);
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
                var ekipman = _context.Ekipman.Find(id);
                if (ekipman != null)
                {
                    _context.Remove(ekipman);
                    await _context.SaveChangesAsync();
                    return Ok(ekipman);
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