using Questao5.Domain.Enumerators;

namespace Questao5.Domain.Entities;

public class Movimento
{
    public string IdMovimento { get; private set; }
    public string IdContaCorrente { get; private set; }
    public DateTime DataMovimento { get; private set; }
    public EAccountMovementType TipoMovimento { get; private set; }
    public decimal Valor { get; private set; }

    public Movimento()
    {
        IdMovimento = string.Empty;
        IdContaCorrente = string.Empty;
    }

    public Movimento(string idMovimento, string idContaCorrente, DateTime dataMovimento,
        EAccountMovementType tipoMovimento, decimal valor)
    {
        IdMovimento = idMovimento;
        IdContaCorrente = idContaCorrente;
        DataMovimento = dataMovimento;
        TipoMovimento = tipoMovimento;
        Valor = valor;
    }
}