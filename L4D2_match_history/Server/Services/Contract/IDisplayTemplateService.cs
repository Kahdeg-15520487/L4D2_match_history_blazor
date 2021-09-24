using L4D2_match_history.Shared;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L4D2_match_history.Server.Services.Contract
{
    public interface IDisplayTemplateService
    {
        IEnumerable<DisplayTemplate> GetDisplayTemplates();
        DisplayTemplate GetDisplayTemplate(string displayTemplateName);
        bool SetDisplayTemplate(DisplayTemplate displayTemplate);
        bool DeleteDisplayTemplate(string displayTemplateName);
    }
}
