using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.Repository;

namespace Questao5.Domain.Services
{
    public class IdempotenciaService : IIdempotenciaService
    {
        private IIdempotenciaRepository _repository;

        public IdempotenciaService(IIdempotenciaRepository repository)
        {
            _repository = repository;
        }    
        public void Save(Idempotencia idempotencia)
        {
            _repository.Save(idempotencia);
        }
    }
}
