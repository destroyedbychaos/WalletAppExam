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
using WalletApp.DAL.Repositories.CardRepository;
using WalletApp.DAL.Repositories.CurrencyRepository;
using WalletApp.DAL.Repositories.IncomeSourceRepository;
using WalletApp.DAL.Repositories.SpendingCategoryRepository;
using WalletApp.DAL.Repositories.WalletRepository;
using WalletApp.BLL.Middlewares;
using WalletApp.BLL.Services.AccountService;
using WalletApp.BLL.Services.JwtService;
using WalletApp.BLL.Services.RoleRepository;
using WalletApp.BLL.Services.RoleService;
using WalletApp.BLL.Services.UserService;
using WalletApp.DAL.Repositories.RoleRepository;
using WalletApp.DAL.Repositories.UserRepository;
using WalletApp.BLL.Services.CardService;
using WalletApp.BLL.Services.SpendingCategoryService;
using WalletApp.BLL.Services.IncomeSourceService;
using WalletApp.BLL.Services.WalletService;
using WalletApp.BLL.Services.MailService;


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
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<IIncomeSourceService, IncomeSourceService>();
builder.Services.AddScoped<IMailService, MailService>();
builder.Services.AddScoped<ISpendingCategoryService, SpendingCategoryService>();
builder.Services.AddScoped<IWalletService, WalletService>();

// Add repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<ICardRepository, CardRepository>();
builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddScoped<IIncomeSourceRepository, IncomeSourceRepository>();
builder.Services.AddScoped<ISpendingCategoryRepository, SpendingCategoryRepository>();
builder.Services.AddScoped<IWalletRepository, WalletRepository>();

// Add automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(optinons =>
{
    optinons.SwaggerDoc("v1", new OpenApiInfo { Title = "NPR321", Version = "v1" });

    optinons.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "¬вед≥ть JWT токен"
    });

    optinons.AddSecurityRequirement(new OpenApiSecurityRequirement
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<MiddlewareExceptionHandling>();
app.UseMiddleware<MiddlewareSecurityTokenExceptionHandling>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(myAllowSpecificOrigins);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "data")),
    RequestPath = "/files"
});


app.MapControllers();

//app.SeedData();

app.Run();