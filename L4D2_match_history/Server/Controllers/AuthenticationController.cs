using System;
using System.Text;
using System.Threading.Tasks;

using L4D2_match_history.Server.Infrastructures;
using L4D2_match_history.Server.Services.Contract;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace L4D2_match_history.Server.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ISteamService steamService;

        public AuthenticationController(ISteamService steamService)
        {
            this.steamService = steamService;
        }

        [HttpGet("~/auth")]
        public async Task<IActionResult> Authentication()
        {
            return View("Auth");
        }

        [HttpGet("~/signin")]
        public async Task<IActionResult> SignIn()
        {
            return Challenge(new AuthenticationProperties { RedirectUri = "/signedin" }, "Steam");
        }

        [HttpGet("~/signedin")]
        public async Task<IActionResult> SignedIn()
        {
            //StringBuilder sb = new StringBuilder();
            //foreach (var c in HttpContext.User.Claims)
            //{
            //    sb.AppendLine($"{c.Type} : {c.Value}");
            //}
            //Console.WriteLine(sb.ToString());
            return Redirect("/");
        }

        [HttpGet("~/signout"), HttpPost("~/signout")]
        public IActionResult SignOutCurrentUser()
        {
            // Instruct the cookies middleware to delete the local cookie created
            // when the user agent is redirected from the external identity provider
            // after a successful authentication flow (e.g Google or Facebook).
            return SignOut(new AuthenticationProperties { RedirectUri = "/" },
                CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [HttpGet("~/status")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var p = await steamService.GetSteamPlayer(HttpContext.GetSteamId());
            if (p == null)
            {
                return Ok(new object());
            }
            return Ok(p);
        }
    }
}
