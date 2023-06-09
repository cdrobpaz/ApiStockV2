﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ApiStockV2.Helpers
{
    public class HelperToken
    {
     
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretKey { get; set; }

        public HelperToken(IConfiguration configuration)
        {
            this.Issuer = configuration.GetValue<string>("JWT:Issuer");
            this.Audience = configuration.GetValue<string>("JWT:Audience");
            this.SecretKey = configuration.GetValue<string>("JWT:SecretKey");
        }

        //EL TOKEN ES GENERADO MEDIANTE UNA CLAVE SIMETRICA A PARTIR DE 
        //UN SECRET KEY PERSONALIZADO.  REALIZA UN CIFRADO
        public SymmetricSecurityKey GetKeyToken()
        {
            byte[] data =
                Encoding.UTF8.GetBytes(this.SecretKey);
            return new SymmetricSecurityKey(data);
        }

        //DEBEMOS CONFIGURAR LAS OPCIONES PARA LA VALIDACION DE 
        //NUESTRA TOKEN.  ESTOS METODOS DE OPCIONES SON Action
        public Action<JwtBearerOptions> GetJwtOptions()
        {
            Action<JwtBearerOptions> options =
                new Action<JwtBearerOptions>(options =>
                {
                    //DEBEMOS INDICAR LAS VALIDACIONES QUE REALIZARA EL TOKEN
                    options.TokenValidationParameters =
                    new TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateActor = true,
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidIssuer = this.Issuer,
                        ValidAudience = this.Audience,
                        IssuerSigningKey = this.GetKeyToken()
                    };
                });
            return options;
        }

        //METODO PARA EL ESQUEMA DE AUTENTIFICACION
        public Action<AuthenticationOptions> GetAuthenticationOptions()
        {
            Action<AuthenticationOptions> options =
                new Action<AuthenticationOptions>(options =>
                {
                    options.DefaultAuthenticateScheme =
                    JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme =
                    JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme =
                    JwtBearerDefaults.AuthenticationScheme;
                });
            return options;
        }
    }
    
}
