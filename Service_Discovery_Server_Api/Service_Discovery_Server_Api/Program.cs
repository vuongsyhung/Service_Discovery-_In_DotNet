using Microsoft.EntityFrameworkCore;
using Service_Discovery_Server_Api.Models;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<HungDbContext>(db => db.UseSqlServer(builder.Configuration.GetConnectionString("HungConnectionString")));




var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapCustomerEndpoints();


app.Run();
