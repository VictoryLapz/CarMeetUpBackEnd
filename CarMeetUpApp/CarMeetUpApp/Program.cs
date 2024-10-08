using CarMeetUpApp;
using CarMeetUpApp.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CarMeetUpDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
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
    options.Authority = "https://dev-5w5l8ake23bk7o4h.us.auth0.com/";  // Your Auth0 domain
    options.Audience = "https://careventapi";  // Your Auth0 API identifier
});

//AUTH ZERO
//This is so in swagger you can put the token in otherwise the endpoint
//would be blocked. Check the static Swagger Extension class for more details
builder.Services.AddCustomSwagger();

var app = builder.Build();

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

app.MapControllers();

app.Run();

