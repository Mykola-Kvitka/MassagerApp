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
    public class ChatController : Controller
    {
        private readonly IChatServise _chatService;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IMapper _mapper;

        public ChatController(IChatServise chatService, IMapper mapper, UserManager<UserEntity> userManager)
        { 
            _userManager = userManager;
            _chatService = chatService;
            _mapper = mapper;
        }

        [HttpGet("chat")]
        public async Task<ActionResult> Index()
        {
            var user = (await _userManager.GetUserAsync(HttpContext.User)).Id;

            var chatVm = _mapper.Map<IEnumerable<Chat>, IEnumerable<ChatViewModel>>(await _chatService.GetAllAsync(user));

            return View(chatVm);
        }

        [HttpGet("chat/create")]
        public ActionResult Create()
        {
            ViewBag.Users = new SelectList(_mapper.Map<List<UserEntity>>(
                       _userManager.Users.ToList()), "Id", "UserName");

            return View();
        }

        [HttpPost("chat/create")]
        public async Task<ActionResult> CreateAsync(CreateChatViewModel requestVm)
        {
            var user = (await _userManager.GetUserAsync(HttpContext.User)).Id;
            requestVm.UserIds.Add(user);

             _chatService.CreateAsync(requestVm.UserIds);

            return Redirect("~/chat/Index");
        }
    }
}
