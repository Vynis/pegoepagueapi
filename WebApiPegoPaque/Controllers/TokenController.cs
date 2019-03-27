using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApiPegoPaque.Models;
using WebApiPegoPaque.Util;

namespace WebApiPegoPaque.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly IConfiguration _configuration;
        private DataContext db = new DataContext();
        private ReturnToken retorno = new ReturnToken();

        public TokenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult RequestToken([FromBody] Usuario request)
        {
            try
            {

                var usuario = db.DbUsuarios.Where(c => c.Email.Equals(request.Email) && c.Senha.Equals(Encryptor.MD5Hash(request.Senha)));

                if (usuario.ToList().Count == 0)
                    return BadRequest("E-mail/Senha esta invalido!");

                var usuarioRetorno = new Usuario();

                foreach (var item in usuario.ToList())
                {
                    usuarioRetorno = item;
                }

                var claims = new[]
                {
                     new Claim(ClaimTypes.Email, request.Email)
                    };

                //recebe uma instancia da classe SymmetricSecurityKey 
                //armazenando a chave de criptografia usada na criação do token
                var key = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));

                //recebe um objeto do tipo SigninCredentials contendo a chave de 
                //criptografia e o algoritmo de segurança empregados na geração 
                // de assinaturas digitais para tokens
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                     issuer: "pegopague",
                     audience: "pegopague",
                     claims: claims,
                     expires: DateTime.Now.AddMinutes(30),
                     signingCredentials: creds);


                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    user = usuarioRetorno
                });


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}