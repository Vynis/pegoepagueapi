using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiPegoPaque.Models;
using WebApiPegoPaque.Util;
using System.Linq;

namespace WebApiPegoPaque.Controllers
{
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

                produtos = db.DbProtudos.ToList().OrderBy(a => a.Nome).ToList();

                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("buscar-produtos/{id}")]
        public IActionResult BuscaProdutoSelecionado(int id)
        {
            try
            {
                var produto = new Produto();

                produto = db.DbProtudos.Find(id);

                using (var contexto = new DataContext())
                {
                    //INNER JOIN COM MARCAS
                    var marcas = from m in contexto.DbMarcas
                                    join cm in contexto.DbCategoriaMarca on m.Id equals cm.MarId
                                    where cm.ProId == id
                                    select new
                                    {
                                        NomeMarca = m.Nome
                                    };

                    //INNER JOIN COM VOLUMES

                

                    return Ok(
                      new
                      {
                          produto,
                          marcas = marcas.ToList()
                      }
                    );
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}