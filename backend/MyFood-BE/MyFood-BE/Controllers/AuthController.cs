using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyFood_BE.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IActionResult BuildToken()
        {
            var claims = new[]
            {
                //utilizamos los claims para enviar algunas informaciones
                new Claim(JwtRegisteredClaimNames.UniqueName, "test@x"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            //creanos una llave secreta y la configuramos como variable de entorno
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("axsamksxASLALAaklsdak(O)smdas(R)dasda(B)d23a1(I)sda5(S)s89das51as5x61asx7a1sc4asdsa;f;;'.dqw,q;qdq[1]'asdasda[]asdaasaasasdaxals;xas4616axs8aq87ewq1dasd*d/*/*a/*da/sda/s*d/s*ad*sa/d*/d"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            JwtSecurityToken token = new JwtSecurityToken(
               issuer: "devteams.com",
               audience: "devteams.com",
               claims: claims,
               signingCredentials: creds);

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}