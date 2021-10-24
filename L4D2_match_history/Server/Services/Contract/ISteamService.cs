using L4D2_match_history.Shared;

using System.Threading.Tasks;

namespace L4D2_match_history.Server.Services.Contract
{
    public interface ISteamService
    {
        Task<SteamPlayer> GetSteamPlayer(string steamId64, bool force = false);
    }
}
