namespace Questao5.Domain.Entities;

public class IdemPotencia
{
    public string ChaveIdempotencia { get; private set; }
    public string Requisicao { get; private set; }
    public string Resultado { get; private set; }

    public IdemPotencia()
    {
        ChaveIdempotencia = string.Empty;
        Requisicao = string.Empty;
        Resultado = string.Empty;
    }

    public IdemPotencia(string chaveIdempotencia, string requisicao, string resultado)
    {
        ChaveIdempotencia = chaveIdempotencia;
        Requisicao = requisicao;
        Resultado = resultado;
    }
}