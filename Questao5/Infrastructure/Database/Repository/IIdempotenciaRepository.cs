using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.Repository
{
    public interface IIdempotenciaRepository
    {
        void Save(Idempotencia idempotencia);
    }
}
