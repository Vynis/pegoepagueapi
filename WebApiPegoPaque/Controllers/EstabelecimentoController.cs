using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApiPegoPaque.Models;
using WebApiPegoPaque.Util;

namespace WebApiPegoPaque.Controllers
{
    [Route("api/estabelecimento")]
    public class EstabelecimentoController : Controller
    {
        private ReturnAllService retorno = new ReturnAllService();
        private DataContext db = new DataContext();

        [HttpGet]
        [Route("buscar-estabelecimentos")]
        public IActionResult BuscarEstabelecimentos()
        {
            try
            {
                var estabelecimentos = db.DbEstabelecimentos.ToList().Where(a => a.Status.Equals("A")).OrderBy(b => b.Nome);

                return Ok(estabelecimentos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}