using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiPegoPaque.DAL;
using WebApiPegoPaque.Models;
using WebApiPegoPaque.Util;

namespace WebApiPegoPaque.Controllers
{
    [Route("api/cliente")]
    [Authorize()]
    public class ClienteController : Controller
    {

        private ClienteDAL dal = new ClienteDAL();
        private ReturnAllService retorno = new ReturnAllService();

        // POST api/values
        [HttpPost]
        [Route("registrarcliente")]
        public ReturnAllService RegistrarCliente([FromBody]Cliente dados)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    retorno.Result = false;
                    retorno.ErroMessage = "Erro ao tentar validar o cliente: ";
                }
                else
                {
                    dal.RegistrarCliente(dados);
                    retorno.Result = true;
                    retorno.ErroMessage = string.Empty;
                }
            }
            catch (Exception ex)
            {
                retorno.Result = false;
                retorno.ErroMessage = "Erro ao tentar registrar o cliente: " + ex.Message;
            }

            return retorno;
        }

        [HttpGet]
        [Route("listagem")]
        public async Task<List<Cliente>> Listagem()
        {
            try
            {
                var cliente = new List<Cliente>();

                cliente = await dal.Listagem();

                return cliente;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("retorna-cliente/{id}")]
        public IActionResult RetornaCliente(int id)
        {
            if (id <= 0)
                return BadRequest("O código deve ser maior que 0");

            var cliente = dal.RetornaCliente(id);

            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

        [HttpPut]
        [Route("atualizar-cliente/{id}")]
        public IActionResult AtualizarCliente(int id, [FromBody]Cliente cliente)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!dal.AtualizarCliente(id, cliente))
                return NotFound();

            retorno.ErroMessage = string.Empty;
            retorno.Result = true;

            return Ok(cliente);
        }

        [HttpDelete]
        [Route("deletar-cliente/{id}")]
        public IActionResult DeletarCliente(int id)
        {
            if (!dal.DeletarCliente(id))
                return BadRequest("Erro ao deletar cliente");

            retorno.ErroMessage = string.Empty;
            retorno.Result = true;

            return Ok(retorno);

        }


    }
}
