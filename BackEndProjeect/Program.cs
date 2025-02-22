using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowmyWebsite",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddDbContext<LibraryContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowmyWebsite");

app.MapControllers();

SeedDatabase(app);
void SeedDatabase(IHost app)
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<LibraryContext>();

        // Ensure the database is created
        context.Database.EnsureCreated();

        // Seed data
        //if (!context.Authors.Any() && !context.Books.Any())
        {
            var authors = new List<Author>
            {
               
                new Author { FirstName = "Jane", LastName = "Smith", BookId = 2 },
                new Author { FirstName = "Emily", LastName = "Johnson", BookId = 3 }
            };

            var books = new List<Book>
            {
               
                new Book { Title = "Another Book", AuthorId = 2 },
                new Book { Title = "Yet Another Book", AuthorId = 3 }
            };

            context.Authors.AddRange(authors);
            context.Books.AddRange(books);

            context.SaveChanges();
        }
    }
}

app.Run();


//Creation of database with code first approach
//Create classes
//Use EFCore to create database and tables
//Seed data