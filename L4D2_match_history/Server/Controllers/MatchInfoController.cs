using L4D2_match_history.Shared;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L4D2_match_history.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MatchInfoController : ControllerBase
    {
        private readonly ILogger<MatchInfoController> _logger;

        public MatchInfoController(ILogger<MatchInfoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Content(await System.IO.File.ReadAllTextAsync("l4d2plays.json"), "application/json");
        }
    }
}
