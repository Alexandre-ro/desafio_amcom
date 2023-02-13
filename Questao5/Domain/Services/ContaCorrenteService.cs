using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.Repository;

namespace Questao5.Domain.Services
{
    public class ContaCorrenteService : IContaCorrenteService
    {
        private IContaCorrenteRepository _repository;

        public ContaCorrenteService(IContaCorrenteRepository repository)
        {
            _repository = repository;
        }

        public ContaCorrente AccountIsActive(int numeroConta)
        {
            return _repository.AccountIsActive(numeroConta);
        }

        public ContaCorrente AccountIsExists(int numeroConta)
        {
            return _repository.AccountIsExists(numeroConta);
        }       

        public ContaCorrente FindById(string idConta)
        {
            return _repository.FindById(idConta);
        }
    }
}
