using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassagerApp.DAL.Models;

namespace MassagerApp.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<MassageEntity> Massages { get; }
        IGenericRepository<ChatsEntity> Chats { get; }
    }
}
