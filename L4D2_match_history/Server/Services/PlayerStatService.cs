﻿using L4D2_match_history.Server.DAL;
using L4D2_match_history.Server.Services.Contract;
using L4D2_match_history.Shared;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace L4D2_match_history.Server.Services
{
    public class PlayerStatService : IPlayerStatService
    {
        private readonly PlayerRankDbContext dbContext;
        private readonly ISteamService steamService;
        private readonly ILogger<PlayerStatService> logger;

        public PlayerStatService(PlayerRankDbContext dbContext, ISteamService steamService, ILogger<PlayerStatService> logger)
        {
            this.dbContext = dbContext;
            this.steamService = steamService;
            this.logger = logger;
        }

        public PlayerRankView GetPlayerRank(string steamId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PlayerRankView> GetPlayerRanks()
        {
            foreach (PlayerRank pr in this.dbContext.PlayerRanks)
            {
                if (string.IsNullOrEmpty(pr.steam_id64))
                {
                    pr.steam_id64 = pr.GetSteamId64();
                }
                if (string.IsNullOrEmpty(pr.play_style))
                {
                    pr.play_style = pr.GetPlayStyle();
                }

                if (string.IsNullOrEmpty(pr.last_known_alias_unicode))
                {
                    try
                    {
                        pr.last_known_alias_unicode = GetSteamUserName(pr.steam_id64).Result;
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "Unable to get unicode name of {0}", pr.last_known_alias);
                        pr.last_known_alias_unicode = pr.last_known_alias.Normalize();
                    }
                }
            }
            this.dbContext.SaveChanges();
            return this.dbContext.PlayerRankViews.ToList();
        }

        public IEnumerable<PlayerSkillModifier> GetPlayerSkillModifiers()
        {
            return dbContext.PlayerSkillModifiers.ToList();
        }

        public bool UpdatePlayerSkillModifiers(IEnumerable<PlayerSkillModifier> updatedPlayerSkillModifiers)
        {
            var query = dbContext.PlayerSkillModifiers.ToList();
            foreach (var upsm in updatedPlayerSkillModifiers)
            {
                var exist = query.FirstOrDefault(q => q.name == upsm.name);
                if (exist != null)
                {
                    exist.modifier = upsm.modifier;
                    exist.update_date = DateTime.UtcNow;
                }
            }
            dbContext.SaveChanges();
            return true;
        }

        private async Task<string> GetSteamUserName(string steamid64)
        {
            return (await steamService.GetSteamPlayer(steamid64))?.personaname;
        }
    }
}
