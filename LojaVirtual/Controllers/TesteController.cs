using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TesteController : ControllerBase
    {
        /*[Route("{teste}")*/
        //[HttpGet]        
        //public ActionResult miguel(string teste)
        //{
        //    return Ok();
        //}
        [HttpGet]
        [Route("{teste3}")]
        public ActionResult miguel2(string teste3)
        {
            return Ok();
        }
    }
}
