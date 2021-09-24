﻿using L4D2_match_history.Shared;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L4D2_match_history.Server.Services.Contract
{
    public interface IPlayerStatService
    {
        PlayerRank GetPlayerRank(string steamId);
        IEnumerable<PlayerRank> GetPlayerRanks();
    }
}