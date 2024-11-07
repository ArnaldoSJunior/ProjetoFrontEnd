using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

public static class ConfigurarRotasBeneficio
{
    public static void MapRotas(WebApplication app)
    {
        app.MapGet("/Beneficios", async (AppDataBase db) =>
            await db.Beneficios.ToListAsync());

        app.MapGet("/Beneficios/{id}", async (int id, AppDataBase db) =>
            await db.Beneficios.FindAsync(id) is Beneficio beneficio
                ? Results.Ok(beneficio)
                : Results.NotFound());

        app.MapPost("/Beneficios", async (Beneficio beneficio, AppDataBase db) =>
        {
            db.Beneficios.Add(beneficio);
            await db.SaveChangesAsync();
            return Results.Created($"/Beneficios/{beneficio.Id}", beneficio);
        });

        app.MapPut("/Beneficios/{id}", async (int id, Beneficio beneficioAlterado, AppDataBase db) =>
        {
            var beneficio = await db.Beneficios.FindAsync(id);
            if (beneficio is null) return Results.NotFound();

            beneficio.nomeBeneficio = beneficioAlterado.nomeBeneficio;
            beneficio.descricao = beneficioAlterado.descricao;
            beneficio.valor = beneficioAlterado.valor;
           

            await db.SaveChangesAsync();

            return Results.NoContent();
        });

        app.MapDelete("/Beneficios/{id}", async (int id, AppDataBase db) =>
        {
            if (await db.Beneficios.FindAsync(id) is Beneficio beneficio)
            {
                db.Beneficios.Remove(beneficio);
                await db.SaveChangesAsync();
                return Results.NoContent();
            }

            return Results.NotFound();
        });
    }
}
