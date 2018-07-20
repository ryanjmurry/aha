using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Aha.Models;


namespace Aha.Controllers
{
    public class ClientsController : Controller
    {
        [HttpGet("/clients/new")]
        public ActionResult Form()
        {
            return View();
        }
    }
}