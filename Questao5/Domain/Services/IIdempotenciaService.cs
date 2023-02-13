using Questao5.Domain.Entities;

namespace Questao5.Domain.Services
{
    public interface IIdempotenciaService
    {
        void Save(Idempotencia idempotencia);
    }
}
