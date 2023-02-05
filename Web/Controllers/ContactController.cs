using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Services.Abstract;
using Web.ViewModels;

namespace Web.Controllers
{

    public class ContactController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

    }
}
