using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "Test",
            ValidAudience = "Test",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                "$2a$12$ZETwMmO7izj.lw29Nm8Xv.9ubCgjgdGq3rUBymyJDUn09mvImzUle")) 
        };
    });

builder.Services.AddAuthorization();


builder.Services.AddReverseProxy()
    .LoadFromMemory(new[]
    {
        new Yarp.ReverseProxy.Configuration.RouteConfig
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
        new Yarp.ReverseProxy.Configuration.ClusterConfig
        {
            ClusterId = "auth_cluster",
            Destinations = new Dictionary<string, Yarp.ReverseProxy.Configuration.DestinationConfig>
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