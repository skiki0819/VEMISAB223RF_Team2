global using Moodle.Server.Models;
global using Moodle.Server.Data;
global using Moodle.Server.Services;
global using Microsoft.EntityFrameworkCore;
global using Moodle.Server.Services.CourseService;
global using Moodle.Server.Services.DegreeService;
global using Moodle.Server.Services.UserService;
global using Moodle.Server.Services.AuthService;
global using Moodle.Server.Services.RoleService;
global using Moodle.Server.Services.EventService;
global using AutoMapper;
global using Moodle.Server.Models.Entities;
global using Moodle.Server.Models.Dtos;
global using Moodle.Server.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Moodle.Server.Services.Init;
using Moodle.Server.WebSocket;
using Moodle.Server.WebSocket.Handlers;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( options => { 
    options.AddSecurityDefinition(name:JwtBearerDefaults.AuthenticationScheme,securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            new string[] {}
        }
    });
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<DataContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<RoleInit>();
builder.Services.AddScoped<CourseInit>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IDegreeService, DegreeService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddWebSocketManager();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<DataContext>();
    ctx.Database.Migrate();
    scope.ServiceProvider.GetRequiredService<CourseInit>().Init().Wait();
    scope.ServiceProvider.GetRequiredService<RoleInit>().Init().Wait();
}

app.UseHttpsRedirection();

// Add CORS policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

var serviceScopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
var serviceProvider = serviceScopeFactory.CreateScope().ServiceProvider;
app.UseWebSockets();
app.MapWebSocketManager("/api/event/ws", serviceProvider.GetService<EventsHandler>());

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();