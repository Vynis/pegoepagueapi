using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApiPegoPaque.DAL;
using WebApiPegoPaque.Models;
using WebApiPegoPaque.Util;

namespace WebApiPegoPaque.Controllers
{
    [Route("api/cliente")]
    public class ClienteController : Controller
    {

        private ClienteDAL dal = new ClienteDAL();
        Autenticacao AutenticaoServico;

        public ClienteController(IHttpContextAccessor context)
        {
            AutenticaoServico = new Autenticacao(context);
        }


        // POST api/values
        [HttpPost]
        [Route("registrarcliente")]
        public ReturnAllService RegistrarCliente([FromBody]Cliente dados)
        {
            ReturnAllService retorno = new ReturnAllService();

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
        public List<Cliente> Listagem()
        {
            return dal.Listagem();
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

            return Ok(cliente);
         
        }

        [HttpDelete]
        [Route("deletar-cliente/{id}")]
        public IActionResult DeletarCliente(int id)
        {
            ReturnAllService retorno = new ReturnAllService();

            if (!dal.DeletarCliente(id))
                return BadRequest("Erro ao deletar cliente");

            retorno.ErroMessage = string.Empty;
            retorno.Result = true;

            return Ok(retorno);

        }


    }
}
