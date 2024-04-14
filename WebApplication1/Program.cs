using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext <OnlineMoviesContext>(x =>
x.UseSqlServer(builder.Configuration.GetConnectionString("myconnectionstring")));

builder.Services.AddControllers();

/*==================Thi?t l?p CORS cho phép call api t? ngu?n khác===========================*/
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});
builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });
/*===========================REGISTER SERVICE===========================================*/
builder.Services.AddScoped<IRepository<MovieAuthor>, Repository<MovieAuthor>>();
builder.Services.AddScoped<MovieAuthorService>();

builder.Services.AddScoped<IRepository<MoviesList>, Repository<MoviesList>>();
builder.Services.AddScoped<MoviesListService>();

builder.Services.AddScoped<IRepository<Movie>, Repository<Movie>>();
builder.Services.AddScoped<MovieService>();
/*==========================REGISTER SERVICE============================================*/

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
/*==================Thi?t l?p CORS cho phép call api t? ngu?n khác===========================*/
app.UseCors("AllowOrigin");
app.UseStaticFiles();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
/*===================================================================*/
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
