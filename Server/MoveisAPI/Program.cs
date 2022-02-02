using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using MoveisAPI;
using MoveisAPI.Filters;
using ServicesP.Services;

using ServicesP.Interface;
using ServicesP.Implementation.Interface;
using ServicesP.Implementation.Services;
using MoveisAPI.Helpers;
using NetTopologySuite.Geometries;
using NetTopologySuite;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(pom => 
    {
        var frontendURL = builder.Configuration.GetValue<string>("frontend_url");
        pom.WithOrigins(frontendURL).AllowAnyMethod().AllowAnyHeader()
        .WithExposedHeaders(new string[] {"totalAmountOfRecords"});  
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")/*,sqlOptions => sqlOptions.UseNetTopologySuite()*/);
});
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["keyjwt"])),
        ClockSkew = TimeSpan.Zero

    };
});
builder.Services.AddAuthorization(options => 
    {
        options.AddPolicy("IsAdmin", policy => policy.RequireClaim("role", "admin"));
    });

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();



builder.Services.AddAutoMapper(typeof(Program));
//builder.Services.AddSingleton(provider => new MapperConfiguration(config =>
//    {
//        var geometryFactory = provider.GetRequiredService<GeometryFactory>();
//        config.AddProfile(new AutoMapperProfiles(geometryFactory));
//    }).CreateMapper());
//builder.Services.AddSingleton<GeometryFactory>(NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326)); //ovo radi sa instancama iz planet eartha



builder.Services.AddControllers(options => { options.Filters.Add(typeof(MyExceptionFilter)); });
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IFileStorageService, InAppStorageService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IActorService, ActorService>();
builder.Services.AddScoped<IMovieTheaterService, MovieTheaterService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddScoped<IUserService, UserService>();


var app = builder.Build();

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors();

app.UseAuthorization();

app.MapControllers();


app.Run();
