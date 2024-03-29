using Application.Activities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
}
);

// Add service support for CORS requests as long as they come from our client
builder.Services.AddCors(opt =>
 {
    opt.AddPolicy("CorsPolicy", policy => 
    {
        // Allow CORS requests with any method and any header as long as the request is coming from our client
        policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000");
    }
    );
 });

builder.Services.AddMediatR(typeof(List.Handler));

var app = builder.Build();

// Configure the HTTP request pipeline. This is Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// Add the CORS policy middleware
app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

// 'using' means the variable will cleaned from memory after the program finished with him. It's like activating the garbage collector directly
// 'CreateScope' means that the data conetext will be available, only for the duration of the http request session
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
// Try to create the Database
try {

    // Get our dbcontext
    var context = services.GetRequiredService<DataContext>();
    // Run any pending migrations and if the data base doesn't exist, create it
    await context.Database.MigrateAsync();
    // Development enviroment only - Seed the database if it's empty
    await Seed.SeedData(context);

} catch (Exception ex) {

    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error ocurred during migrations");

}


app.Run();
