using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication.Controllers
{
    public class MsgController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetMsg()
        {
            return Request.CreateResponse(
                HttpStatusCode.OK,
                new MsgDto("Msg."));
        }
    }
}
