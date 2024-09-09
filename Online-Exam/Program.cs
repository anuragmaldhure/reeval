using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
//using Online_Exam.Areas.Identity.Data;
using Online_Exam.Data;
using Online_Exam.Models;
using Online_Exam.Repositories.Interfaces;
using Online_Exam.Repositories;
using Online_Exam.Repository;
using Online_Exam.Repository.Interface;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configure the database connection string
var connectionString = builder.Configuration.GetConnectionString("Online_ExamContextConnection")
    ?? throw new InvalidOperationException("Connection string 'Online_ExamContextConnection' not found.");

builder.Services.AddDbContext<Online_ExamContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<Online_ExamUser, IdentityRole>()
    .AddEntityFrameworkStores<Online_ExamContext>()
    .AddDefaultTokenProviders();



// Register repositories for the exam system
builder.Services.AddScoped<IExamRepository, ExamRepository>();
builder.Services.AddScoped<ISectionRepository, SectionRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IOptionRepository, OptionRepository>();
builder.Services.AddScoped<IExamResultRepository, ExamResultRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<ISectionActionRepository, SectionActionRepository>();



// Register AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Add CORS policy to allow requests from Angular app
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});



// Configure JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                builder.Configuration["JwtSettings:Secret"]
                ?? throw new InvalidOperationException("JwtSettings:Secret configuration value not found.")))
        };
    });

// Add services to the container
builder.Services.AddControllers();

// Configure Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger in Development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use HTTPS for requests
app.UseHttpsRedirection();

// Use CORS policy defined earlier
app.UseCors("AllowSpecificOrigin");

// Use Authentication and Authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Map controllers to routes
app.MapControllers();

app.Run();
