using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

var connectionString = builder.Configuration.GetConnectionString("myconnectionstring");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'myconnectionstring' not found.");
}

builder.Services.AddDbContext<OnlineMoviesContext>(options =>
    options.UseSqlServer(connectionString));

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

/*===========================REGISTER SERVICE===========================================*/
builder.Services.AddScoped<IMovieAuthorRepository<MovieAuthor>, MovieAuthorRepository<MovieAuthor>>();
builder.Services.AddScoped<MovieAuthorService>();

builder.Services.AddScoped<IMoviesListRepository<MoviesList>, MoviesListRepository<MoviesList>>();
builder.Services.AddScoped<MoviesListService>();

builder.Services.AddScoped<IMovieRepository<Movie>, MovieRepository<Movie>>();
builder.Services.AddScoped<MovieService>();

builder.Services.AddScoped<ICommentRepository<Comment>, CommentRepository<Comment>>();
builder.Services.AddScoped<CommentService>();

builder.Services.AddScoped<IAccountRepository<AccountUser>, AccountRepository<AccountUser>>();
builder.Services.AddScoped<AccountService>();

builder.Services.AddScoped<IInforAccountRepository<InforAccountUser>, InforAccountRepository<InforAccountUser>>();
builder.Services.AddScoped<InforAccountService>();

builder.Services.AddScoped<IMoviesUserOwnedRepository<MoviesUserOwned>, MoviesUserOwnedRepository<MoviesUserOwned>>();
builder.Services.AddScoped<MoviesUserOwnedService>();

builder.Services.AddScoped<IMovieCategoryRepository<MovieCategory>, MovieCategoryRepository<MovieCategory>>();
builder.Services.AddScoped<MovieCategoryService>();

builder.Services.AddScoped<IMovieBillRepository<MoviesBill>, MovieBillRepository<MoviesBill>>();
builder.Services.AddScoped<MovieBillService>();
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

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
