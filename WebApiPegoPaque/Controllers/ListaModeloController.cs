using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using WebApiPegoPaque.Models;
using WebApiPegoPaque.Util;

namespace WebApiPegoPaque.Controllers
{
    [Route("api/listaModelos")]
    public class ListaModeloController : Controller
    {

        private ReturnAllService retorno = new ReturnAllService();
        private DataContext db = new DataContext();

        [HttpGet]
        [Route("buscar-lista-modelo/{id}")]
        public IActionResult BuscaListaModeloUsuario(int id)
        {
            try
            {
                var listaModelo = db.DbListasModelo.ToList().Where(a => a.UsuId == id).OrderByDescending(b => b.DtCadastro);

                return Ok(listaModelo);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("seleciona-lista-modelo")]
        public IActionResult SelecionaListaModeloUsuario(int id)
        {
            try
            {
                var validaUsuario = db.DbUsuarios.ToList().Exists(c => c.Id == id);

                if (!validaUsuario)
                    return BadRequest("Usuario nao encontrado");

                var listaModeloExistente = db.DbListasModelo.ToList().FindAll(a => a.Status.Equals("A") && a.UsuId.Equals(id));

                if (listaModeloExistente.Count() > 0)
                    return Ok(listaModeloExistente);

                var listaModelo = new ListaModelo
                {
                    Nome = $"lista_{id}_{DateTime.Now.ToString("ddMMyyyyhhmmss")}",
                    DtCadastro = DateTime.Now,
                    UsuId = id,
                    Status = "A"
                };

                db.DbListasModelo.Add(listaModelo);
                db.SaveChanges();

                return Ok(listaModelo);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("gravar-lista-modelo-usuario")]
        public IActionResult GravaListaModeloUsuario([FromBody]ProdutosLista produtosLista)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var erro = string.Join(", ", ModelState.Values
                                .SelectMany(x => x.Errors)
                                .Select(x => x.ErrorMessage));
                    return BadRequest(erro);
                }

                if (produtosLista.Id != 0)
                    db.Entry(produtosLista).State = EntityState.Modified;
                else
                    db.DbProdutosLista.Add(produtosLista);

                db.SaveChanges();

                return Ok("Cadastro realizado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("busca-produtos-lista-selecionado/{idLista}")]
        public IActionResult BuscaProdutosListaSelecionado(int idLista)
        {
            try
            {
                var produtosLista = db.DbProdutosLista.ToList().Where(a => a.LimId.Equals(idLista));

                return Ok(produtosLista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}