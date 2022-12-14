using BoardComponent;
using DbComponent;
using DbComponent.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ScrumBoardContext>(e => e.UseSqlServer("Server=localhost;Database=ScrumBoard;Trusted_Connection=True;"));
builder.Services.AddScoped<IDbservice, DbService>(sp =>
{
	var ctx = sp.GetService<ScrumBoardContext>();
	return new DbService(ctx);
});
builder.Services.AddScoped<IBoardService, BoardService>();
var app = builder.Build();

CreateDbIfNotExists(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors(e => e.AllowAnyOrigin().AllowAnyHeader());

app.MapControllers();

app.Run();

static void InitializeDb(ScrumBoardContext context)
{
    context.Database.EnsureCreated();
}

//Created DB with EF if DB doesn't already exist.
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