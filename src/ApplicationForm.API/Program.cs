using ApplicationForm.Domain.Interfaces;
using Microsoft.Azure.Cosmos;
using ApplicationForm.Infrastructure.Implementation;
using ApplicationForm.Application.Services;
using FluentValidation.AspNetCore;
using ApplicationForm.API.Validation;
using FluentValidation;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<QuestionDtoValidator>();
builder.Services.AddAuthorization();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Application Form API", Version = "v1" });
});

var cosmosDbConnectionString = builder.Configuration["CosmosDb:ConnectionString"];
var cosmosClient = new CosmosClient(cosmosDbConnectionString);
builder.Services.AddSingleton(cosmosClient);

builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IApplicationFormService, ApplicationFormService>();
builder.Services.AddScoped<IApplicationFormRepository, ApplicationFormRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
