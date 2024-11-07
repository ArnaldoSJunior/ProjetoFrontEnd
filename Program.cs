using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDataBase>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:3000")  // Permite acesso do frontend em http://localhost:3000
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


app.MapGet("/", () => "API");


ConfigurarRotasCargo.MapRotas(app);
ConfigurarRotasFuncionario.MapRotas(app);
ConfigurarRotasBeneficio.MapRotas(app);
ConfigurarRotasDepartamento.MapRotas(app);
ConfigurarHistoricoSalario.MapRotas(app);

app.UseCors("AllowLocalhost");

app.Run();
