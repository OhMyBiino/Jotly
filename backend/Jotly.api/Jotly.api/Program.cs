using Jotly.api.Data;
using Jotly.api.Repositories.NotesRepo;
using Jotly.api.Repositories.UserRepo;
using Jotly.api.Services.NoteService;
using Jotly.api.Services.UserService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

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
