namespace Questao1
{
    public class ContaBancaria
    {
        public int NumeroConta { get; private set; }
        public string NomeTitular { get; set; }
        public double ValorDeposito { get; private set; }
        public double Saldo { get; private set; }

        private double TaxaSaque = 3.50;

        public ContaBancaria(int numeroConta, string nomeTitular, double valorDeposito)
        {
            NumeroConta = numeroConta;
            NomeTitular = nomeTitular;
            ValorDeposito = valorDeposito;

            this.Deposito(valorDeposito);
        }

        public ContaBancaria(int numeroConta, string nomeTitular)
        {
            NumeroConta = numeroConta;
            NomeTitular = nomeTitular;
        }

        public void Deposito(double valorDeposito)
        {
            if (valorDeposito < 0)
            {
                System.Console.WriteLine($"O valor informado de {valorDeposito} é inválido!");
                return;
            }

            this.Saldo += valorDeposito;
        }

        public void Saque(double valorSaque)
        {
            if (valorSaque <= 0)
            {
                System.Console.WriteLine($"O valor informado de {valorSaque} é inválido!");
                return;
            }

            this.Saldo -= valorSaque;
            this.Saldo -= this.TaxaSaque;
        }

        public override string ToString()
        {
            return string.Format($"Conta: {NumeroConta}, Titular: {NomeTitular}, Saldo: {Saldo.ToString("C")}");
        }
    }
}
