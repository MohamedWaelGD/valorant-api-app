global using Microsoft.EntityFrameworkCore;
global using ValorantAPIApp.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using ValorantAPIApp;
using ValorantAPIApp.Data;
using ValorantAPIApp.Services;
using ValorantAPIApp.Services.AgentLoadoutServices;
using ValorantAPIApp.Services.AgentServices;
using ValorantAPIApp.Services.AuthenticationServices;
using ValorantAPIApp.Services.MissionServices;
using ValorantAPIApp.Services.PlayerServices;
using ValorantAPIApp.Services.PlayerWeaponsServices;
using ValorantAPIApp.Services.TeamServicecs;
using ValorantAPIApp.Services.WeaponServices;
using ValorantAPIApp.Services.WeaponSkinServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authentication header using the Bearer scheme, e.g. \"bearer {token}\"",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    c.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddDbContext<ValorantDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("default"));
});
builder.Services.AddHttpClient();
builder.Services.AddScoped(typeof(IAgentService), typeof(AgentService));
builder.Services.AddScoped(typeof(IWeaponService), typeof(WeaponService));
builder.Services.AddScoped(typeof(IAuthService), typeof(AuthService));
builder.Services.AddScoped(typeof(IMissionService), typeof(MissionService));
builder.Services.AddScoped(typeof(IAgentLoadoutService), typeof(AgentLoadoutService));
builder.Services.AddScoped(typeof(ITeamService), typeof(TeamService));
builder.Services.AddScoped(typeof(IPlayerWeaponsService), typeof(PlayerWeaponsService));
builder.Services.AddScoped(typeof(IWeaponSkinService), typeof(WeaponSkinService));
builder.Services.AddScoped(typeof(IPlayerService), typeof(PlayerService));
builder.Services.AddScoped(typeof(HttpService));
builder.Services.AddAutoMapper(typeof(ProfileMapper));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("https://localhost:4200").AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(builder =>
{
    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
