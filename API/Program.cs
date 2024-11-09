using Autofac;
using Autofac.Extensions.DependencyInjection;
using API.Profiles;
using API.Cores;
using API.Docs;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;

using System.Reflection;

using AspNetCoreRateLimit;
using ITBClub.API.Repositories;
using Microsoft.Extensions.Configuration;
using API.Repositories.API.Repositories;

var builder = WebApplication.CreateBuilder(args);


var isDevEnv = false;

// Add services to the container.
Config? config = builder.Configuration.GetSection("Config").Get<Config>();
builder.Services.AddSingleton(config);

builder.Services.AddDbContext<DbContext, TextMiningDbContext>(options =>
{
    options
        .UseSqlServer(builder.Configuration.GetConnectionString("LLL"), o => o.EnableRetryOnFailure(2));
    //.UseSqlServer(builder.Configuration.GetConnectionString(connectionString));
});
// Other service registrations

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());



// Register Packages
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<AccountRepository>().As<IAccountRepository>();
    containerBuilder.RegisterType<ScopeRepository>().As<IScopeRepository>();
    containerBuilder.RegisterType<AuthenticationRepository>().As<IAuthenticationRepository>();
    containerBuilder.RegisterType<RefreshTokenRepository>().As<IRefreshTokenRepository>();
    containerBuilder.RegisterType<UserRepository>().As<IUserRepository>();
    containerBuilder.RegisterType<SubCategoryRepository>().As<ISubCategoryRepository>();
    containerBuilder.RegisterType<CategoryRepository>().As<ICategoryRepository>();
    containerBuilder.RegisterType<BrandRepository>().As<IBrandRepository>();
    containerBuilder.RegisterType<ProductRepository>().As<IProductRepository>();
    containerBuilder.RegisterType<ProductColorRepository>().As<IProductColorRepository>();
    containerBuilder.RegisterType<ProductImageRepository>().As<IProductImageRepository>();
    containerBuilder.RegisterType<ProductPriceHistoryRepository>().As<IProductPriceHistoryRepository>();
    containerBuilder.RegisterType<ReviewRepository>().As<IReviewRepository>();


    // Service
    containerBuilder.RegisterType<AccountService>().As<IAccountService>();
    containerBuilder.RegisterType<GoogleAuthService>().As<IGoogleAuthService>();
    containerBuilder.RegisterType<SmtpService>().As<ISmtpService>();
    containerBuilder.RegisterType<TokenService>().As<ITokenService>();
    containerBuilder.RegisterType<TokenGenerateService>().As<ITokenGenerateService>();
    containerBuilder.RegisterType<AuthenticationService>().As<IAuthenticationService>();
    containerBuilder.RegisterType<AccountService>().As<IAccountService>();
    containerBuilder.RegisterType<UserService>().As<IUserService>();
    containerBuilder.RegisterType<SubCategoryService>().As<ISubCategoryService>();
    containerBuilder.RegisterType<CategoryService>().As<ICategoryService>();
    containerBuilder.RegisterType<BrandService>().As<IBrandService>();
    containerBuilder.RegisterType<ProductService>().As<IProductService>();
    containerBuilder.RegisterType<ReviewService>().As<IReviewService>();
    // Register other dependencies here


    // Profile
    containerBuilder.Register(context => new MapperConfiguration(cfg =>
    {
       
        cfg.AddProfile<RefreshTokenProfile>();
        cfg.AddProfile<AccountProfile>();
        cfg.AddProfile<AuthenticationProfile>();
        cfg.AddProfile<UserProfile>();
        cfg.AddProfile<SubCategoryProfile>();
        cfg.AddProfile<CategoryProfile>();
        cfg.AddProfile<BrandProfile>();
        cfg.AddProfile<ProductProfile>();
        cfg.AddProfile<ReviewProfile>();

    }).CreateMapper()).As<IMapper>().InstancePerLifetimeScope();
});


//Config filter
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services
    .AddControllers(options =>
    {
        options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
        options.Filters.Add<HttpResponseExceptionFilter>();
    })
    .AddJsonOptions(ResponseCore.JsonOptions);

builder.Services.AddOptions()
    .AddMemoryCache();

builder.Services
    .Configure<IpRateLimitOptions>(RateLimitCore.Options);

builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>()
    .AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>()
    .AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>()
    .AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>()
    .AddInMemoryRateLimiting();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(OpenAPI.Version.ToString(), OpenAPI.Info);
    options.AddSecurityDefinition(OpenAPI.SecurityScheme.Scheme, OpenAPI.SecurityScheme);
    options.AddSecurityRequirement(OpenAPI.SecurityRequirement);
    options.AutoMapping();
    options.DocumentFilter<SetHostAndSchemesFilter>();
});

builder.Services.AddHttpContextAccessor();

//Setting security
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
       .AddJwtBearer(AuthenticationCore.Options(config));

WebApplication? app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

if (true)
{
    app.UseSwagger();
    app.UseSwaggerUI(options => options.SwaggerEndpoint($"{OpenAPI.Version}/swagger.json", "LiveLaughLove API"));
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors();
app.UseStatusCodePages();
app.UseAuthentication();
app.UseAuthorization();
app.UseIpRateLimiting();
app.MapControllers();
app.Run();