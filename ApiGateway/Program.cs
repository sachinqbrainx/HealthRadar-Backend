using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);


builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();


// Add DI to services


builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors((setup) =>
{
    setup.AddPolicy("default", (options) =>
    {
        options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    });
});

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseCors("default");

app.UseAuthorization();

app.MapControllers();
await app.UseOcelot();

app.Run();
