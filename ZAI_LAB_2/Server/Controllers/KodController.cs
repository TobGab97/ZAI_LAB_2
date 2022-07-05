using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZAI_LAB_2.Shared;
using Microsoft.Extensions.Logging;
using System.Text;
using Blazored.LocalStorage;

namespace ZAI_LAB_2.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KodController : ControllerBase
    {

        private readonly LAB1Context _context;
        private readonly ILogger<KodController> _logger;


        public KodController(ILogger<KodController> logger, LAB1Context context)
        {
            _context = context;
            _logger = logger;

        }

        [HttpGet]
        public IEnumerable<KodyPocztowe> Get()
        {

            //return _context.KodyPocztowe.TakeLast(5).ToList();
            return _context.KodyPocztowe.OrderByDescending(k=>k.Id).Take(5).ToList();
            //return _context.KodyPocztowe.ToList();


        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] KodyPocztowe kody)
        {
            var id = _context.KodyPocztowe.Max(k => k.Id);
            long up = 1;
            id = id + up;
            kody.Id = id;
            _context.Add(kody);
            _context.SaveChanges();
            return Ok(kody);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(String Id)
        {
            long id = long.Parse(Id);
            
            var usuwany = _context.KodyPocztowe.FirstOrDefault(x => x.Id == id);

            if (usuwany == null)
            {
                return NotFound("Nie ma takiego kodu");
            }
            _context.KodyPocztowe.Remove(usuwany);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPut("Edytuj")]
        public async Task<IActionResult> Put(KodyPocztowe nowyKod)
        {
            var edytowany = _context.KodyPocztowe.FirstOrDefault(x => x.Id == nowyKod.Id);

            if (edytowany == null)
            {
                return NotFound("Nie ma takiego kodu");
            }

            _context.KodyPocztowe.Remove(edytowany);
            _context.KodyPocztowe.Add(nowyKod);
            _context.SaveChanges();
            return Ok();
        }
    }
}
