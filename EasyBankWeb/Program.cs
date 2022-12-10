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
builder.Services.AddSingleton<AutoDebitRepository>();

builder.Services.AddSingleton<Saving>();
builder.Services.AddSingleton<Loan>();
builder.Services.AddSingleton<Bill>();
builder.Services.AddSingleton<Transfer>();
builder.Services.AddSingleton<CreditCard>();
builder.Services.AddSingleton<MonthTimer>();
builder.Services.AddSingleton<SafetyPassword>();
builder.Services.AddSingleton<CancelAccountService>();
builder.Services.AddSingleton<Register>();
builder.Services.AddSingleton<Profile>();

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("corsapp");

app.MapControllers();


app.Run();



//var service = builder.Services.sGetService(typeof(IFooService));
