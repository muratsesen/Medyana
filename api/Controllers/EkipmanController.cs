using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using api.Models;
using api.Models.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using api.Controllers.Resources;
using api.Extensions;

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

        [HttpPost("list")]
        public async Task<QueryResult<Ekipman>> List(EkipmanFilter filter)
        {
            var result = new QueryResult<Ekipman>();
            var query = _context.Ekipman.Include(x => x.Klinik).AsQueryable();

            #region Filtering
            if (!string.IsNullOrEmpty(filter.Adi))
            {
                query = query.Where(x => x.Adi.Contains(filter.Adi));
            }
            if (!string.IsNullOrEmpty(filter.Adi))
            {
                query = query.Where(x => x.Adi.Contains(filter.Adi));
            }
            if (filter.TeminTarihi.HasValue)
            {
                query = query.Where(x => x.TeminTarihi == filter.TeminTarihi);
            }
            if (filter.Adet.HasValue)
            {
                query = query.Where(x => x.Adet == filter.Adet);
            }
            if (filter.BirimFiyat.HasValue)
            {
                query = query.Where(x => x.BirimFiyat == filter.BirimFiyat);
            }
            if (filter.KullanimOrani.HasValue)
            {
                query = query.Where(x => x.KullanimOrani == filter.KullanimOrani);
            }
            if (filter.KlinikId.HasValue)
            {
                query = query.Where(x => x.KlinikId == filter.KlinikId);
            }
            #endregion

            #region Sorting
            var map = new Dictionary<string, Expression<Func<Ekipman, object>>>
            {
                ["Adi"] = x => x.Adi,
                ["TeminTarihi"] = x => x.TeminTarihi,
                ["Adet"] = x => x.Adet,
                ["BirimFiyat"] = x => x.BirimFiyat,
                ["KullanimOrani"] = x => x.KullanimOrani,
                ["KlinikId"] = x => x.KlinikId
            };
            query = query.ApplyOrdering(filter, map);
            #endregion

            result.TotalItems = await query.CountAsync();

            query = query.ApplyPaging(filter);

            result.Items = await query.ToListAsync();

            return result;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] EkipmanResource ekipmanResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var klinik = await _context.Klinik.FindAsync(ekipmanResource.KlinikId);
            if(klinik == null) {
                ModelState.AddModelError("Klinik bulunamadı!","error");
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

        [HttpPut("update")]
        public async Task<IActionResult> Put(EkipmanResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var ekipman = _context.Ekipman.Find(resource.Id);
                if (ekipman != null)
                {
                    ekipman.Adet = resource.Adet;
                    ekipman.Adi = resource.Adi;
                    ekipman.BirimFiyat = resource.BirimFiyat;
                    ekipman.KlinikId = resource.KlinikId;
                    ekipman.KullanimOrani = resource.KullanimOrani;
                    ekipman.TeminTarihi = resource.TeminTarihi;
                    _context.Ekipman.Update(ekipman);
                    await _context.SaveChangesAsync();
                    return Ok(ekipman);
                }
                else
                {
                    ModelState.AddModelError("Item couldn't updated!", "error");
                    return BadRequest(ModelState);
                }

            }
            catch (System.Exception)
            {
                ModelState.AddModelError("Item couldn't updated!", "error");
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("delete")]
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