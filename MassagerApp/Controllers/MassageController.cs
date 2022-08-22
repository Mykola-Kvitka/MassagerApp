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

            var massagePage = new MassagePagedViewModel() { 
                Massages = _mapper.Map<IEnumerable<Massages>, 
                IEnumerable<MassageViewModel>>(chatMassage).OrderBy(a => a.CreateDate),
                Page = page, 
                TotalCount = await _massageService.GetCountAsync() };
            
            return View(massagePage);
        }

        [HttpPost("massage/create")]
        public async Task<ActionResult> Create(MassageViewModel requestVm)
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
            var red = (await _massageService.GetAsync(id)).ChatId;
            await _massageService.DeleteAsync(id);

            return Redirect("~/massage/" + red);
        }

        [HttpPost("massage/softdelete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SoftDelete(Guid id)
        {
            var red = (await _massageService.GetAsync(id)).ChatId;

            _massageService.SoftDeleteAsync(id);

            return Redirect("~/massage/" + red);
        }

    }

}

