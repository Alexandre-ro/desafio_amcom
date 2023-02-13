using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.Repository;

namespace Questao5.Domain.Services
{
    public class MovimentoService : IMovimentoService
    {
        private IMovimentoRepository _repository;

        public MovimentoService(IMovimentoRepository repository)
        {
            _repository = repository;     
        }       

        public int SaveMoviment(Movimento movimento)
        {
            return _repository.SaveMoviment(movimento);
        }

        public decimal GetBalance(int idContaCorrente)
        {
            return _repository.GetBalance(idContaCorrente);
        }
    }
}
