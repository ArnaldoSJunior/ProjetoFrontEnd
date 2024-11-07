using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

public static class ConfigurarRotasDepartamento
{
    public static void MapRotas(WebApplication app)
    {
        app.MapGet("/Departamentos", async (AppDataBase db) =>
            await db.Departamentos.ToListAsync());

        app.MapGet("/Departamentos/{id}", async (int id, AppDataBase db) =>
            await db.Departamentos.FindAsync(id) is Departamento departamento
                ? Results.Ok(departamento)
                : Results.NotFound());

        app.MapPost("/Departamentos", async (Departamento departamento, AppDataBase db) =>
        {
            db.Departamentos.Add(departamento);
            await db.SaveChangesAsync();
            return Results.Created($"/Departamentos/{departamento.Id}", departamento);
        });

        app.MapPut("/Departamentos/{id}", async (int id, Departamento departamentoAlterado, AppDataBase db) =>
        {
            var departamento = await db.Departamentos.FindAsync(id);
            if (departamento is null) return Results.NotFound();

            departamento.nomeDepartamento = departamentoAlterado.nomeDepartamento;
            departamento.descricao = departamentoAlterado.descricao;
           
           

            await db.SaveChangesAsync();

            return Results.NoContent();
        });

        app.MapDelete("/Departamentos/{id}", async (int id, AppDataBase db) =>
        {
            if (await db.Departamentos.FindAsync(id) is Departamento departamento)
            {
                db.Departamentos.Remove(departamento);
                await db.SaveChangesAsync();
                return Results.NoContent();
            }

            return Results.NotFound();
        });
    }
}
