using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ravi.learn.web.cookieauth.Authentication
{
    public class InMemoryTicketStore : ITicketStore
    {
        private readonly IMemoryCache _memoryCache;

        public InMemoryTicketStore(IMemoryCache memoryCache)
        {
            this._memoryCache = memoryCache;
        }
        public Task RemoveAsync(string key)
        {
            _memoryCache.Remove(key);
            return Task.CompletedTask;
        }

        public Task RenewAsync(string key, AuthenticationTicket ticket)
        {
            _memoryCache.Set<AuthenticationTicket>(key, ticket);
            return Task.CompletedTask;
        }

        public Task<AuthenticationTicket> RetrieveAsync(string key)
        {
            var ticket = _memoryCache.Get<AuthenticationTicket>(key);
            return Task.FromResult(ticket);

        }

        public Task<string> StoreAsync(AuthenticationTicket ticket)
        {
            var cacheKey = ticket.Principal.FindFirst(ClaimTypes.Name).Value;
            _memoryCache.Set<AuthenticationTicket>(cacheKey, ticket);

            return Task.FromResult(cacheKey);
        }
    }
}
