using Core.Models.Options;
using Quimica.Core.Bussiness;
using Quimica.Core.DataAccess;
using Quimica.Service.Business;
using Quimica.Service.DataAccess;
using Microsoft.OpenApi.Models;
using Quimica.Core.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c => {

    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Api Quimica"
    });
});

// Configuración de DbOptions desde appsettings.json
builder.Services.Configure<DbOptions>(builder.Configuration.GetSection("DataBase"));

// Registra IConnectionBuilder (ya resuelve IOptions<DbOptions> internamente)
builder.Services.AddTransient<IConnectionBuilder, ConnectionBuilder>();

// Registra el repositorio genérico (ahora depende de IConnectionBuilder)
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
// En Program.cs
builder.Services.AddTransient<IGenericRepository<Shipment>, GenericRepository<Shipment>>();
builder.Services.AddTransient<IGenericRepository<Address>, GenericRepository<Address>>();
builder.Services.AddTransient<IGenericRepository<shipments_products>, GenericRepository<shipments_products>>();

builder.Services.AddTransient<IConnectionBuilder, ConnectionBuilder>();
builder.Services.AddTransient<IFormulaService, FormulaService>();
builder.Services.AddTransient<IFormulaRepository, FormulaRepository>();
builder.Services.AddTransient<IShipmentService,ShipmentService>();
builder.Services.AddTransient<IShipmentRepository,ShipmentRepository>();
builder.Services.AddTransient<IproductRepository, ProductRepository>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<ISummaryShipmentsService, SummaryShipmentsService>();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOriginPolicy",
        builder =>
        {
           
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.Configure<DbOptions>(builder.Configuration.GetSection("DataBase"));
var app = builder.Build();




// Configure the HTTP request pipeline.

app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowAnyOriginPolicy");
    


app.UseAuthorization();

app.MapControllers();

app.Run();
