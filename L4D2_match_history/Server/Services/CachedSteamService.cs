using L4D2_match_history.Server.Services.Contract;
using L4D2_match_history.Shared;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace L4D2_match_history.Server.Services
{
    public class CachedSteamService : ISteamService
    {
        private readonly IConfiguration configuration;
        private readonly IMemoryCache cache;
        private readonly IHttpClientFactory httpClientFactory;

        public CachedSteamService(IConfiguration configuration, IMemoryCache cache, IHttpClientFactory httpClientFactory)
        {
            this.configuration = configuration;
            this.cache = cache;
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<SteamPlayer> GetSteamPlayer(string steamId64, bool force = false)
        {
            if (string.IsNullOrEmpty(steamId64))
            {
                return null;
            }

            SteamPlayer player = default;
            if (force || !cache.TryGetValue<SteamPlayer>(steamId64, out player))
            {
                HttpClient client = httpClientFactory.CreateClient();
                var res = await client.GetAsync($"https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key={configuration["steamkey"]}&steamids={steamId64}");

                if (res.IsSuccessStatusCode)
                {
                    var rd = await res.Content.ReadAsStringAsync();
                    var d = JsonConvert.DeserializeObject<Root>(rd);
                    if (d.response.players.Length == 0)
                    {
                        return null;
                    }
                    else
                    {
                        var p = d.response.players.First();
                        cache.Set<SteamPlayer>(steamId64, p);
                        return p;
                    }
                }
                else
                {
                    throw new Exception($"{res.StatusCode} : {res.ReasonPhrase} , {await res.Content.ReadAsStringAsync()} ");
                }
            }

            return player;
        }
    }
}
