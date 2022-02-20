using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

using L4D2_match_history.Server.Services.Contract;
using L4D2_match_history.Shared;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace L4D2_match_history.Server.Services
{
    public class UpdateDataService : IUpdateDataService
    {
        public static readonly string DataFile = "/var/L4D2MI/l4d2plays.json";

        private readonly ILogger<UpdateDataService> logger;

        public UpdateDataService(ILogger<UpdateDataService> logger)
        {
            this.logger = logger;
        }

        public IEnumerable<MatchInfo> UpdateMatchInfos()
        {
            List<MatchInfo> old = null;
            if (File.Exists(UpdateDataService.DataFile))
            {
                old = JsonConvert.DeserializeObject<List<MatchInfo>>(File.ReadAllText(UpdateDataService.DataFile));
            }
            else
            {
                old = new List<MatchInfo>();
            }
            try
            {
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
                    Thread.Sleep(1000);
                    //isDeadCenter = currentResult.Exists(s => old.Exists(o => o.VideoUrl == $"https://www.youtube.com/watch?v={s.Id.VideoId}"));
                }
                while (!isDeadCenter);

                var filtered = totalResult.Where(j => j.Snippet.Title.Contains("L4D2") && !old.Exists(o => o.VideoUrl == $"https://www.youtube.com/watch?v={j.Id.VideoId}")).Select(j => new
                {
                    Title = WebUtility.HtmlDecode(j.Snippet.Title).Replace("wiht", "with"),
                    Thumbnail = j.Snippet.Thumbnails.High.Url,
                    Url = $"https://www.youtube.com/watch?v={j.Id.VideoId}"
                }).ToList();

                var plays = new List<MatchInfo>();
                foreach (var item in filtered)
                {
                    try
                    {
                        var parts = item.Title.Split("-").Select(p => p.Trim()).ToArray();
                        var playInfoParts = parts[1].Split("with").Select(p => p.Trim()).ToArray();
                        var mapName = playInfoParts[0];
                        var players = playInfoParts[1].Split(new string[] { ",", "and" }, StringSplitOptions.None).Select(p => p.Trim()).ToArray();
                        var playDate = DateTime.ParseExact(parts[2], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        MatchInfo playinfo = new MatchInfo(mapName, players, playDate, item.Url, item.Thumbnail);
                        plays.Add(playinfo);
                    }
                    catch (Exception ex)
                    {
                        this.logger.LogError(ex, "Can't parse title: {0}", item.Title);
                    }
                }
                plays = plays.Concat(old).OrderBy(p => p.PlayDate).ToList();

                File.WriteAllText(UpdateDataService.DataFile, JsonConvert.SerializeObject(plays, Formatting.Indented));

                return plays;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Can't fetch youtube data.");
            }
            return old;
        }
    }
}
