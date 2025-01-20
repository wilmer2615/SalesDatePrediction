using Logic;
using Logic.CustomerLogic;
using Logic.EmployeeLogic;
using Logic.OrderLogic;
using Logic.ProductLogic;
using Logic.ShipperLogic;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Repository.CustomerRepository;
using Repository.Repository.EmployeeRepository;
using Repository.Repository.OrderRepository;
using Repository.Repository.ProductRepository;
using Repository.Repository.ShipperRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configuracion Cors Origin
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWebApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Configurar AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Se realiza la configuracion de la inyección de dependencias.
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IShipperRepository, ShipperRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();


builder.Services.AddScoped<IEmployeeLogic, EmployeeLogic>();
builder.Services.AddScoped<ICustomerLogic, CustomerLogic>();
builder.Services.AddScoped<IOrderLogic, OrderLogic>();
builder.Services.AddScoped<IShipperLogic, ShipperLogic>();
builder.Services.AddScoped<IProductLogic, ProductLogic>();

//Configuracion conexion a base de datos
builder.Services.AddDbContext<AplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

// Habilitar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Usar la política de CORS
app.UseCors("AllowSpecificOrigins");

app.UseAuthorization();

app.UseCors("AllowWebApp");

app.MapControllers();

app.Run();
