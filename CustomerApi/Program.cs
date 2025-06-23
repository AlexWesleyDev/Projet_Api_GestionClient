using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using CustomerApi.Data;
using CustomerApi.SERVICES;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services
    .AddCors(options => { 
        options.AddDefaultPolicy ( policy => { policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); } ) ; 
    })
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddDbContext<CustomerContext>(options => options.UseSqlite("Data Source=Customer.db"))
    .AddScoped<ICustomerService, CustomerService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CustomerContext>();
    dbContext.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app
        .UseSwagger()
        .UseSwaggerUI();
}

app
    .UseHttpsRedirection()
    .UseCors()
    .UseAuthorization();

app.MapControllers();
app.Run();