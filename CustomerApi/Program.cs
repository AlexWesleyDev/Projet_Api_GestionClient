using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using CustomerApi.Data;
using CustomerApi.SERVICES;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

//builder.Services.AddOpenApi(); Demander à CHATGPT si c'est important den avoir ou pas
builder.Services.AddControllers();




builder.Services.AddCors(options => 
    { 
        options.AddDefaultPolicy ( policy => { policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); } ) ; 
    });





// Rajout
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CustomerContext>(options => options.UseSqlite("Data Source=Customer.db"));

// Partie Concept SOLID
builder.Services.AddScoped<ICustomerService, CustomerService>();


var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CustomerContext>();
    dbContext.Database.EnsureCreated(); // 👈 crée le fichier si besoin
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    
    // Rajout
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

// PARTIE VIEW pour VOIR et TRAITER les donnés
// Autoriser la connexion depuis un autre port local, notamment pour le Vu.js
app.UseCors();

app.UseAuthorization();
app.MapControllers();
app.Run();

