﻿using L4D2_match_history.Server.Services.Contract;
using L4D2_match_history.Server.Services;
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
    public class MatchInfoController : ControllerBase
    {
        private readonly ILogger<MatchInfoController> _logger;
        private readonly IUpdateDataService updateDataService;

        public MatchInfoController(IUpdateDataService updateDataService, ILogger<MatchInfoController> logger)
        {
            this.updateDataService = updateDataService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            if (System.IO.File.Exists(UpdateDataService.DataFile))
            {
                return Content($"{{\"lastUpdate\":\"{System.IO.File.GetLastWriteTimeUtc("l4d2plays.json")}\",\"matchs\":{await System.IO.File.ReadAllTextAsync(UpdateDataService.DataFile)}}}", "application/json");
            }
            return Content($"{{\"lastUpdate\":\"{System.IO.File.GetLastWriteTimeUtc("l4d2plays.json")}\",\"matchs\": []}}", "application/json");
        }

        [HttpPut]
        public void Update()
        {
            updateDataService.UpdateMatchInfos();
        }
    }
}
