using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Yarp.ReverseProxy.Configuration;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "AuthService",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                "$2a$12$ZETwMmO7izj.lw29Nm8Xv.9ubCgjgdGq3rUBymyJDUn09mvImzUle"))
        };
        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = context =>
            {
                var httpContext = context.HttpContext;
                var requestPath = httpContext.Request.Path.Value ?? "";

                var claims = context.Principal?.Claims;
                var audienceClaims = claims?.Where(c => c.Type == "aud").Select(c => c.Value).ToList();
 
                if (requestPath.StartsWith("/user/") && (audienceClaims == null || !audienceClaims.Contains("UserAudience")))
                {
                    context.Fail("Invalid audience for user.");
                }


                return Task.CompletedTask;
            }
        };
    });




builder.Services.AddReverseProxy()
    .LoadFromMemory(new[]
    {
        new RouteConfig
    {
        RouteId = "user-public",
        ClusterId = "user_cluster",
        Match = new RouteMatch
        {
            Path = "/user/api/Car/{**catch-all}"
        }
    },


    new RouteConfig
    {
        RouteId = "user",
        ClusterId = "user_cluster",
        Match = new RouteMatch
        {
            Path = "/user/{**catch-all}"
        },
        AuthorizationPolicy = "jwt"
    },
        new RouteConfig
        {
            RouteId = "auth",
            ClusterId = "auth_cluster",
            Match = new Yarp.ReverseProxy.Configuration.RouteMatch
            {
                Path = "/auth/{**catch-all}"
            },
            AuthorizationPolicy = "jwt"
        }
    },
    new[]
    {
        
        new ClusterConfig
        {
            ClusterId = "auth_cluster",
            Destinations = new Dictionary<string, DestinationConfig>
            {
                { "auth_destination", new() { Address = "http://localhost:8080/" } }
            }
        }
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("jwt", policy =>
    {
        policy.RequireAuthenticatedUser();
    });
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapReverseProxy();

app.Run();