using L4D2_match_history.Server.Services.Contract;
using L4D2_match_history.Shared;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L4D2_match_history.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerRankController : ControllerBase
    {
        private readonly ILogger<PlayerRankController> _logger;
        private readonly IPlayerStatService playerStatService;

        public PlayerRankController(ILogger<PlayerRankController> logger, IPlayerStatService playerStatService)
        {
            _logger = logger;
            this.playerStatService = playerStatService;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(playerStatService.GetPlayerRanks());
        }
    }
}
