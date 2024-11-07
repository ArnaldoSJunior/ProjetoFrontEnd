public class HistoricoSalario
{
    public int Id { get; set; }
    public int funcionarioId { get; set; }
    public DateTime dataAlteracao { get; set; }
    public double salarioAntigo { get; set; }
    public double salarioNovo { get; set; }
    public string? motivoAlteracao { get; set; }

   
}
