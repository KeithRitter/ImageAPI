using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace maingoframe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        private readonly IWordService _wordList;

        public TestController(IWordService wordListService)
        {
            _wordList = wordListService;
        }

        [HttpGet]
        public static string Get()
        {
            return "Whatru lookin at huh?";
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public IActionResult Post()
        {
            var file = Request.Form.Files.FirstOrDefault(x => _okContentTypes.Contains(x.ContentType));
            if (!Request.Headers.TryGetValue("pw", out var hValue)) return new BadRequestResult();
            if (!hValue.ToString().Equals("plsdontstealthisgithub", StringComparison.InvariantCulture)) return new BadRequestResult();

            if (file is null || file.Length < 0) return BadRequest();
            if (!Path.HasExtension(file.FileName)) return BadRequest();

            var name = GetAvailableFileName(file.FileName.Split(".").Last());
            if (!CreateFile(file, name)) return new BadRequestResult();

            return Ok($"{Request.Scheme}://{Request.Host}{Request.PathBase}/ss/{name}");
        }

        private string GetAvailableFileName(string ext, int attempts = 0)
        {
            var name = _wordList.GetRandomWord();
            if (attempts > 100) name += DateTime.Now.Ticks;
            if (attempts > 1) Console.WriteLine($"Attempts: {attempts}");
            name += $".{ext}";
            if (System.IO.File.Exists($"/var/www/mywebsite/html/ss/{name}")) GetAvailableFileName(ext, attempts + 1);
            return name;
        }

        private static bool CreateFile(IFormFile file, string name)
        {
            using (var fs = new FileStream($"/var/www/mywebsite/html/ss/{name}", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                file.CopyTo(fs);
            }
            return true;
        }


        private readonly List<string> _okContentTypes = new List<string>()
        {
            {"image/jpg"}, {"image/jpeg"}, {"image/pjpeg"}, {"image/png"}, {"image/gif"}
        };
    }
}

