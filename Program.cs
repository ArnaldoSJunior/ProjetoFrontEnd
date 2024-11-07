using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
       
        policy.WithOrigins("http://localhost:3000") 
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


builder.Services.AddDbContext<AppDataBase>();


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
