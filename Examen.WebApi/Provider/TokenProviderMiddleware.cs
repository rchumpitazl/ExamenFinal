using Examen.UnidadDeTrabajo;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Examen.WebApi.Provider
{
    public class TokenProviderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TokenProviderOptions _options;
        private readonly IUnidadTrabajo _unidad;

        public TokenProviderMiddleware(RequestDelegate next, IOptions<TokenProviderOptions> options, IUnidadTrabajo unidad)
        {
            _next = next;
            _options = options.Value;
            _unidad = unidad;
        }

        public Task Invoke(HttpContext context)
        {
            if (!context.Request.Path.ToString().ToLower().Contains(_options.Path))
            {
                return _next(context);
            }

            if (!context.Request.Method.Equals("POST") || !context.Request.HasFormContentType)
            {
                context.Response.StatusCode = 400;
                return context.Response.WriteAsync("Bad request.");
            }

            return GenerateToken(context);
        }

        private async Task GenerateToken(HttpContext context)
        {
            var email = context.Request.Form["email"];
            var contrasena = context.Request.Form["contrasena"];

            var identity = await CheckIdentity(email, contrasena);

            if (identity == null)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Usuario o contraseña invalidos");
                return;
            }

            var now = DateTime.UtcNow;

            var claims = new Claim[]{
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var jwt = new JwtSecurityToken(
                issuer:_options.Issuer,
                audience: _options.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(_options.Expiration),
                signingCredentials: _options.SigningCredentials
                );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new {
                access_token = encodedJwt,
                expires_in = (int)_options.Expiration.TotalSeconds
            };

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        private Task<ClaimsIdentity> CheckIdentity(string email, string contrasena)
        {
            var usuario = _unidad.Usuarios.ValidarUsuario(email, contrasena);

            if(usuario != null)
            {
                return Task.FromResult(new ClaimsIdentity(new GenericIdentity(email, "token"), new Claim[] { }));
            }

            return Task.FromResult<ClaimsIdentity>(null);

        }
    }
}
