using Dapper;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.Repository
{
    public class IdempotenciaRepository : IIdempotenciaRepository
    {
        public void Save(Idempotencia idempotencia)
        {
            using (var dbConnection = SqlBaseRepository.SimpleDbConnection())
            {
                string query = "insert into idempotencia (chave_idempotencia , " +
                               "requisicao," +
                               "resultado)" +
                               "values(@ChaveIdempotencia," +
                               "@Requisicao," +
                               "@Resultado)";
                dbConnection.Open();
               dbConnection.Execute(query, idempotencia);
            }
        }
    }
}
