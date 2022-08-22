using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MassagerApp.BLL.Interfaces;
using MassagerApp.BLL.Models;
using MassagerApp.PL.ViewModels;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using MassagerApp.DAL.Models;

namespace ShortUrlMVC.PL.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
