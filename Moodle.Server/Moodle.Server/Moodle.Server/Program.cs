global using Moodle.Server.Models;
global using Moodle.Server.Data;
global using Moodle.Server.Services;
global using Microsoft.EntityFrameworkCore;
global using Moodle.Server.Services.CourseService;
global using Moodle.Server.Services.DegreeService;
global using Moodle.Server.Services.UserService;
global using Moodle.Server.Services.AuthService;
global using AutoMapper;
global using Moodle.Server.Models.Entities;
global using Moodle.Server.Models.Dtos;
global using Moodle.Server.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<DataContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IDegreeService, DegreeService>();
builder.Services.AddScoped<IUserService, UserService>();

//builder.Services.AddAuthentication().AddJwtBearer();

var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add CORS policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthorization();
app.MapControllers();
app.Run();