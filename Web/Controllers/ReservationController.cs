﻿using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ReservationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
