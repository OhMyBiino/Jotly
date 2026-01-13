using Jotly.api.Data;
using Jotly.api.Repositories.NotesRepo;
using Jotly.api.Repositories.UserRepo;
using Jotly.api.Services.AuthService;
using Jotly.api.Services.NoteService;
using Jotly.api.Services.TokenService;
using Jotly.api.Services.UserService;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
