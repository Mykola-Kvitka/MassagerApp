using MassagerApp.BLL.Models;
using MassagerApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MassagerApp.BLL.Interfaces
{
    public interface IChatServise
    {
        void CreateAsync(IEnumerable<string> request);
        Task<IEnumerable<Chat>> GetAllAsync(string id);

    }
}
