using Questao5.Domain.Entities;

namespace Questao5.Domain.Services
{
    public interface IMovimentoService
    {
        int SaveMoviment(Movimento movimento);
        decimal GetBalance(int idContaCorrente);
    }
}
