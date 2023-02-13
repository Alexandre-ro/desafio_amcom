using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Services;
using System.Globalization;
using System.Net;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContaCorrenteController : ControllerBase
    {
        private IContaCorrenteService _service;
        private IMovimentoService _movimentoService;
        private IIdempotenciaService  _idempontenciaService;

        public ContaCorrenteController(IContaCorrenteService service, IMovimentoService movimentoService, IIdempotenciaService idempontenciaService)
        {
            _service = service;
            _movimentoService = movimentoService;
            _idempontenciaService = idempontenciaService;
        }

        // POST api/contacorrente
        /// <summary>
        /// Movimenta a Conta Corrente - Debitando ou Creditando um valor
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST /api/contacorrente
        ///     {
        ///        "numeroconta": 123,
        ///        "valor": "500",
        ///        "movimento": "C"
        ///     }
        ///
        /// </remarks>
        /// <param name="value"></param>
        /// <returns>Um novo item criado</returns>
        /// <response code="200">Retorna o ID da Movimentação.</response>
        /// <response code="400">Se a movimentação não foi executada com sucesso, devido há algum parâmetro incorreto.</response>    
        /// <response code="500">Se a ocorrer algum erro interno no servidor.</response> 
        [HttpPost]
        public IActionResult Moviment(int numeroConta, decimal valor, string movimento)
        {
            try
            {
                ContaCorrente conta;

                if (valor <= 0)
                {
                    return BadRequest($" {ErrorEnumcs.INVALID_VALUE}. O Valor informado precisa ser acima de Zero(0).");
                }

                if (movimento != "C" && movimento != "D")
                {
                    return BadRequest($"{ErrorEnumcs.INVALID_TYPE.ToString()}. O Movimento informado é inválido! C (Crédito) ou D (Débito) são aceitos");
                }

                conta = _service.AccountIsExists(numeroConta);

                if (conta == null)
                {
                    return BadRequest($"{ErrorEnumcs.INVALID_ACCOUNT}. A Conta {numeroConta}, NÃO EXISTE!");
                }

                conta = _service.AccountIsActive(numeroConta);

                if (conta == null)
                {
                    return BadRequest($"{ErrorEnumcs.INVALID_ACCOUNT}. A Conta informada: {numeroConta}, está DESATIVADA!");
                }

                Movimento mov = new Movimento
                {
                    IdMovimento = Guid.NewGuid().ToString(),
                    IdContaCorrente = conta.Numero,
                    DataMovimento = DateTime.Now.ToString("dd/MM/yyyy"),
                    TipoMovimento = movimento,
                    Valor = valor
                };

                int retorno = _movimentoService.SaveMoviment(mov);

                if (retorno <= 0)
                {
                    return StatusCode(500, "Ocorreu um erro ao Registrar o Movimento. Tente novamente mais tarde.");
                }

                return Ok(mov.IdMovimento);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro interno no Servidor. Tente novamente mais tarde. {ex.Message}");
            }
        }

        // GET api/contacorrente
        /// <summary>
        /// Executa uma consulta na movimentação da Conta Corrente informada, deve se informar o Número da Conta
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     GET /api/contacorrente
        ///     {
        ///        "numeroconta": 123               
        ///     }
        ///
        /// </remarks>
        /// <param name="value"></param>
        /// <returns>Um novo item criado</returns>
        /// <response code="200">Retorna se a movimentação da Conta informada for encontrada.</response>
        /// <response code="400">Se a movimentação não foi encontrada, devido algum parâmetro incorreto.</response>    
        /// <response code="500">Se a ocorrer algum erro interno no servidor.</response> 
        [HttpGet("{numeroConta}")]
        public IActionResult GetBalance(int numeroConta)
        {
            try
            {
                ContaCorrente conta;

                conta = _service.AccountIsExists(numeroConta);

                if (conta == null)
                {
                    return BadRequest($"{ErrorEnumcs.INVALID_ACCOUNT}. A Conta, NÃO EXISTE!");
                }

                conta = _service.AccountIsActive(numeroConta);

                if (conta == null)
                {
                    return BadRequest($"{ErrorEnumcs.INVALID_ACCOUNT}. A Conta informada, está DESATIVADA!");
                }

                decimal saldo = _movimentoService.GetBalance(numeroConta);

                return Ok("Número da Conta: " + numeroConta + "\r\n"
                + "Titular: " + conta.Nome + "\r\n"
                + "Data/Hora:   " + DateTime.Now.ToString() + "\r\n"
                + "Saldo Atual: " + saldo.ToString("C", CultureInfo.CurrentCulture));

            }
            catch (Exception ex)
            {
                Idempotencia idempotencia = new Idempotencia {
                    ChaveIdempotencia = Guid.NewGuid().ToString(),
                    Requisicao = "GET",
                    Resultado = $"500, {ex.Message}"
                };

                _idempontenciaService.Save(idempotencia);

                return StatusCode(500, $"Ocorreu um erro interno no Servidor. Tente novamente mais tarde. {ex.Message}");
            }
        }
    }
}
