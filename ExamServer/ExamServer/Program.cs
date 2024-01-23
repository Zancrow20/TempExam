using Domain.Entities;
using ExamServer.ApplicationServicesExtensions;
using ExamServer.Endpoints;
using ExamServer.HostedServices;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
//builder.Services.AddHostedService<DbContextMigration>();

builder.Services
    .AddApplicationDb(builder.Configuration.GetConnectionString("DefaultConnection"))
    .AddJWTAuthorization(config)
    .AddMediatr();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapIdentityApi<User>();

UserEndpoints.Map(app);

app.UseHttpsRedirection();

app.Run();
