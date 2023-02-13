using FluentResults;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.Repository
{
    public interface IContaCorrenteRepository
    {
        IEnumerable<ContaCorrente> FindAll();
        ContaCorrente AccountIsExists(int numeroConta);
        ContaCorrente AccountIsActive(int numeroConta);
        ContaCorrente FindById(string idConta);
    }
}
