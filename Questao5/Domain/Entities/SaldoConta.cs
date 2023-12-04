namespace Questao5.Domain.Entities;

public class SaldoConta
{
    public int Numero { get; private set; }
    public string Nome { get; private set; }
    public decimal Saldo { get; private set; }
    public DateTime DataHoraConsulta { get; private set; }

    public SaldoConta()
    {
        Nome = string.Empty;
    }

    public SaldoConta(int numero, string nome, decimal saldo, DateTime dataHoraConsulta)
    {
        Numero = numero;
        Nome = nome;
        Saldo = saldo;
        DataHoraConsulta = dataHoraConsulta;
    }
}