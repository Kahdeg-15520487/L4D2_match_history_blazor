using L4D2_match_history.Client.Infrastructure;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace L4D2_match_history.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            using (HttpClient http = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) })
            {
                string envName = builder.HostEnvironment.Environment;
                Settings settings = await http.GetFromJsonAsync<Settings>($"settings.{envName}.json");
                builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(settings.ApiUrl) });
            }

            await builder.Build().RunAsync();
        }
    }
}
