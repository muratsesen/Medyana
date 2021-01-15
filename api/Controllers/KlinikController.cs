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

        [HttpPost("list")]
        public async Task<QueryResult<Klinik>> List(KlinikFilter filter)
        {
            var result = new QueryResult<Klinik>();
            var query = _context.Klinik.AsQueryable();
            #region Filter
            if (!string.IsNullOrEmpty(filter.Adi))
            {
                query = query.Where(x => x.Adi.Contains(filter.Adi));
            }
            #endregion
            #region Sort
            var map = new Dictionary<string, Expression<Func<Klinik, object>>>
            {
                ["Adi"] = x => x.Adi
            };
            query = query.ApplyOrdering(filter, map);
            #endregion

            result.TotalItems = await query.CountAsync();

            query = query.ApplyPaging(filter);

            result.Items = await query.ToListAsync();

            return result;
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