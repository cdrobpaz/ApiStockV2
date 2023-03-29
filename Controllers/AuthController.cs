using ApiStockV2.Helpers;
using ApiStockV2.Models;
using ApiStockV2.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiStockV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private RepositoryStock repo;
        private HelperToken helper;
        public AuthController(RepositoryStock repo, HelperToken helper)
        {
            this.repo = repo;
            this.helper = helper;
        }
        [HttpPost]
        [Route("[action]")]
        public ActionResult Login(LoginModel model)
        {
            LoginModel mod = this.repo.ExisteAdmin(model.UserName, model.Password);
            if (mod == null)
            {
                return Unauthorized();
            }
            else
            {
                //UN TOKEN CONTIENE UNAS CREDENCIALES
                SigningCredentials credentials =
                    new SigningCredentials(this.helper.GetKeyToken()
                    , SecurityAlgorithms.HmacSha256);
                //Mediante Claims vamos a almacenar información
                //string jsonAdmin = JsonConvert.SerializeObject(mod);
                //Claim[] info = new[]
                //{
                //    new Claim("ADMIN", jsonAdmin)
                //};
                //ES EL MOMENTO DE GENERAR EL TOKEN
                //EL TOKEN ESTARA COMPUESTO POR ISSUER, AUDIENCE, CREDENTIALS
                //TIME
                JwtSecurityToken token =
                    new JwtSecurityToken(
                        issuer: this.helper.Issuer,
                        audience: this.helper.Audience,
                        signingCredentials: credentials,
                        expires: DateTime.UtcNow.AddDays(1000),
                        notBefore: DateTime.UtcNow
                        );
                //DEVOLVEMOS UNA RESPUESTA CORRECTA CON EL TOKEN
                return Ok(
                    new
                    {
                        response =
                        new JwtSecurityTokenHandler().WriteToken(token)
                    });
            }
        }
    }
}
