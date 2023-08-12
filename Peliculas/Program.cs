using Microsoft.EntityFrameworkCore;
using Peliculas;
using Peliculas.CompiledModels;
using Peliculas.Servicios;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(op => op.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Esta sección configura el servicio del DbContext
var stringConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextFactory<ApplicationDbContext>(opciones =>
{
    opciones.UseSqlServer(stringConnection, sqlServer => sqlServer.UseNetTopologySuite());
    opciones.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);//Ahora esta configurado por defecto en la aplicación
                                                                        //para todos los Query de la aplicación
                                                                        //opciones.UseModel(ApplicationDbContextModel.Instance); //Linea necesaria solo si vamos a usar modelos compilados
}
);

//Necesario solo para usar la configuración del OnConfiguring en el DbContext en lugar de inyección de dependencias 
//builder.Services.AddDbContext<ApplicationDbContext>(); 

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IServicioUsuario, ServicioUsuario>();
builder.Services.AddScoped<IEventosDbContext, EventosDbContext>();
builder.Services.AddSingleton<Singleton>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())//permite crear un contexto por el cual instanciar el DBContext
{
    var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    //applicationDbContext.Database.Migrate();//Ejectuar las migraciones al momento de cargar la aplicación
}
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
