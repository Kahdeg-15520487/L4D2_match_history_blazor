using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

using L4D2_match_history.Server.Services.Contract;
using L4D2_match_history.Shared;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace L4D2_match_history.Server.Services
{
    public class UpdateDataService : IUpdateDataService
    {
        public IEnumerable<MatchInfo> UpdateMatchInfos()
        {
            List<MatchInfo> old = null;
            if (File.Exists("l4d2plays.json"))
            {
                old = JsonConvert.DeserializeObject<List<MatchInfo>>(File.ReadAllText("l4d2plays.json"));
            }

            var apiKey = Environment.GetEnvironmentVariable("yt_token");
            var channelId = "UCNeODVHK2KtEQQEMUdQTA4A";

            YouTubeService t = new YouTubeService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                ApiKey = apiKey,
                ApplicationName = "TimeStampBot"
            });

            string nextPageToken = null;
            List<SearchResult> totalResult = new List<SearchResult>();
            bool isDeadCenter = false;
            do
            {
                SearchResource.ListRequest request = t.Search.List("snippet");
                request.ChannelId = channelId;
                request.MaxResults = 50;
                request.Type = "video";
                request.Order = SearchResource.ListRequest.OrderEnum.Date;
                request.Q = "L4D2";
                if (nextPageToken != null)
                {
                    request.PageToken = nextPageToken;
                }
                SearchListResponse result = request.ExecuteAsync().Result;
                nextPageToken = result.NextPageToken;
                var currentResult = result.Items.ToList();
                totalResult.AddRange(currentResult);
                isDeadCenter = currentResult.Where(s => s.Id.VideoId == "cIkFVL82n5s").FirstOrDefault() != null;
            }
            while (!isDeadCenter);

            var filtered = totalResult.Where(j => j.Snippet.Title.Contains("L4D2")).Select(j => new
            {
                Title = WebUtility.HtmlDecode(j.Snippet.Title).Replace("wiht", "with"),
                Thumbnail = j.Snippet.Thumbnails.High.Url,
                Url = $"https://www.youtube.com/watch?v={j.Id.VideoId}"
            }).ToList();

            var plays = new List<MatchInfo>();
            foreach (var item in filtered)
            {
                var parts = item.Title.Split("-").Select(p => p.Trim()).ToArray();
                var playInfoParts = parts[1].Split("with").Select(p => p.Trim()).ToArray();
                var mapName = playInfoParts[0];
                var players = playInfoParts[1].Split(new string[] { ",", "and" }, StringSplitOptions.None).Select(p => p.Trim()).ToArray();
                var playDate = DateTime.ParseExact(parts[2], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                MatchInfo playinfo = new MatchInfo(mapName, players, playDate, item.Url, item.Thumbnail);
                plays.Add(playinfo);
            }
            plays = plays.OrderBy(p => p.PlayDate).ToList();

            File.WriteAllText("l4d2plays.json", JsonConvert.SerializeObject(plays, Formatting.Indented));

            return plays;
        }
    }
}
