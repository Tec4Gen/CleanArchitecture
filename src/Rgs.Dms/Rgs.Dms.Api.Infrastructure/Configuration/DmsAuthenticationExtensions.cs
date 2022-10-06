using Microsoft.AspNetCore.Authentication;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Rgs.Dms.Api.Infrastructure.Rest;
using System.Net;

namespace Rgs.Dms.Api.Infrastructure.Configuration
{
    public static class DmsAuthenticationExtensions
    {
        public static AuthenticationBuilder AddDmsAuthentication(this IServiceCollection services)
        {
            return services
                .AddAuthentication("OAuth")
                .AddJwtBearer("OAuth", config =>
                 {
                     config.Events = new JwtBearerEvents
                     {
                         OnMessageReceived = context =>
                         {
                             if (context.Request.Headers.ContainsKey("token"))
                             {
                                 context.Token = context.Request.Headers["token"];
                             }

                             else if (context.Request.Query.ContainsKey("token"))
                             {
                                 context.Token = context.Request.Query["token"];
                             }

                             return Task.CompletedTask;
                         },
                         OnAuthenticationFailed = c =>
                         {
                             c.NoResult();
                             c.Response.StatusCode = ((int)HttpStatusCode.Unauthorized);
                             c.Response.ContentType = "text/plain";
                             c.Response.WriteAsync("Authorization has been denied for this request.").Wait();
                             return Task.CompletedTask;
                         }
                     };

                     //Секрет для кодировки
                     byte[] secretBytes = Encoding.UTF8.GetBytes("SecretytSecretyt");

                     var key = new SymmetricSecurityKey(secretBytes);

                     config.TokenValidationParameters = new TokenValidationParameters
                     {
                         // укзывает, будет ли валидироваться издатель при валидации токена
                         ValidateIssuer = false,
                         // строка, представляющая издателя
                         ValidIssuers = JwtTokenProvider.Issuers,

                         // будет ли валидироваться время существования
                         ValidateLifetime = false,

                         // будет ли валидироваться потребитель токена
                         ValidateAudience = false,
                         // установка потребителя токена
                         //ValidAudience = ,

                         // валидация ключа безопасности
                         ValidateIssuerSigningKey = false,
                         //установка ключа безопасности
                         IssuerSigningKey = key,
                     };
                 });
        }
    }
}
