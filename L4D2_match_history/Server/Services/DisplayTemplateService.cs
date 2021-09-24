using L4D2_match_history.Server.DAL;
using L4D2_match_history.Server.Services.Contract;
using L4D2_match_history.Shared;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L4D2_match_history.Server.Services
{
    public class DisplayTemplateService : IDisplayTemplateService
    {
        private readonly PlayerRankDbContext dbContext;
        private readonly ILogger<DisplayTemplateService> logger;

        public DisplayTemplateService(PlayerRankDbContext dbContext, ILogger<DisplayTemplateService> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public IEnumerable<DisplayTemplate> GetDisplayTemplates()
        {
            return dbContext.displayTemplates.ToList();
        }

        public DisplayTemplate GetDisplayTemplate(string displayTemplateName)
        {
            return dbContext.displayTemplates.Include(dt => dt.Columns).FirstOrDefault(dt => dt.Name == displayTemplateName);
        }

        public bool SetDisplayTemplate(DisplayTemplate displayTemplate)
        {
            try
            {
                var existing = dbContext.displayTemplates.Include(dt => dt.Columns).FirstOrDefault(dt => dt.Name == displayTemplate.Name);
                if (existing != null)
                {
                    //update
                    dbContext.displayColumns.RemoveRange(existing.Columns);
                    existing.Name = displayTemplate.Name;
                    existing.Columns = displayTemplate.Columns;
                    dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    //create
                    dbContext.displayTemplates.Add(displayTemplate);
                    dbContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error when setting display template!");
                return false;
            }
        }
        public bool DeleteDisplayTemplate(string displayTemplateName)
        {
            try
            {
                DisplayTemplate displayTemplate = dbContext.displayTemplates.FirstOrDefault(dt => dt.Name == displayTemplateName);
                if (displayTemplate != null)
                {
                    dbContext.Remove(displayTemplate);
                    dbContext.SaveChanges();
                    return true;
                }
                logger.LogWarning("Display template not found: {0}", displayTemplateName);
                return false;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error when deleting display template!");
                return false;
            }
        }
    }
}
