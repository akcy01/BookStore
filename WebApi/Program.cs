using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using System.Reflection;
using WebApi.Middlewares;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddDbContext<BookStoreDbContext>(Options => Options.UseInMemoryDatabase(databaseName: "BookStoreDB"));
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton<ILoggerService,ConsoleLogger>(); //Yazdığımız servisi buraya ekledik.Iloggerservice çağrıdlığında consolelogger çalışsın. console da çalışsın istersen consoloeloggerı dbde yazsın istiyorsan dbloggeri sağdaki kısma yazmalısın.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

using (var scope = app.Services.CreateScope()) { var services = scope.ServiceProvider;DataGenerator.Initialize(services); } //Initialize metodu datayı insert etmeye yarar.

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCustomExceptionMiddle();

app.MapControllers();

app.Run();
