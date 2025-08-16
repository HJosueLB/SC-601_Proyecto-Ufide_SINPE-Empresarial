using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SINPE.Empresarial.Web.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/secure")]
    public class SecureController : ApiController
    {
        [HttpGet, Route("ping")]
        public IHttpActionResult Ping() => Ok(new { ok = true, user = User.Identity.Name });
    }
}
