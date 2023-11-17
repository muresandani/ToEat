using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToEat.Data;
using ToEat.Services;
using ToEat.Strategies;
using ToEat.Functions;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ToEatContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ToEatContext") ?? throw new InvalidOperationException("Connection string 'ToEatContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IResponseHandlingStrategy, FunctionCallStrategy>();
builder.Services.AddScoped<IResponseHandlingStrategy, SimpleAnswerStrategy>();
builder.Services.AddScoped<ResponseHandlingService, ResponseHandlingService>();
builder.Services.AddScoped<ChatCompletionService>();
builder.Services.AddScoped<IFunction, AddInventoryItemFunction>();
builder.Services.AddScoped<FunctionRepository>();
builder.Services.AddScoped<ConversationService>();
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
