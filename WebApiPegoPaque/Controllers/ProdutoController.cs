using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiPegoPaque.Models;
using WebApiPegoPaque.Util;

namespace WebApiPegoPaque.Controllers
{
    [Authorize()]
    [Route("api/produto")]
    public class ProdutoController : Controller
    {

        private ReturnAllService retorno = new ReturnAllService();
        private DataContext db = new DataContext();

        [HttpGet]
        [Route("buscar-produtos")]
        public IActionResult BuscarProdutos()
        {
            try
            {
                var produtos = new List<Produto>();

                produtos = db.DbProdutos.ToList().Where(a => a.Status.Equals("A")).OrderBy(b => b.Nome).ToList();

                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("selecionar-produto/{id}")]
        public IActionResult BuscaProdutoSelecionado(int id)
        {
            try
            {
                var produto = new Produto();

                produto = db.DbProdutos.Find(id);

                using (var contexto = new DataContext())
                {
                    //INNER JOIN COM MARCAS
                    var marcas = from m in contexto.DbMarcas
                                 join cm in contexto.DbCategoriaMarca on m.Id equals cm.MarId
                                 where cm.ProId == id
                                 select new
                                 {
                                     id = m.Id,
                                     NomeMarca = m.Nome
                                 };

                    //INNER JOIN COM VOLUMES
                    var tipoVolumes = from t in contexto.DbTipoVolumes
                                      join tvp in contexto.DbTipoVolumesProdutos on t.Id equals tvp.TivId
                                      where tvp.ProId == id
                                      select new
                                      {
                                          id = t.Id,
                                          NomeVolume = t.Nome
                                      };


                    return Ok(
                      new
                      {
                          produto,
                          marcas = marcas.ToList(),
                          volumes = tipoVolumes.ToList()
                      }
                    );
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("selecionar-produto-registrado/{prlId}")]
        public IActionResult BuscaProdutoRegistrado(int prlId)
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

                var produloLista = db.DbProdutosLista.Find(prlId);

                using (var contexto = new DataContext())
                {
                    //INNER JOIN COM MARCAS
                    var marcas = from m in contexto.DbMarcas
                                 join cm in contexto.DbCategoriaMarca on m.Id equals cm.MarId
                                 where cm.ProId == produloLista.ProId
                                 select new
                                 {
                                     id = m.Id,
                                     NomeMarca = m.Nome
                                 };

                    //INNER JOIN COM VOLUMES
                    var tipoVolumes = from t in contexto.DbTipoVolumes
                                      join tvp in contexto.DbTipoVolumesProdutos on t.Id equals tvp.TivId
                                      where tvp.ProId == produloLista.ProId
                                      select new
                                      {
                                          id = t.Id,
                                          NomeVolume = t.Nome
                                      };

                    return Ok( 
                        new {
                            produloLista,
                            marcas = marcas.ToList(),
                            volumes = tipoVolumes.ToList()
                         } );
                }

                    

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}