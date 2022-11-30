using Microsoft.EntityFrameworkCore;
using DecisionMakingSurvey.Commands.Handlers;
using DecisionMakingSurvey.Commands.Interface;
using DecisionMakingSurvey.DBContexts;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DecisionMakingConnection");

// Add services to the container.
builder.Services.AddDbContext<DecisionMakingSurveyDBContext>(options =>
   options.UseNpgsql(connectionString));


// Add DI to services



builder.Services.AddScoped<IDecisionMakingSurveyCommand, DecisionMakingSurveyCommand>();

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


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseCors("default");

app.UseAuthorization();

app.MapControllers();

app.Run();
