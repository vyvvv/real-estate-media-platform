using Azure.Storage.Blobs;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RealEstateMediaPlatform.API.Common;
using RealEstateMediaPlatform.API.Data;
using RealEstateMediaPlatform.API.Exceptions;
using RealEstateMediaPlatform.API.Models;
using RealEstateMediaPlatform.API.Repositories.CaseContactRepositories;
using RealEstateMediaPlatform.API.Repositories.ListingCaseRepositories;
using RealEstateMediaPlatform.API.Repositories.MediaAssetRepositories;
using RealEstateMediaPlatform.API.Repositories.UserRepositories;
using RealEstateMediaPlatform.API.Services.AzureBlobStorage;
using RealEstateMediaPlatform.API.Services.CaseContactServices;
using RealEstateMediaPlatform.API.Services.EmailSenderServices;
using RealEstateMediaPlatform.API.Services.ListingCaseServices;
using RealEstateMediaPlatform.API.Services.MediaAssetServices;
using RealEstateMediaPlatform.API.Services.UserServices;
using RealEstateMediaPlatform.API.Validators;
using System.Text;

var builder = WebApplication.CreateBuilder(args);






// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


builder.Services.AddControllers(); //让项目支持写【apicontroller】等业务，处理http路由等功能
builder.Services.AddEndpointsApiExplorer();//让框架能够扫描所有的Api节点。是生成swagger文档的前置要求
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RealEstate API", Version = "1.0.0" });
    c.EnableAnnotations(); //允许在swagger使用一些注解，代码中使用特性比如说【SwaggerOperation】

        // Define JWT Bearer authentication scheme for Swagger UI
        //效果：加了这段之后，Swagger 网页界面上会出现一个 "Authorize" 按钮，你可以点击后输入 Token，之后测试接口时会自动带上这个 Token。
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,  //要求凭证放在header里而不是body或者url里
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

        // Apply JWT authentication requirement globally to all API endpoints
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
   {
       {
           new OpenApiSecurityScheme
           {
               Reference = new OpenApiReference
               {
                   Type = ReferenceType.SecurityScheme,
                   Id = "Bearer"
               },
               Scheme = "oauth2",
               Name = "Bearer",
               In = ParameterLocation.Header
           },
           new List<string>()
       }
   });
});

// 数据库连接
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
); 
builder.Services.AddSingleton<MongoDbContext>(); //Singleton = 单例 ：整个应用程序运行期间，只创建一个 MongoDbContext 实例，所有请求共用这一个实例
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));


// Identity 注册
builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAutoMapper(typeof(Program));
//注册 AutoMapper 库，这是一个用来自动把一个对象的属性"映射/复制"到另一个对象的工具，常用于把数据库实体（Entity）转换成 DTO，或者反过来

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey not configured");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,           
        ValidateAudience = true,      
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
        ClockSkew = TimeSpan.Zero
    };
});


builder.Services.AddAuthorization();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ICaseContactService, CaseContactService>();
builder.Services.AddScoped<ICaseContactRepository, CaseContactRepository>();

builder.Services.AddScoped<IListingCaseService, ListingCaseService>();
builder.Services.AddScoped<IListingCaseRepository, ListingCaseRepository>();

builder.Services.AddScoped<IMediaAssetService, MediaAssetService>();
builder.Services.AddScoped<IMediaAssetRepository,MediaAssetRepository>();
builder.Services.AddScoped<IEmailSenderService, EmailSenderService>();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// 模拟数据库迁移和数据填充
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

    await DataSeeder.SeedAsync(context, userManager, roleManager);
}

app.UseCors("AllowReactApp");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();