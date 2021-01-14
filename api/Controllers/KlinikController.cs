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
    [Route("api")]
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
        #endregion
    }
}