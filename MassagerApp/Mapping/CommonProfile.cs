using AutoMapper;
using MassagerApp.BLL.Models;
using MassagerApp.PL.ViewModels;
using MassagerApp.DAL.Models;
using System.Linq.Expressions;
using System;

namespace MassagerApp.PL.Mapping
{
    public class CommonProfile : Profile
    {
        public CommonProfile()
        {
            AddBusinessMapping();
            AddWebMapping();
        }

        public void AddWebMapping()
        {
            CreateMap<MassageEntity, Massages>().ReverseMap();
            CreateMap<ChatsEntity, Chat>().ReverseMap();
        }

        public void AddBusinessMapping()
        {
            CreateMap<Massages, MassageViewModel>().ReverseMap();
            CreateMap<Chat, ChatViewModel>().ReverseMap();
        }
    }
}
