using System.Diagnostics;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;
using XSS.Models;

namespace XSS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private HtmlEncoder _htmlEncode = HtmlEncoder.Default;
        private UrlEncoder _urlEncode = UrlEncoder.Default;
        private JavaScriptEncoder _javaScriptEncoder = JavaScriptEncoder.Default;
        public HomeController(ILogger<HomeController> logger, HtmlEncoder htmlEncode = null, UrlEncoder urlEncode = null, JavaScriptEncoder javaScriptEncoder = null)
        {
            _logger = logger;
            _htmlEncode = htmlEncode;
            _urlEncode = urlEncode;
            _javaScriptEncoder = javaScriptEncoder;
        }
        public IActionResult CommentAdd()
        {
            if (System.IO.File.Exists("comment.txt"))
            {
                ViewBag.comment = System.IO.File.ReadAllText("comment.txt");
            }
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult CommentAdd(string name,string comment)
        {
            _urlEncode.Encode(name); // url XSS şifreleme
            _htmlEncode.Encode(comment); // html XSS şifreleme
            _javaScriptEncoder.Encode(comment); // js XSS şifreleme
            ViewBag.name = name;
            ViewBag.comment = comment;

            System.IO.File.AppendAllText("wwwroot/comment.txt", $"{name}:{comment}\n");
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
