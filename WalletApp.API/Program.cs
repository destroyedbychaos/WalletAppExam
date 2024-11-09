using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WalletApp.DAL.Data;
using WalletApp.DAL.Models.Identity;
using WalletApp.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add database context
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer("name=Default");
});

// Add CORS
var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins, policy =>
    {
        policy.WithOrigins("http://localhost:3000")
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

// Add identity
builder.Services.AddIdentity<User, Role>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = Settings.PasswordLength;
    options.Password.RequiredUniqueChars = 1;
})
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<AppDbContext>();

// Add authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            RequireExpirationTime = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AuthSettings:key"])),
            ValidIssuer = builder.Configuration["AuthSettings:issuer"],
            ValidAudience = builder.Configuration["AuthSettings:audience"]
        };
    });

// Add services

// Add repositories

// Add automapper

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "NPR321", Version = "v1" });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "������ JWT �����"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            //new []{ Settings.AdminRole, Settings.UserRole }
            Array.Empty<string>()
        }
    });
});
//builder.Services.AddApplicationInsightsTelemetry();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMiddleware<MiddlewareExceptionHandling>();
//app.UseMiddleware<MiddlewareSecurityTokenExceptionHandling>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(myAllowSpecificOrigins);

//app.UseStaticFiles(new StaticFileOptions
//{
    //FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "data")),
    //RequestPath = "/files"
//});

//app.UseStaticFiles(new StaticFileOptions
//{
    //FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "data/images")),
    //RequestPath = "/images"
//});

app.MapControllers();

//app.SeedData();

app.Run();