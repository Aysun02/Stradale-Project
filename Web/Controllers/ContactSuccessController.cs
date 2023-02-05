using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Web.Controllers
{
    public class ContactSuccessController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
