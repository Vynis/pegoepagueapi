using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using WebApiPegoPaque.Models;
using WebApiPegoPaque.Util;
using Microsoft.AspNetCore.Http;

namespace WebApiPegoPaque.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {

        private DataContext db = new DataContext();
        Autenticacao AutenticaoServico;

        public ValuesController(IHttpContextAccessor context)
        {
            AutenticaoServico = new Autenticacao(context);
        }

        // GET api/values
        [HttpGet]
        public List<Product> Get()
        {

            AutenticaoServico.Autenticar();

            //var lista = db.Products
            return db.Products.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
