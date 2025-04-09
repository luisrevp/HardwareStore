using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using HardwareStore.BE.DbContext;
using HardwareStore.BE.Services;
using HardwareStore.BE.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SqlServerLocalDb")?.ToString();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddCookie(IdentityConstants.ApplicationScheme)
.AddJwtBearer(x =>
{
    var configSection = builder.Configuration.GetSection("JwtSettings");
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = configSection["Issuer"],
        ValidAudience = configSection["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configSection["Key"] ?? string.Empty)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});


builder.Services.AddAuthorization();
builder.Services
    .AddIdentityCore<User>()
    .AddEntityFrameworkStores<HardwareStoreDbContext>()
    .AddApiEndpoints();

builder.Services.AddControllers(); // enabling controllers (useEndpoints)
builder.Services.AddDbContext<HardwareStoreDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IUserCartService, UserCartService>();




// MIDDLEWARES SECTION
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting(); // ENABLE ROUTES IN OUR CONTROLLER
app.MapControllers(); // ENABLE ENDPOINTS FROM CONTROLLER
app.UseCors(options => options.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
app.MapIdentityApi<User>();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.Run();
