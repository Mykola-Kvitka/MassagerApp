using AutoMapper;
using MassagerApp.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassagerApp.DAL.Models;
using MassagerApp.BLL.Models;
using MassagerApp.PL.ViewModels;

namespace MassagerApp.PL.Controllers
{
    [Authorize]
    public class MassageController : Controller
    {
        private readonly IMassageService _massageService;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IMapper _mapper;

        public MassageController(IMassageService massageService, IMapper mapper, UserManager<UserEntity> userManager)
        { 
            _userManager = userManager;
            _massageService = massageService;
            _mapper = mapper;
        }

        [HttpGet("massage/{id}")]
        public async Task<ActionResult> Index(Guid id, int page = 1)
        {
            var userId = (await _userManager.GetUserAsync(HttpContext.User)).Id;

            var chatMassage =  await _massageService.GetPagedAsync(id,userId, page);
            
            return View(_mapper.Map<IEnumerable<Massages>, IEnumerable<MassageViewModel>>(chatMassage));
        }

        [HttpGet("massage/create")]
        public ActionResult Create()
        {
            ViewBag.Users = new SelectList(_mapper.Map<List<UserEntity>>(
                       _userManager.Users.ToList()), "Id", "UserName");

            return View();
        }

        [HttpPost("massage/create")]
        public async Task<ActionResult> CreateAsync(MassageViewModel requestVm)
        {
            var user = (await _userManager.GetUserAsync(HttpContext.User));

            requestVm.OwnerId = user.Id;
            requestVm.UserName = user.UserName;

            await _massageService.CreateAsync(_mapper.Map<MassageViewModel,Massages>(requestVm));

            return Redirect("~/massage/" + requestVm.ChatId);
        }      
        
        [HttpPost("massage/replay")]
        public async Task<ActionResult> ReplayAsync(MassageViewModel requestVm)
        {
            var user = (await _userManager.GetUserAsync(HttpContext.User));

            requestVm.Massage = "@" + requestVm.UserName + " " + requestVm.Massage;

            requestVm.OwnerId = user.Id;
            requestVm.UserName = user.UserName;

            await _massageService.CreateAsync(_mapper.Map<MassageViewModel,Massages>(requestVm));

            return Redirect("~/massage/" + requestVm.ChatId);
        }

        [HttpPost("massage/delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _massageService.DeleteAsync(id);

            return Redirect("~/massage/");
        }

        [HttpPost("massage/softdelete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult SoftDelete(Guid id)
        {
            _massageService.SoftDeleteAsync(id);

            return Redirect("~/massage/");
        }

    }

}

