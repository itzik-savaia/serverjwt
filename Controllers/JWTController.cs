using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWT.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWT.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JWTController : ControllerBase
    { 
        private readonly IJwtAutenticationManager jwtAutenticationManager;
        public JWTController(IJwtAutenticationManager jwtAutenticationManager)
        {
            this.jwtAutenticationManager = jwtAutenticationManager;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserCred userCred)
        {
            try
            {
                var url_project = MiddlewareCallAPI.CallApi(userCred.url);
                var token = jwtAutenticationManager.Authenticate(userCred.Username, userCred.Password, url_project);
                if (token == null)
                    return Unauthorized();
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
