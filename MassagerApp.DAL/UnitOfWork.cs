using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassagerApp.DAL.DataAccses;
using MassagerApp.DAL.Interfaces;
using MassagerApp.DAL.Models;
using MassagerApp.DAL.Repositories;

namespace MassagerApp.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataAccsess _appData;

        private GenericRepository<MassageEntity> _massages;
        private GenericRepository<ChatsEntity> _chats;

        public UnitOfWork(DataAccsess appData)
        {
            _appData = appData;
        }

        public IGenericRepository<MassageEntity> Massages => _massages ??= new GenericRepository<MassageEntity>(_appData);
        public IGenericRepository<ChatsEntity> Chats => _chats ??= new GenericRepository<ChatsEntity>(_appData);

    }
}
