using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

public static class ConfigurarRotasFuncionario
{
    public static void MapRotas(WebApplication app)
    {
        app.MapGet("/Funcionarios", async (AppDataBase db) =>
            await db.Funcionarios.ToListAsync());

        app.MapGet("/Funcionarios/{id}", async (int id, AppDataBase db) =>
            await db.Funcionarios.FindAsync(id) is Funcionario funcionario
                ? Results.Ok(funcionario)
                : Results.NotFound());

        app.MapPost("/Funcionarios", async (Funcionario funcionario, AppDataBase db) =>
        {
            db.Funcionarios.Add(funcionario);
            await db.SaveChangesAsync();
            return Results.Created($"/Funcionarios/{funcionario.Id}", funcionario);
        });

        app.MapPut("/Funcionarios/{id}", async (int id, Funcionario funcionarioAlterado, AppDataBase db) =>
        {
            var funcionario = await db.Funcionarios.FindAsync(id);
            if (funcionario is null) return Results.NotFound();

            funcionario.nome = funcionarioAlterado.nome;
            funcionario.cpf = funcionarioAlterado.cpf;
            funcionario.cargo = funcionarioAlterado.cargo;
            funcionario.dataContratacao = funcionarioAlterado.dataContratacao;
            funcionario.salario = funcionarioAlterado.salario;
            funcionario.endereco = funcionarioAlterado.endereco;
            funcionario.telefone = funcionarioAlterado.telefone;
          

            await db.SaveChangesAsync();

            return Results.NoContent();
        });

        app.MapDelete("/Funcionarios/{id}", async (int id, AppDataBase db) =>
        {
            if (await db.Funcionarios.FindAsync(id) is Funcionario funcionario)
            {
                db.Funcionarios.Remove(funcionario);
                await db.SaveChangesAsync();
                return Results.NoContent();
            }

            return Results.NotFound();
        });
    }
}
