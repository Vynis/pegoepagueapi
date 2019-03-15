using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiPegoPaque.Models;
using WebApiPegoPaque.Util;

namespace WebApiPegoPaque.Controllers
{
    [Route("api/usuario")]
    public class UsuarioController : Controller
    {

        private ReturnAllService retorno = new ReturnAllService();
        private DataContext db = new DataContext();

        [HttpPost]
        [Route("gravar-usuario")]
        public IActionResult GravarUsuario([FromBody] Usuario usuario)
        {
            try
            {
                //Defini registros padroes
                usuario.DtCadastro = DateTime.Now;
                usuario.Status = "A";

                if (!ModelState.IsValid)
                {
                    var erro = string.Join(", ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                    return BadRequest(erro);
                }
                    

                

                //Criptografa a senha
                usuario.Senha = Encryptor.MD5Hash(usuario.Senha);

                //Valida se já possui email cadastrado
                var listaUsuarioCadastrado = db.DbUsuarios.Where(c => c.Email.Equals(usuario.Email.Trim()));

                if (listaUsuarioCadastrado.ToList().Count > 0)
                    return BadRequest("E-mail já esta cadastrado. Favor utilize outro email");

                //Atribui os dados para framework
                db.DbUsuarios.Add(usuario);
                db.SaveChanges();

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest($"Erro na api {ex.Message}");
            }

        }

    }
}