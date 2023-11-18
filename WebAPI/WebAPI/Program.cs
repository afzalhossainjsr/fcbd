using System.Text;
using DAL.Common;
using DAL.Repository.Auth;
using DAL.Repository.Hubs;
using DAL.Repository.Store;
using DAL.Repository.Store.Menu;
using DAL.Repository.Store.Product;
using DAL.Repository.Store.ProductOrder;
using DAL.Repository.Store.Reports;
using DAL.Repository.Store.StockOrder;
using DAL.Repository.Team;
using DAL.Services.Auth;
using DAL.Services.Hubs;
using DAL.Services.Store;
using DAL.Services.Store.Menu;
using DAL.Services.Store.Product;
using DAL.Services.Store.ProductOrder;
using DAL.Services.Store.Reports;
using DAL.Services.Store.StockOrder;
using DAL.Services.Team;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Model.Auth;
using Newtonsoft.Json.Serialization;
using WebAPI.Auth;
using WebAPI.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var emailConfig = builder.Configuration
      .GetSection("EmailConfiguration")
      .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);

builder.Services.AddControllers().AddJsonOptions(options =>
{
  options.JsonSerializerOptions.PropertyNamingPolicy = null;
})
.AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
});

// For Entity Framework  
builder.Services.AddDbContext<ApplicationDbContext>(options =>
              options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// For Identity  
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

//Adding Authentication
builder.Services.AddAuthentication(options =>
{
 options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
 options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:ValidAudience"],
                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
                };
            }).AddCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Enable this in production with HTTPS
                options.ExpireTimeSpan = TimeSpan.FromDays(30); // Set cookie expiration time
                options.LoginPath = "/Account/Login"; // Specify the login path for UI
                options.SlidingExpiration = true;
            });


//Auth Related Settings
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 4;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
    options.SignIn.RequireConfirmedPhoneNumber = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
builder.Services.AddCors();
builder.Services.AddSignalR();

builder.Host.ConfigureServices(services =>
{
    services.AddScoped<IDataManager, DataManager>();
    services.AddScoped<IConnectedUserDAL, ConnectedUserDAL>();
    services.AddScoped<IUserRegisterDAL, UserRegisterDAL>();
    services.AddScoped<IPlayerRegistrationDal, PlayerRegistrationDal>();
    services.AddScoped<IBasicDataDAL, BasicDataDAL>();
    services.AddScoped<IStoreProductSupplierDAL, StoreProductSupplierDAL>();
    services.AddScoped<IStoreProductDAL, StoreProductDAL>();
    services.AddScoped<IWebProductViewDAL, WebProductViewDAL>();
    services.AddScoped<IProductOrderDAL, ProductOrderDAL>();
    services.AddScoped<IOrderProcessDAL, OrderProcessDAL>();
    services.AddScoped<IStockOrderDAL, StockOrderDAL>();
    services.AddScoped<IColorInfoDAL, ColorInfoDAL>();

    services.AddScoped<IConnectedUserDAL, ConnectedUserDAL>();
    services.AddScoped<IStoreReportDAL, StoreReportDAL>();
    services.AddScoped<IMenuPermissionDAL, MenuPermissionDAL>();
});

//app configurations
var app = builder.Build();
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        });
}
app.UseRouting();

app.UseCors(options =>
  options.WithOrigins("http://localhost:4200", "http://localhost:4300", "http://103.222.21.142:4300")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    );

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory()))
});


app.UseDeveloperExceptionPage();




app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); 
    endpoints.MapHub<NotificationHubs>("/notification");
});

app.Run();

