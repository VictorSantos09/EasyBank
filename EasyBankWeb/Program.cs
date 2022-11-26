using EasyBankWeb.Crosscutting;
using EasyBankWeb.Entities;
using EasyBankWeb.Repository;
using EasyBankWeb.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<BillRepository>();
builder.Services.AddSingleton<CreditCardRepository>();
builder.Services.AddSingleton<LoanRepository>();
builder.Services.AddSingleton<SavingRepository>();
builder.Services.AddSingleton<UserRepository>();

builder.Services.AddSingleton<Saving>();
builder.Services.AddSingleton<CreditCard>();
builder.Services.AddSingleton<MonthTimer>();


var app = builder.Build();
MonthTimer.Main();

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

