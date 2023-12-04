namespace Questao1
{
    class ContaBancaria
    {
        public int Conta { get; }
        public string Titular { get; private set; }
        public double Saldo { get; private set; }

        public ContaBancaria(int conta, string titular)
        {
            Conta = conta;
            Titular = titular;
            Saldo = 0.0;
        }

        public ContaBancaria(int conta, string titular, double depositoInicial)
        {
            Conta = conta;
            Titular = titular;
            Saldo = depositoInicial;
        }

        public void Deposito(double valor)
        {
            Saldo += valor;
        }

        public void Saque(double valor)
        {
            Saldo -= valor + 3.5;
        }

        public override string ToString()
        {
            return $"Conta {Conta}, Titular: {Titular}, Saldo $ {Saldo:N}";
        }
    }
}