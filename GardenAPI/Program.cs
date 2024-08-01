using GardenAPI.DAL;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddScoped<VentaDAL>();
builder.Services.AddScoped<UsuarioDAL>(); // Registrar UsuarioDAL

// Configurar CORS para permitir solicitudes desde el origen de tu frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder
                .AllowAnyOrigin() // Puedes restringir esto a orígenes específicos
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging(); // Registrar el servicio de logging

var app = builder.Build();

// Configure the app
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Aplicar la política de CORS
app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
