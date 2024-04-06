global using Moodle.Server.Models;
global using Moodle.Server.Data;
global using Moodle.Server.Services;
global using Microsoft.EntityFrameworkCore;
global using Moodle.Server.Services.CourseService;
global using Moodle.Server.Services.DegreeService;
global using AutoMapper;
global using Moodle.Server.Models.Entities;
global using Moodle.Server.Models.Dtos;
global using Moodle.Server.Constants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<DataContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IDegreeService, DegreeService>();

var app = builder.Build();

// Add CORS policy
app.UseCors(policy =>
{
    policy.AllowAnyHeader();
    policy.AllowAnyMethod();
    policy.AllowAnyOrigin();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();