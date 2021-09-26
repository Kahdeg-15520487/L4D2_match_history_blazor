using L4D2_match_history.Server.DAL;
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
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;
        private readonly ILogger<PlayerStatService> logger;

        public PlayerStatService(PlayerRankDbContext dbContext, IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<PlayerStatService> logger)
        {
            this.dbContext = dbContext;
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
            this.logger = logger;
        }

        public PlayerRankView GetPlayerRank(string steamId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PlayerRankView> GetPlayerRanks()
        {
            //this.dbContext.PlayerRanks.ToList();
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

        private async Task<string> GetSteamUserName(string steamid64)
        {
            HttpClient client = httpClientFactory.CreateClient();
            var res = await client.GetAsync($"https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key={configuration["steamkey"]}&steamids={steamid64}");
            if (res.IsSuccessStatusCode)
            {
                var rd = await res.Content.ReadAsStringAsync();
                var d = JsonConvert.DeserializeObject<Root>(rd);
                return d.response.players[0].personaname;
            }
            else
            {
                throw new Exception($"{res.StatusCode} : {res.ReasonPhrase} , {await res.Content.ReadAsStringAsync()} ");
            }
        }
    }

    public class Root
    {
        public SteamResponse response { get; set; }

    }

    public class SteamResponse
    {
        public SteamPlayer[] players { get; set; }
    }

    public class SteamPlayer
    {
        public string personaname { get; set; }
    }
}
