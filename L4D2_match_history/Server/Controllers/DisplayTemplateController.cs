using L4D2_match_history.Server.Services.Contract;
using L4D2_match_history.Shared;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;

namespace L4D2_match_history.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DisplayTemplateController : ControllerBase
    {
        private readonly ILogger<DisplayTemplateController> _logger;
        private readonly IDisplayTemplateService displayTemplateService;

        public DisplayTemplateController(ILogger<DisplayTemplateController> logger, IDisplayTemplateService displayTemplateService)
        {
            _logger = logger;
            this.displayTemplateService = displayTemplateService;
        }

        [HttpGet]
        public IEnumerable<DisplayTemplate> Get()
        {
            return displayTemplateService.GetDisplayTemplates();
        }

        [HttpGet("{displayTemplateName}")]
        public ActionResult GetDisplayTemplate(string displayTemplateName)
        {
            var result = displayTemplateService.GetDisplayTemplate(displayTemplateName);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public ActionResult SetDisplayTemplate([FromBody] DisplayTemplate displayTemplate)
        {
            if (displayTemplateService.SetDisplayTemplate(displayTemplate))
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("parse")]
        public ActionResult ParseDisplayTemplate([FromBody] string raw)
        {
            using (var reader = new System.IO.StringReader(raw))
            {
                var name = reader.ReadLine();
                var title = reader.ReadLine();

                using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<DisplayColumnMap>();
                    csv.Context.Configuration.HeaderValidated = null;
                    var displayTemplate = new DisplayTemplate()
                    {
                        Name = name,
                        Columns = csv.GetRecords<DisplayColumn>().ToList()
                    };
                    return Ok(displayTemplate);
                }
            }
        }

        [HttpDelete("{displayTemplateName}")]
        public ActionResult DeleteDisplayTemplate(string displayTemplateName)
        {
            if (displayTemplateService.DeleteDisplayTemplate(displayTemplateName))
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
