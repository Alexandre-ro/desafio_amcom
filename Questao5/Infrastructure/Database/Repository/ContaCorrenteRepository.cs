using Dapper;
using FluentResults;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.Repository
{
    public class ContaCorrenteRepository : IContaCorrenteRepository
    {
        public ContaCorrente AccountIsActive(int numeroConta)
        {
            int ativo = 1;

            using (var dbConnection = SqlBaseRepository.SimpleDbConnection())
            {

                string sql = $"select idcontacorrente," +
                             $" idcontacorrente," +
                             $" numero," +
                             $" nome," +
                             $" ativo"+
                             $" from contacorrente where numero = @Numero and ativo = {ativo}";

                dbConnection.Open();

                ContaCorrente conta = dbConnection.Query<ContaCorrente>(sql, new { Numero = numeroConta }).FirstOrDefault();
                if (conta == null)
                {
                    return null;
                }

                return conta;
            }
        }

        public ContaCorrente AccountIsExists(int numeroConta)
        {
            using (var dbConnection = SqlBaseRepository.SimpleDbConnection())
            {

                string sql = "select" +
                             " idcontacorrente," +
                             " numero," +
                             " nome," +
                             " ativo" +
                             " from contacorrente where numero = @Numero";

                dbConnection.Open();

                ContaCorrente conta = dbConnection.Query<ContaCorrente>(sql, new { Numero = numeroConta }).FirstOrDefault();
                if (conta == null)
                {
                    return null;
                }

                return conta;
            }
        }

        public ContaCorrente FindById(string idConta)
        {
            using (var dbConnection = SqlBaseRepository.SimpleDbConnection())
            {
                string query = "select idcontacorrente," +
                                       " numero," +
                                       " nome," +
                                       " ativo" +
                                       " from contacorrente" +
                                       " where idcontacorrente = @IdContaCorrente";
                                       

                dbConnection.Open();
                return dbConnection.Query<ContaCorrente>(query, new { IdContaCorrente = idConta }).FirstOrDefault();
            }

        }

        public IEnumerable<ContaCorrente> FindAll()
        {
            using (var dbConnection = SqlBaseRepository.SimpleDbConnection())
            {
                dbConnection.Open();
                return dbConnection.Query<ContaCorrente>("SELECT * FROM contacorrente");
            }
        }


    }
}
