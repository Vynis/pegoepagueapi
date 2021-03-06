﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
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
                    return Ok(new { listaModelo = listaModeloExistente, count = listaModeloExistente.ToList().Count });
                else
                    return Ok(new { listaModelo = "", count = listaModeloExistente.ToList().Count });

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("gravar-lista-modelo-usuario")]
        public IActionResult GravaListaModelo(int idUsuario)
        {
            try
            {
                if (idUsuario <= 0)
                    return BadRequest("Informe id do usuario valido");

                var listaModelo = new ListaModelo
                {
                    Nome = $"lista_{idUsuario}_{DateTime.Now.ToString("ddMMyyyyhhmmss")}",
                    DtCadastro = DateTime.Now,
                    UsuId = idUsuario,
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
        [Route("gravar-produtos-lista-modelo-usuario")]
        public IActionResult GravaProdutosListaModeloUsuario([FromBody]ProdutosLista produtosLista)
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

                //Copia do nome da marca
                var marca = db.DbMarcas.Find(produtosLista.MarId);
                produtosLista.MarNome = marca.Nome;

                //Copia do nome do tipo de volume
                var volume = db.DbTipoVolumes.Find(produtosLista.TivId);
                produtosLista.Volume = volume.Nome;

                if (produtosLista.Id != 0)
                    db.Entry(produtosLista).State = EntityState.Modified;
                else
                    db.DbProdutosLista.Add(produtosLista);

                db.SaveChanges();

                return Ok(produtosLista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("busca-produtos-lista-selecionado")]
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

        [HttpGet]
        [Route("busca-qtd-produtos-lista-selecionado/{idLista}")]
        public IActionResult BuscaQtdProdutosListaSelecionado(int idLista)
        {
            try
            {
                var produtosLista = db.DbProdutosLista.ToList().Where(a => a.LimId.Equals(idLista));

                return Ok(new { count = produtosLista != null ? produtosLista.ToList().Count : 0 });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("excluir-lista-andamento")]
        public IActionResult ExcluiListaAndamento(int idLista)
        {
            try
            {
                var listaEmAndamento = db.DbListasModelo.Find(idLista);

                if (listaEmAndamento == null)
                    return BadRequest("Nenhuma lista em andamento selecionada");

                if (!listaEmAndamento.Status.Equals("A"))
                    return BadRequest("A lista não esta em andamento");

                listaEmAndamento.Status = "F";
                db.Entry(listaEmAndamento).State = EntityState.Modified;
                db.SaveChanges();

                var listaNova = GravaListaModelo(listaEmAndamento.UsuId);

                return Ok(listaNova);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("excluir-produto-lista")]
        public IActionResult ExcluirProdutoLista(int IdProdLista)
        {
            try
            {

                var produtoLista = db.DbProdutosLista.Find(IdProdLista);

                if (produtoLista == null)
                    return BadRequest("Produto não encontrado");

                db.DbProdutosLista.Remove(produtoLista);
                db.SaveChanges();

                return Ok(produtoLista);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);

            }
        }


    }
}