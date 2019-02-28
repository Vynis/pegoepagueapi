using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace WebApiPegoPaque.Util
{
    

    public class Autenticacao
    {
        public static string ApiToken = Encryptor.MD5Hash("V@I#N!k%heu");
        public static string FalhaAutenticacao = "Falha na autenticação.";
        IHttpContextAccessor contextAccessor;

        public Autenticacao(IHttpContextAccessor context)
        {
            contextAccessor = context;
        }

        public void Autenticar()
        {
            try
            {
                string TokenRecebido = contextAccessor.HttpContext.Request.Headers["Token"].ToString();
                if (!String.Equals(ApiToken, TokenRecebido))
                {
                    throw new Exception(FalhaAutenticacao);
                }
            }
            catch
            {
                throw new Exception(FalhaAutenticacao);
            }
        }

    }




}
