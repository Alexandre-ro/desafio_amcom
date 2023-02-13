using Dapper;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.Repository
{
    public class MovimentoRepository : IMovimentoRepository
    {
        public int SaveMoviment(Movimento movimento)
        {
            using (var dbConnection = SqlBaseRepository.SimpleDbConnection())
            {
                string query = "insert into movimento (idmovimento, " +
                               "idcontacorrente," +
                               "datamovimento," +
                               "tipomovimento," +
                               "valor)" +
                               "values(@IdMovimento," +
                               "@IdContaCorrente," +
                               "@DataMovimento," +
                               "@TipoMovimento," +
                               "@Valor)";

                dbConnection.Open();

                return dbConnection.Execute(query, movimento);
            }
        }

        public decimal GetBalance(int idContaCorrente)
        {
            string credito = "C";
            string debito  = "D";           
            decimal valorCredito;
            decimal valorDebito;
            decimal saldo;

            var parametrosCredito = new { Tipo = credito, IdContaCorrente = idContaCorrente };
            var parametrosDebito = new { Tipo = debito, IdContaCorrente = idContaCorrente };


            using (var dbConnection = SqlBaseRepository.SimpleDbConnection())
            {
                string query = "SELECT SUM(valor)" +
                               " FROM movimento WHERE tipomovimento = @Tipo" +
                               " AND idcontacorrente = @IdContaCorrente";

                var sum = dbConnection.ExecuteScalar(query, parametrosCredito);

                valorCredito = Convert.ToDecimal(sum);                
            }

            using (var dbConnection = SqlBaseRepository.SimpleDbConnection())
            {
                string query = "SELECT SUM(valor)" +
                               " FROM movimento WHERE tipomovimento = @Tipo" +
                               " AND idcontacorrente = @IdContaCorrente";

                var sum = dbConnection.ExecuteScalar(query, parametrosDebito);

                valorDebito = Convert.ToDecimal(sum);
            }

            saldo = valorCredito - valorDebito;           

            return saldo;
        }       
    }
}
