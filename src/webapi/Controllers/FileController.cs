using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private const string PATHFOLDER = "./files";
        private const string PATHFILE = PATHFOLDER + "/simple-file.txt";
        private readonly ILogger<FileController> _logger;

        public FileController(ILogger<FileController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post()
        {
            if (System.IO.File.Exists(PATHFILE))
                return BadRequest();

            if(!System.IO.Directory.Exists(PATHFOLDER))
                System.IO.Directory.CreateDirectory(PATHFOLDER);

            System.IO.File.WriteAllText(PATHFILE, $"[{DateTime.Now.ToString()}] Arquivo criado");

            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            if(!System.IO.File.Exists(PATHFILE))
                return BadRequest();

            System.IO.File.Delete(PATHFILE);

            return Ok();
        }

        [HttpPut]
        public IActionResult Put([FromBody] string message)
        {
            if(!System.IO.File.Exists(PATHFILE))
                return BadRequest();

            System.IO.File.WriteAllText(
                PATHFILE,
                $"[{DateTime.Now.ToString()}] {message}");
            return Ok();
        }
    }
}
