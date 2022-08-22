using MassagerApp.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassagerApp.BLL.Interfaces
{
    public interface IMassageService
    {
        Task CreateAsync(Massages request);
        Task<IEnumerable<Massages>> GetPagedAsync(Guid ChatId, int page = 1, int pageSize = 15);
        Task DeleteAsync(Guid id);
        void SoftDeleteAsync(Guid id);
        Task<Massages> GetAsync(Guid id);
        void UpdateAsync(Massages request);

    }
}
