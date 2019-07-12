using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ravi.learn.web.cookieauth.Authentication
{
    public class RevokeAuthenticationEvents : CookieAuthenticationEvents
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<RevokeAuthenticationEvents> _logger;

        public RevokeAuthenticationEvents(IMemoryCache memoryCache, ILogger<RevokeAuthenticationEvents> logger)
        {
            this._memoryCache = memoryCache;
            this._logger = logger;
        }

        public override Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            
            var userId = context.Principal.FindFirst(ClaimTypes.Name);
            _logger.LogDebug($"checking revocation for user - {userId}");

            var cacheKey = $"revoke-{userId.Value}";
            if (_memoryCache.Get<bool>(cacheKey))
            {
                context.RejectPrincipal();
                _memoryCache.Remove(cacheKey);
                _logger.LogDebug($"Removed from cache. Key: {cacheKey}");
            }

            return Task.CompletedTask;
        }
    }
}
