using Questao5.Domain.Entities;

namespace Questao5.Domain.Services
{
    public interface IContaCorrenteService
    {   ContaCorrente AccountIsExists(int numeroConta);
        ContaCorrente AccountIsActive(int numeroConta);
        ContaCorrente FindById(string idConta);
    }
}
