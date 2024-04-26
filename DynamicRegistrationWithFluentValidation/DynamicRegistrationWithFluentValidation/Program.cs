using Domain;
using Domain.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//regiter the fluent validation validators
builder.Services.AddFluentValidationValidators();
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

app.UseHttpsRedirection();

app.MapPost("Person",([FromBody] Person person, IValidator<Person> validator) =>
{
    var personValidation = validator.Validate(person);
    if (!personValidation.IsValid)
    {
        return Results.BadRequest(personValidation.ToDictionary());
    }
    return Results.Ok("Person created successfully");
})
    .WithName("CreatePerson")
    .WithOpenApi();

app.Run();


