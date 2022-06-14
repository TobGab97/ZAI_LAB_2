using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZAI_LAB_2.Shared;
using Microsoft.Extensions.Logging;
using System.Text;

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


            return _context.KodyPocztowe.ToList();

        }
    }
}
