using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L4D2_match_history.Shared
{
    public class MatchInfo
    {
        public MatchInfo(string mapName, string[] players, DateTime playDate, string url, string thumbnail)
        {
            this.MapName = mapName;
            this.Players = players;
            this.PlayDate = playDate;
            this.VideoUrl = url;
            this.VideoThumbnail = thumbnail;
        }

        public MatchInfo() { }

        public string MapName { get; set; }
        public string[] Players { get; set; }
        public DateTime PlayDate { get; set; }
        public string VideoUrl { get; set; }
        public string VideoThumbnail { get; set; }
        public string GetPlayerNames()
        {
            var playerCount = Players.Length;
            string playerNames;
            if (playerCount > 2)
            {
                playerNames = string.Join(", ", Players.Take(playerCount - 2));
                playerNames += $" and {Players[playerCount - 1]}";
            }
            else
            {
                playerNames = string.Join(" and ", Players);
            }
            return playerNames;
        }

        public override string ToString()
        {
            return $"{MapName} with {GetPlayerNames()} on {PlayDate.ToShortDateString()}";
        }
    }
}
