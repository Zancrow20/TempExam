using ExamServer.ApplicationServicesExtensions;
using ExamServer.Endpoints;
using ExamServer.HostedServices;

const string MyPolicy = "MyPolicy";
var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddHostedService<DbContextMigration>();
builder.Services
    .AddApplicationDb(builder.Configuration.GetConnectionString("DefaultConnection"))
    .AddJWTAuthorization(config)
    .AddMediatr()
    .AddSwagger()
    .AddCors(options => options.AddPolicy(MyPolicy, pb 
        => pb.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
        ));

builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseCors(MyPolicy);

app.UseAuthentication();
app.UseAuthorization();

UserEndpoints.Map(app);
AuthorizationEndpoints.Map(app);
RatingEndpoints.Map(app);

app.Run();
