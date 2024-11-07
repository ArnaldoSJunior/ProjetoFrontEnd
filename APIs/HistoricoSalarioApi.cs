using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

public static class ConfigurarHistoricoSalario
{
    public static void MapRotas(WebApplication app)
    {
        app.MapGet("/HistoricoSalarios", async (AppDataBase db) =>
            await db.HistoricoSalarios.ToListAsync());

        app.MapGet("/HistoricoSalarios/{id}", async (int id, AppDataBase db) =>
            await db.HistoricoSalarios.FindAsync(id) is HistoricoSalario historicoSalario
                ? Results.Ok(historicoSalario)
                : Results.NotFound());

        app.MapPost("/HistoricoSalarios", async (HistoricoSalario historicoSalario, AppDataBase db) =>
        {
            db.HistoricoSalarios.Add(historicoSalario);
            await db.SaveChangesAsync();
            return Results.Created($"/HistoricoSalarios/{historicoSalario.Id}", historicoSalario);
        });

        app.MapPut("/HistoricoSalarios/{id}", async (int id, HistoricoSalario historicoSalarioAlterado, AppDataBase db) =>
        {
            var historicoSalario = await db.HistoricoSalarios.FindAsync(id);
            if (historicoSalario is null) return Results.NotFound();

            historicoSalario.funcionarioId = historicoSalarioAlterado.funcionarioId;
            historicoSalario.dataAlteracao = historicoSalarioAlterado.dataAlteracao;
            historicoSalario.salarioAntigo = historicoSalarioAlterado.salarioAntigo;
            historicoSalario.salarioNovo = historicoSalarioAlterado.salarioNovo;
            historicoSalario.motivoAlteracao = historicoSalarioAlterado.motivoAlteracao;
            

            await db.SaveChangesAsync();

            return Results.NoContent();
        });

        app.MapDelete("/HistoricoSalarios/{id}", async (int id, AppDataBase db) =>
        {
            if (await db.HistoricoSalarios.FindAsync(id) is HistoricoSalario historicoSalario)
            {
                db.HistoricoSalarios.Remove(historicoSalario);
                await db.SaveChangesAsync();
                return Results.NoContent();
            }

            return Results.NotFound();
        });
    }
}