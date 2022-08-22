using Microsoft.Extensions.DependencyInjection;
using MassagerApp.BLL.Interfaces;
using MassagerApp.BLL.Models;
using MassagerApp.BLL.Services;
using MassagerApp.DAL;
using MassagerApp.DAL.Interfaces;
using MassagerApp.DAL.Models;
using MassagerApp.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MassagerApp.PL.Infastructure
{
    public static class DependencyConfiguration
    {
        public static void AddDependencies(this IServiceCollection service)
        {
            //DAL configuration
            service.AddScoped<IUnitOfWork, UnitOfWork>();
            service.AddTransient<IGenericRepository<ChatsEntity>, GenericRepository<ChatsEntity>>();
            service.AddTransient<IGenericRepository<MassageEntity>, GenericRepository<MassageEntity>>();

            //BL configuration
            service.AddTransient<IChatServise, ChatServise>();
            service.AddTransient<IMassageService, MassageService>();
        }

    }
}
