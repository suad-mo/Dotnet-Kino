using Kino.Infrastructure.Database;
using Kino.Repositories.KategorijaArtikl;
using Kino.Services.KategorijaArtikl;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IKategorijaArtiklService, KategorijaArtiklService>();
builder.Services.AddScoped<IKategorijaArtiklRepository, KategorijaArtiklRepository>();//Ovdje prikazuje grešku

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        x => x.MigrationsAssembly("Kino.Infrastructure"))); // Ensure 'Microsoft.EntityFrameworkCore.SqlServer' package is installed

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
