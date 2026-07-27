using backend.Datos;
using backend.Servicios;
using Microsoft.EntityFrameworkCore;

// Crea el constructor de la App
var builder = WebApplication.CreateBuilder(args);

// Registrar Controladores
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

// Registrar el DbContext (Entity Framework)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer( // Indica motor de BD a utilizar
        builder.Configuration.GetConnectionString("DefaultConnection")  // busca la coneccion 'DefaultConnection' 
    )
);

// Registrar servicio, calcula los horarios disponibles para un turno
builder.Services.AddScoped<GeneradorHorariosService>();

// habilita documentación automática de la API
builder.Services.AddOpenApi();

var app = builder.Build();

// Configuracion del entorno
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Habilita ruta de los controladores
app.MapControllers();

// Inicia el servidor, ejecuta la App
app.Run();