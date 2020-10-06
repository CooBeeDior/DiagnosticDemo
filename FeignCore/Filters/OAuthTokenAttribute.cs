using System;
using System.Threading.Tasks;
using WebApiClient.AuthTokens;


namespace FeignCore.Filters
{
    public class OAuthTokenAttribute : AuthTokenFilter
    {
        protected override Task<TokenResult> RequestRefreshTokenAsync(string refresh_token)
        {
            throw new NotImplementedException();
        }

        protected override Task<TokenResult> RequestTokenResultAsync()
        {
            throw new NotImplementedException();
        }
    }
}
