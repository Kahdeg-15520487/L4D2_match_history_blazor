using L4D2_match_history.Server.DAL;
using L4D2_match_history.Server.Services.Contract;
using L4D2_match_history.Shared;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L4D2_match_history.Server.Services
{
    public class PlayerStatService : IPlayerStatService
    {
        private readonly PlayerRankDbContext dbContext;

        public PlayerStatService(PlayerRankDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public PlayerRank GetPlayerRank(string steamId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PlayerRank> GetPlayerRanks()
        {
            return this.dbContext.PlayerRanks.ToList();
        }
    }
}
