using bookcatalog.Data;
using bookcatalog.Services.AuthorService;
using bookcatalog.Services.BookService;
using bookcatalog.Services.SubjectService;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// DataContext Connect to SqlServer Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options  => options.UseSqlServer(connectionString));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:5005")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

QuestPDF.Settings.License = LicenseType.Community;

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

//AutoMapper Service
builder.Services.AddAutoMapper(typeof(Program).Assembly);

//All Scoped
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowFrontend");

//app.UseHttpsRedirection();

app.UseRouting();

app.MapControllers();

app.Run();
