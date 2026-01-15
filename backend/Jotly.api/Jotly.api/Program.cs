using Jotly.api.Data;
using Jotly.api.Repositories.NotesRepo;
using Jotly.api.Repositories.UserRepo;
using Jotly.api.Services.AuthService;
using Jotly.api.Services.NoteService;
using Jotly.api.Services.TokenService;
using Jotly.api.Services.UserService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Swagger Config
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
       In = ParameterLocation.Header,
       Name = "Authorization",
       Type = SecuritySchemeType.Http,
       Scheme = "bearer",
       BearerFormat = "JWT",
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
            new List<string>()
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => 
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]!)),
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            ClockSkew = TimeSpan.Zero

        };
    
    });
builder.Services.AddAuthorization();

//AppDbContext Connection
builder.Services.AddDbContextFactory<AppDbContext>( options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

//Register Notes Service-Repository
builder.Services.AddScoped<INotesRepository, NotesRepository>();
builder.Services.AddScoped<INoteService, NoteService>();

//Register User Service-Repository
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

//Register AuthService
builder.Services.AddScoped<IAuthService, AuthService>();

//Register Token Service
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
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
