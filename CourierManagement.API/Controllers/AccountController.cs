using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using CourierManagement.API.Models;

namespace CourierManagement.API.Controllers
{

    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        public AuthenticationRepository repo = null;
        public AccountController()
        {
            repo = new AuthenticationRepository();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (repo != null)
                {
                    repo.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        [Route("RegisterAccount")]
        public async Task<IHttpActionResult> Register(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await repo.RegisterUser(userModel);
            if (result == null)
            {
                return InternalServerError();
            }
            return Ok();
        }
    }
}
