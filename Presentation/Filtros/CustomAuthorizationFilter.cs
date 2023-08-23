namespace Presentation.Filtros
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Presentation.Errors;
    using Presentation.Models;
    using System.Security.Claims;
    using System.Text;
    public class CustomAuthorizationFilter : ActionFilterAttribute
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomAuthorizationFilter(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ;
            //var config=context.HttpContext.RequestServices.GetService<IConfiguration>();
            var authorizationHeader = getAuthorizationFromHeader(context);
            validaterAuthorizationHeaderIsBearerToken(authorizationHeader);
            var bearerToken = getTokenFromBearerToken(authorizationHeader);
            await valideToken(bearerToken);

            await next();
        }
        private string getAuthorizationFromHeader(ActionExecutingContext context)
        {
            string authorizationHeader = context.HttpContext.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                throw new AuthorizationException("La autorización ha fallado: se requiere el encabezado de autorización.");
            }
            return authorizationHeader;
        }
        private bool validaterAuthorizationHeaderIsBearerToken(string authorizationHeader)
        {
            if (!authorizationHeader.StartsWith("Bearer "))
            {
                throw new AuthorizationException("La autorización ha fallado: se requiere un token de acceso JWT válido en el encabezado de autorización.");
            }
            return true;
        }
        private string getTokenFromBearerToken(string bearerToken)
        {
            string token = bearerToken.Substring("Bearer ".Length).Trim();
            return token;
        }
        private async Task<bool> valideToken(string token)
        {
            var httpClient = new HttpClient();
            var body = new { token = token };
            var jsonBody = JsonConvert.SerializeObject(body);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(_configuration["ApiEndPointSeguridadProd"] + "/api/Auth/ValidateToken", content);
            //var response = await httpClient.PostAsync(_configuration["ApiEndPointSeguridadDev"] + "/api/Auth/ValidateToken", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new AuthorizationException("La autorización ha fallado: el token de acceso JWT no es válido.");
            }
            var answer = await response.Content.ReadAsStringAsync();
            var tokenResult = JsonConvert.DeserializeObject<TokenResult>(answer);
            if (!tokenResult.IsValid)
            {
                throw new AuthorizationException($"La autorización ha fallado: {tokenResult.ErrorMessage}.");
            }


            var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, tokenResult.UserId??""),
                            new Claim(ClaimTypes.Email, tokenResult.Email ?? ""),
                            new Claim("NumDocumento", tokenResult.NumDocumento ?? ""),
                        };
            var identity = new ClaimsIdentity(claims, "Custom");
            var principal = new ClaimsPrincipal(identity);

            _httpContextAccessor.HttpContext.User = principal;

            return true;
        }
    }

}
