using CarMeetUpApp;
using CarMeetUpApp.Data;
using CarMeetUpApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container
builder.Services.AddControllers();

builder.Services.AddHttpClient();

// Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<CarMeetUpDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<ApiService>(); // needed  in order to grab the service from the services folder and allow connection

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.WithOrigins("http://localhost:4000", "https://careventmeetup.z13.web.core.windows.net/") //cors will allow local host OR Azure to use back end
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

//AUTH ZERO
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = "https://dev-jm6hvg0klmxse04f.us.auth0.com/";  // Your Auth0 domain
    options.Audience = "https://careventapi";  // Your Auth0 API identifier
});

//AUTH ZERO
//This is so in swagger you can put the token in otherwise the endpoint
//would be blocked. Check the static Swagger Extension class for more details
builder.Services.AddCustomSwagger();

var app = builder.Build(); //add azure stuff here
//this is where we have a self-migration--VV
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CarMeetUpDbContext>();
    dbContext.Database.Migrate();  // Applies any pending migrations to the database
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

//AUTH ZERO 
//make sure use authentication and use authorization are in this EXACT order
app.UseAuthentication();
app.UseAuthorization();
//2nd part of Azure self-migration--VV
app.UseExceptionHandler(errorApp => { errorApp.Run(async context => { var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>(); var exception = exceptionHandlerPathFeature?.Error; if (exception != null) { var logger = context.RequestServices.GetRequiredService<ILogger<Program>>(); logger.LogError(exception, "Unhandled exception occurred."); } context.Response.StatusCode = 500; await context.Response.WriteAsync("An unexpected error occurred. Please try again later."); }); }); //logs any errors to Azure so you can see what messed up where and when

app.MapControllers();

app.Run();

