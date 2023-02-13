using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.Repository
{
    public interface IMovimentoRepository
    {
        int SaveMoviment(Movimento movimento);

        decimal GetBalance(int idContaCorrente);
    }
}
