using DbComponent.DbModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using ScrumboardApi.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ScrumBoardContext>(e => e.UseSqlServer("Server=localhost;Database=ScrumBoard;Trusted_Connection=True;"));

var app = builder.Build();

CreateDbIfNotExists(app.Services);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<ScrumBoardContext>();
    var states = ctx.States.Where(e => e.Name == "Todo");
    var user = new BoardTask()
    {

    };
    ctx.AddRange(user);
    ctx.SaveChanges();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void InitializeDb(ScrumBoardContext context)
{
    context.Database.EnsureCreated();
}

static void CreateDbIfNotExists(IServiceProvider host)
{
    using (var scope = host.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<ScrumBoardContext>();
            InitializeDb(context);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred creating the DB.");
        }
    }
}